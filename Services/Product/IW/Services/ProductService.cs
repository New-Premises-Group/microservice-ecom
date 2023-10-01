using IW.Exceptions.ReadProductError;
using IW.Interfaces;
using IW.Models.DTOs;
using IW.Models.DTOs.Product;
using IW.Models.DTOs.ProductDto;
using IW.Models.Entities;

namespace IW.Services
{
    public class ProductService : IProductService
    {
        public readonly IUnitOfWork _unitOfWork;
        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task CreateProduct(CreateProduct input)
        {
            var product = await _unitOfWork.Products.FindByCondition(u => u.Name == input.Name);
            Category category =  await _unitOfWork.Categories.GetById(input.CategoryId);

            Product newProduct = new()
            {
                Name = input.Name,
                Description = input.Description,
                Images = input.Images,
                Price = input.Price,
                SKU = input.SKU,
                CategoryId= input.CategoryId,
                Category= category
            };

            ProductValidator validator = new();
            validator.ValidateAndThrowException(newProduct);

            _unitOfWork.Products.Add(newProduct);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<ProductDto> GetProduct(int id)
        {
            var product = await ProductExist(id);
            if (Equals(product, null))
            {
                throw new ProductNotFoundException(id);
            }
            var category = await _unitOfWork.Categories.GetById(product.Id);
            ProductDto result = new()
            {
                Id = id,
                Name = product.Name,
                Category=category,
                CategoryId = product.CategoryId,
                Description = product.Description,
                Images= product.Images,
                Price= product.Price,
                SKU = product.SKU,
            };
            return result;
        }

        public async Task<IEnumerable<ProductDto>> GetProducts(int offset, int amount)
        {
            var products = await _unitOfWork.Products.GetAll(offset, amount);
            ICollection<ProductDto> result = new List<ProductDto>();
            foreach (var product in products)
            {
                ProductDto item = new()
                {
                    Id= product.Id,
                    Name = product.Name,
                    Description = product.Description,
                    Images= product.Images,
                    Price= product.Price,
                    SKU= product.SKU,
                    Category=product.Category,
                    CategoryId= product.CategoryId,
                };
                result.Add(item);
            }
            return result;
        }

        public async Task<IEnumerable<ProductDto>> GetProducts(GetProduct query, int offset, int amount)
        {
            var products = await _unitOfWork.Products.FindByConditionToList(
                p => p.Name == query.Name ||
                p.CategoryId==query.CategoryId
                , offset, amount);
            ICollection<ProductDto> result = new List<ProductDto>();
            foreach (var product in products)
            {
                ProductDto item = new()
                {
                    Id = product.Id,
                    Images = product.Images,
                    CategoryId = product.CategoryId,
                    Description = product.Description,
                    Category = product.Category,
                    Price= product.Price,
                    SKU= product.SKU,
                    Name = product.Name
                };
                result.Add(item);
            }
            return result;
        }

        public async Task UpdateProduct(int id, UpdateProduct model)
        {
            var product = await ProductExist(id);
            if (Equals(product, null)) throw new ProductNotFoundException(id);

            product.Name = model.Name ?? product.Name;
            product.CategoryId = model.CategoryId;
            product.Description = model.Description;
            product.Price = model.Price;
            product.SKU = model.SKU;
            product.Images=model.Images;

            ProductValidator validator = new();
            validator.ValidateAndThrowException(product);

            _unitOfWork.Products.Update(product);
            await _unitOfWork.CompleteAsync();
        }

        public async Task DeleteProduct(int id)
        {
            var user = await ProductExist(id);
            if (Equals(user, null)) throw new ProductNotFoundException(id);

            _unitOfWork.Products.Remove(user);
            await _unitOfWork.CompleteAsync();
        }
        private async Task<Product?> ProductExist(int id)
        {
            if (id.ToString() == String.Empty) return null;
            var result = await _unitOfWork.Products.GetById(id);
            return result;
        }
    }
}
