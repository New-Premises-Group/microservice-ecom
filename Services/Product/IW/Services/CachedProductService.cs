using IW.Interfaces;
using IW.Models;
using IW.Models.DTOs;
using IW.Models.DTOs.Product;
using IW.Models.DTOs.ProductDto;
using IW.Models.Entities;
using Mapster;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace IW.Services
{
    public class CachedProductService : IProductService
    {
        private readonly ProductService _decorated;
        private readonly IDistributedCache _distributedCache;
        private readonly AppDbContext _dbContext;

        public CachedProductService(ProductService decorated, IDistributedCache distributedCache,AppDbContext dbContext)
        {
            _decorated = decorated;
            _distributedCache = distributedCache;
            _dbContext = dbContext;
        }

        public async Task CreateProduct(CreateProduct input)
        {
            await _decorated.CreateProduct(input);

            string key = input.Name;
            await _distributedCache.SetStringAsync(key,
                JsonConvert.SerializeObject(input),
                new DistributedCacheEntryOptions()
                {
                    SlidingExpiration = TimeSpan.FromSeconds(30),
                });
        }

        public async Task DeleteProduct(int id)
        {
            string key = CreateKey(id);
            Task deleteProduct= _decorated.DeleteProduct(id);
            Task updateCache= _distributedCache.RemoveAsync(key);
            await Task.WhenAll(updateCache,deleteProduct);
        }

        public async Task<ProductDto?> GetProduct(int id)
        {
            string key = CreateKey(id);
            string? cachedProduct = await _distributedCache.GetStringAsync(key);

            ProductDto? product;
            if (string.IsNullOrEmpty(cachedProduct))
            {
                product = await _decorated.GetProduct(id);
                if (product is null)
                {
                    return product;
                }
                await _distributedCache.SetStringAsync(key, 
                    JsonConvert.SerializeObject(product), 
                    new DistributedCacheEntryOptions()
                {
                    SlidingExpiration = TimeSpan.FromSeconds(30),
                });
                return product;
            }

            product = JsonConvert.DeserializeObject<ProductDto>(cachedProduct);

            if (product is not null)
            {
                _dbContext.Set<Product>().Attach(product.Adapt<Product>());
            }
            return product;
        }

        public async Task<IEnumerable<ProductDto>> GetProducts(int offset, int amount)
        {
            return await _decorated.GetProducts(offset, amount);
        }

        public async Task<IEnumerable<ProductDto>> GetProducts(GetProduct query, int offset, int amount)
        {
            string key = "";
            if (!string.IsNullOrEmpty(query.Name)){
                key = CreateKey(query.Name);
            }
            if (query.CategoryId!=null)
            {
                key = CreateKey((int)query.CategoryId);
            }
            string? cachedProducts = await _distributedCache.GetStringAsync(key);

            if(string.IsNullOrEmpty(cachedProducts))
            {
                IEnumerable<ProductDto> newProducts = await _decorated.GetProducts(query, offset, amount);
                
                return newProducts;
            }
            var products = JsonConvert.DeserializeObject<IEnumerable<ProductDto>>(cachedProducts);

            if (products is not null)
            {
                foreach(Product product in products.Adapt<IEnumerable<Product>>())
                {
                    _dbContext.Set<Product>().Attach(product);
                }
            }
            return products;
        }

        public async Task UpdateProduct(int id, UpdateProduct model)
        {
            await _decorated.UpdateProduct(id,model);
            ProductDto product = _dbContext
                .Set<Product>()
                .Entry(model.Adapt<Product>())
                .Properties
                .Select(e => e.CurrentValue)
                .Adapt<ProductDto>();

            string key = CreateKey(product.Id);
            await _distributedCache.SetStringAsync(key,
                JsonConvert.SerializeObject(product),
                new DistributedCacheEntryOptions()
                {
                    SlidingExpiration = TimeSpan.FromSeconds(30),
                });
        }

        /// <summary>
        /// Create a string key for the key - value pair store values in Redis.
        /// </summary>
        /// <param name="id">Id of the product</param>
        /// <returns>String key to identify in redis</returns>
        private static string CreateKey(int id)
        {
            return $"product-{id}";
        }

        private static string CreateKey(string key)
        {
            return $"product-{key}";
        }
    }
}
