using IW.Models.DTOs.OrderDtos;

namespace IW.Interfaces.Services
{
    public interface IOrderService
    {
        Task<OrderDto> GetOrder(int id);
        Task<IEnumerable<OrderDto>> GetOrders(int page, int amount);
        Task<IEnumerable<OrderDto>> GetOrders(GetOrder query, int page, int amount);
        Task UpdateOrder(int id, UpdateOrder input);
        Task FinishOrder(int id);
        Task DeleteOrder(int id);
        Task<int> CreateOrder(CreateOrder input);
        Task<int> CreateGuestOrder(CreateGuestOrder input);
        Task<IEnumerable<OrderDto>> GetOrdersByStatus(GetOrder query, int page, int amount);
    }
}
