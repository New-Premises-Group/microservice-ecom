using IW.Models.Entities;

namespace IW.Interfaces.Repositories
{
    public interface IOrderRepository : IBaseRepository<Order>
    {
        Task<Order?> GetById (int id);
    }
}
