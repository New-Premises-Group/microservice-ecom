using IW.Common;
using IW.Interfaces;
using IW.Models;
using IW.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
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

        public override async Task<IEnumerable<Product>> FindByConditionToList(Expression<Func<Product, bool>> expression, int page, int amount)
        {
            IEnumerable<Product> results = await dbSet
                .AsNoTracking()
                .Where(expression)
                .Include(product => product.Category)
                .Include (product => product.Reviews)
                .Skip((page - 1) * amount)
                .Take(amount)
                .ToListAsync();
            return results;
        }

        public override async Task<IEnumerable<Product>> GetAll(int page, int amount)
        {
            return await dbSet
                .Include(u => u.Category)
                .Include(product => product.Reviews)
                .AsNoTracking()
                .Skip((page - 1) * amount)
                .Take(amount)
                .OrderBy(p => p.Id)
                .ToListAsync();
        }
        public override async Task<Product?> GetById (int id)
        {
            return await dbSet
                .Include(u => u.Category)
                .Include(product => product.Reviews)
                .Where(p=>p.Id==id)
                .FirstAsync();
        }

        public Product? GetByIdSync(int id)
        {
            return dbSet
                .Include(u => u.Category)
                .Include(product => product.Reviews)
                .Where(p => p.Id == id)
                .FirstOrDefault();
        }
    }
}
