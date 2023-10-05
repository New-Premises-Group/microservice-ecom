using IW.Models.DTOs;
using IW.Models.DTOs.Cart;

namespace IW.Interfaces.Services
{
    public interface ICartService
    {
        Task CreateCart(CreateCart input);

        Task<CartDto> GetCart(int id);

        Task<IEnumerable<CartDto>> GetCarts(int offset, int amount);

        Task<IEnumerable<CartDto>> GetCarts(GetCart query, int offset, int amount);

        Task UpdateCart(int id, UpdateCart model);

        Task DeleteCart(int id);
    }
}