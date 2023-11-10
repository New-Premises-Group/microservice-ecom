using IW.Interfaces;
using IW.Models;
using IW.Models.DTOs;
using IW.Models.DTOs.Item;
using IW.Models.DTOs.ItemDtos;
using Microsoft.Extensions.Caching.Distributed;

namespace IW.Services
{
    public class CachedItemService : IItemService
    {
        private readonly ItemService _itemService;
        private readonly IDistributedCache _distributedCache;
        private readonly AppDbContext _appDbContext;

        public CachedItemService(ItemService itemService, IDistributedCache distributedCache, AppDbContext appDbContext)
        {
            _itemService = itemService;
            _distributedCache = distributedCache;
            _appDbContext = appDbContext;
        }

        public async Task CreateItem(int orderId, CreateItem input)
        {
            throw new NotImplementedException();
        }

        public Task CreateItems(int orderId, IEnumerable<CreateItem> items)
        {
            throw new NotImplementedException();
        }

        public Task DeleteItem(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ItemDto> GetItem(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ItemDto>> GetItems(int offset, int amount)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ItemDto>> GetItems(GetItem query, int offset, int amount)
        {
            throw new NotImplementedException();
        }

        public Task UpdateItem(int id, UpdateItem model)
        {
            throw new NotImplementedException();
        }
    }
}