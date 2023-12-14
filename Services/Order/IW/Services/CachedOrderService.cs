using IW.Interfaces.Services;
using IW.Models;
using IW.Models.DTOs.OrderDtos;
using IW.Models.Entities;
using Mapster;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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
                    SlidingExpiration = TimeSpan.FromSeconds(5),
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
            order = await _orderService.GetOrder(id);
            return order;

            //if (string.IsNullOrEmpty(cachedOrder))
            //{
            //    order = await _orderService.GetOrder(id);
            //    if (order is null)
            //    {
            //        return order;
            //    }
            //    await _distributedCache.SetStringAsync(key,
            //        JsonConvert.SerializeObject(order),
            //        new DistributedCacheEntryOptions()
            //        {
            //            SlidingExpiration = TimeSpan.FromSeconds(5)
            //        });
            //    return order;
            //}

            //order = JsonConvert.DeserializeObject<OrderDto>(cachedOrder);

            //if (order is not null)
            //{
            //    _appDbContext.Set<Order>().Attach(order.Adapt<Order>());
            //}

            //return order;
        }

        public async Task<IEnumerable<OrderDto>> GetOrders(int offset, int amount)
        {
            IEnumerable<OrderDto> newProducts = await _orderService.GetOrders(offset, amount);
            return newProducts;

            //string key = CreateKey("all");

            //string? cachedProducts = await _distributedCache.GetStringAsync(key);

            //if (string.IsNullOrEmpty(cachedProducts))
            //{
            //    Console.WriteLine("Missed");
            //    IEnumerable<OrderDto> newProducts = await _orderService.GetOrders(offset, amount);

            //    await _distributedCache.SetStringAsync(key,
            //    JsonConvert.SerializeObject(newProducts),
            //    new DistributedCacheEntryOptions()
            //    {
            //        SlidingExpiration = TimeSpan.FromSeconds(5),
            //    });

            //    return newProducts;
            //}
            //Console.WriteLine("Hit");
            //var orders = JsonConvert.DeserializeObject<IEnumerable<OrderDto>>(cachedProducts);

            //return orders;
        }

        public async Task<IEnumerable<OrderDto>> GetOrders(GetOrder query, int offset, int amount)
        {
            IEnumerable<OrderDto> newProducts = await _orderService.GetOrders(query, offset, amount);
            return newProducts;
            //string key = "";
            //if (!string.IsNullOrEmpty(query.UserId.ToString()))
            //{
            //    key = CreateKey(query.UserId.ToString());
            //}
            //string? cachedProducts = await _distributedCache.GetStringAsync(key);

            //if (string.IsNullOrEmpty(cachedProducts))
            //{
            //    IEnumerable<OrderDto> newProducts = await _orderService.GetOrders(query, offset, amount);

            //    await _distributedCache.SetStringAsync(key,
            //    JsonConvert.SerializeObject(newProducts),
            //    new DistributedCacheEntryOptions()
            //    {
            //        SlidingExpiration = TimeSpan.FromSeconds(5),
            //    });

            //    return newProducts;
            //}
            //var orders = JsonConvert.DeserializeObject<IEnumerable<OrderDto>>(cachedProducts);

            //if (orders is not null)
            //{
            //    foreach (Order order in orders.Adapt<IEnumerable<Order>>())
            //    {
            //        _appDbContext.Set<Order>().Attach(order);
            //    }
            //}
            //return orders;
        }

        public async Task UpdateOrder(int id, UpdateOrder model)
        {
            await _orderService.UpdateOrder(id, model);
            //OrderDto order = _appDbContext
            //    .Set<Order>()
            //    .Entry(model.Adapt<Order>())
            //    .Properties
            //    .Select(e => e.CurrentValue)
            //    .Adapt<OrderDto>();

            //string key = CreateKey(order.Id.ToString());
            //await _distributedCache.SetStringAsync(key,
            //    JsonConvert.SerializeObject(order),
            //    new DistributedCacheEntryOptions()
            //    {
            //        SlidingExpiration = TimeSpan.FromSeconds(5),
            //    });
        }

        public async Task<int> CreateGuestOrder(CreateGuestOrder input)
        {
            var GuestEmail = await _orderService.CreateGuestOrder(input);

            string key = input.Email.ToString();
            await _distributedCache.SetStringAsync(key,
                JsonConvert.SerializeObject(input),
                new DistributedCacheEntryOptions()
                {
                    SlidingExpiration = TimeSpan.FromSeconds(5),
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

        public async Task FinishOrder(int id)
        {
            string oneProductKey = CreateKey(id);
            string multipleProductKey = CreateKey("all");

            Task finishOrder=_orderService.FinishOrder(id);
            Task updateCacheForOne = _distributedCache.RemoveAsync(oneProductKey);
            Task updateCacheForMany = _distributedCache.RemoveAsync(multipleProductKey);
            await Task.WhenAll(updateCacheForOne, updateCacheForMany, finishOrder);
        }
        //for now, no caching
        public async Task<IEnumerable<OrderDto>> GetOrdersByStatus(GetOrder query, int page, int amount)
        {
            IEnumerable<OrderDto> newProducts = await _orderService.GetOrdersByStatus(query, page, amount);
            return newProducts;
        }

        public async Task CancelOrder(int id)
        {
            await _orderService.CancelOrder(id);
        }
    }
}