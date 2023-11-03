using IW.Interfaces.Services;
using IW.Models;
using IW.Models.DTOs.OrderDto;
using IW.Models.Entities;
using Mapster;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace IW.Services
{
    public class CachedOrderService : IOrderService
    {
        private readonly OrderService _orderService;
        private readonly IDistributedCache _distributedCache;
        private readonly AppDbContext _appDbContext;

        public CachedOrderService(OrderService orderService, IDistributedCache distributedCache, AppDbContext appDbContext)
        {
            _orderService = orderService;
            _distributedCache = distributedCache;
            _appDbContext = appDbContext;
        }

        public async Task<int> CreateOrder(CreateOrder input)
        {
            var orderId = await _orderService.CreateOrder(input);

            string key = input.UserId.ToString();
            await _distributedCache.SetStringAsync(key,
                JsonConvert.SerializeObject(input),
                new DistributedCacheEntryOptions()
                {
                    SlidingExpiration = TimeSpan.FromSeconds(30),
                });
            return orderId;
        }

        public async Task DeleteOrder(int id)
        {
            string key = CreateKey(id);
            Task deleteProduct = _orderService.DeleteOrder(id);
            Task updateCache = _distributedCache.RemoveAsync(key);
            await Task.WhenAll(updateCache, deleteProduct);
        }

        public async Task<OrderDto> GetOrder(int id)
        {
            string key = CreateKey(id);
            string? cachedOrder = await _distributedCache.GetStringAsync(key);

            OrderDto? order;
            if (string.IsNullOrEmpty(cachedOrder))
            {
                order = await _orderService.GetOrder(id);
                if (order is null)
                {
                    return order;
                }
                await _distributedCache.SetStringAsync(key,
                    JsonConvert.SerializeObject(order),
                    new DistributedCacheEntryOptions()
                    {
                        SlidingExpiration = TimeSpan.FromSeconds(30)
                    });
                return order;
            }

            order = JsonConvert.DeserializeObject<OrderDto>(cachedOrder);

            if (order is not null)
            {
                _appDbContext.Set<Order>().Attach(order.Adapt<Order>());
            }

            return order;
        }

        public async Task<IEnumerable<OrderDto>> GetOrders(int offset, int amount)
        {
            return await _orderService.GetOrders(offset, amount);
        }

        public async Task<IEnumerable<OrderDto>> GetOrders(GetOrder query, int offset, int amount)
        {
            string key = "";
            if (!string.IsNullOrEmpty(query.UserId.ToString()))
            {
                key = CreateKey(query.UserId.ToString());
            }
            string? cachedProducts = await _distributedCache.GetStringAsync(key);

            if (string.IsNullOrEmpty(cachedProducts))
            {
                IEnumerable<OrderDto> newProducts = await _orderService.GetOrders(query, offset, amount);

                return newProducts;
            }
            var orders = JsonConvert.DeserializeObject<IEnumerable<OrderDto>>(cachedProducts);

            if (orders is not null)
            {
                foreach (Order order in orders.Adapt<IEnumerable<Order>>())
                {
                    _appDbContext.Set<Order>().Attach(order);
                }
            }
            return orders;
        }

        public async Task UpdateOrder(int id, UpdateOrder model)
        {
            await _orderService.UpdateOrder(id, model);
            OrderDto order = _appDbContext
                .Set<Order>()
                .Entry(model.Adapt<Order>())
                .Properties
                .Select(e => e.CurrentValue)
                .Adapt<OrderDto>();

            string key = CreateKey(order.Id.ToString());
            await _distributedCache.SetStringAsync(key,
                JsonConvert.SerializeObject(order),
                new DistributedCacheEntryOptions()
                {
                    SlidingExpiration = TimeSpan.FromSeconds(30),
                });
        }

        public async Task<int> CreateGuestOrder(CreateGuestOrder input)
        {
            var GuestEmail = await _orderService.CreateGuestOrder(input);

            string key = input.Email.ToString();
            await _distributedCache.SetStringAsync(key,
                JsonConvert.SerializeObject(input),
                new DistributedCacheEntryOptions()
                {
                    SlidingExpiration = TimeSpan.FromSeconds(30),
                });
            return GuestEmail;
        }

        /// <summary>
        /// Create a string key for the key - value pair store values in Redis.
        /// </summary>
        /// <param name="id">Id of the product</param>
        /// <returns>String key to identify in redis</returns>
        private static string CreateKey(int id)
        {
            return $"order-{id}";
        }

        private static string CreateKey(string key)
        {
            return $"order-{key}";
        }
    }
}