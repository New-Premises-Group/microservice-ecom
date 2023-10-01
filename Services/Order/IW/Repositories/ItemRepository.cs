using IW.Common;
using IW.Interfaces;
using IW.Models;
using IW.Models.Entities;

namespace IW.Repositories
{
    internal class ItemRepository:GenericRepository<OrderItem>,IItemRepository
    {
        public ItemRepository(AppDbContext context):base(context)
        {
        }

        //public override async Task<OrderItem?> FindByCondition(Expression<Func<OrderItem, bool>> expression)
        //{
        //    return await dbSet.Where(expression).Include(u=>u.Category).FirstOrDefaultAsync();
        //}
    }
}
