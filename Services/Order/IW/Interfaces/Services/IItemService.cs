using IW.Models.DTOs;
using IW.Models.DTOs.Item;
using IW.Models.DTOs.ItemDto;

namespace IW.Interfaces
{
    public interface IItemService
    {
        Task CreateItem(CreateItem input);
        Task<ItemDto> GetItem(int id);
        Task<IEnumerable<ItemDto>> GetItems(int offset, int amount);
        Task<IEnumerable<ItemDto>> GetItems(GetItem query, int offset , int amount );
        Task UpdateItem(int id, UpdateItem model);
        Task DeleteItem(int id);
    }
}
