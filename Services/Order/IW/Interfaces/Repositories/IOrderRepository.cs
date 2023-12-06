using IW.Models.Entities;

namespace IW.Interfaces.Repositories
{
    public interface IOrderRepository : IBaseRepository<Order>
    {
        Task SetDone(int id);
        Task<Order> SetCancel(int id);
    }
}
