using IW.Common;
using IW.Interfaces.Repositories;
using IW.Models;
using IW.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace IW.Repositories
{
    internal class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        public OrderRepository(AppDbContext context) : base(context)
        {
        }

        public override async Task<IEnumerable<Order>> GetAll(int page, int amount)
        {
            return await dbSet.Include(u=>u.Items).AsNoTracking().Skip((page - 1) * amount).Take(amount).ToListAsync();
        }

        public override async Task<Order?> GetById (int id)
        {
            return await dbSet.Include(u=>u.Items).Where(o => o.Id==id).FirstAsync();
        }

        public override async Task<Order?> FindByCondition(Expression<Func<Order, bool>> expression)
        {
            return await dbSet.Where(expression).Include(u => u.Items).FirstOrDefaultAsync();
        }
    }
}
