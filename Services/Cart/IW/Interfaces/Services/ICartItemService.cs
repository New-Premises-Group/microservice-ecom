using IW.Models.DTOs.CartItemDto;

namespace IW.Interfaces.Services
{
    public interface ICartItemService
    {
        Task<CartItemDto> GetCartItem(int id);

        Task<IEnumerable<CartItemDto>> GetCartItems(int offset, int amount);

        Task<IEnumerable<CartItemDto>> GetCartItems(GetCartItem query, int offset, int amount);

        Task UpdateCartItem(int id, UpdateCartItem input);

        Task DeleteCartItem(int id);

        Task CreateCartItem(CreateCartItem input);
    }
}