using IW.Models.DTOs;
using IW.Models.DTOs.Item;
using IW.Models.DTOs.ItemDto;

namespace IW.Interfaces
{
    public interface IItemService
    {
        Task CreateItem(int orderId, CreateItem input);
        Task CreateItems(int orderId,IEnumerable<CreateItem> items);
        Task<ItemDto> GetItem(int id);
        Task<IEnumerable<ItemDto>> GetItems(int page, int amount);
        Task<IEnumerable<ItemDto>> GetItems(GetItem query, int page , int amount );
        Task UpdateItem(int id, UpdateItem model);
        Task DeleteItem(int id);
    }
}
