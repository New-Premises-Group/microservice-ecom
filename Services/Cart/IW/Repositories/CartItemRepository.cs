using IW.Common;
using IW.Interfaces.Repositories;
using IW.Models;
using IW.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace IW.Repositories
{
    internal class CartItemRepository : GenericRepository<CartItem>, ICartItemRepository
    {
        public CartItemRepository(AppDbContext context) : base(context)
        {
        }

        public override async Task<IEnumerable<CartItem>> GetAll(int offset, int amount)
        {
            return await dbSet.AsNoTracking().ToListAsync();
        }

        public override async Task<CartItem?> FindByCondition(Expression<Func<CartItem, bool>> expression)
        {
            return await dbSet.Where(expression).Include(u => u.Id).FirstOrDefaultAsync();
        }
    }
}