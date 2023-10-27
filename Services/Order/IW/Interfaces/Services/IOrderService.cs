using IW.Models.DTOs.OrderDto;

namespace IW.Interfaces.Services
{
    public interface IOrderService
    {
        Task<OrderDto> GetOrder(int id);
        Task<IEnumerable<OrderDto>> GetOrders(int offset, int amount);
        Task<IEnumerable<OrderDto>> GetOrders(GetOrder query, int offset, int amount);
        Task UpdateOrder(int id, UpdateOrder input);
        Task DeleteOrder(int id);
        Task<int> CreateOrder(CreateOrder input);
    }
}
