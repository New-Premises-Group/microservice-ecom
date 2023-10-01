using IW.Common;
using IW.Interfaces;
using IW.Models;
using IW.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace IW.Repositories
{
    internal class ProductRepository:GenericRepository<Product>,IProductRepository
    {
        public ProductRepository(AppDbContext context):base(context)
        {
        }

        public override async Task<Product?> FindByCondition(Expression<Func<Product, bool>> expression)
        {
            return await dbSet.Where(expression).Include(u=>u.Category).FirstOrDefaultAsync();
        }
    }
}
