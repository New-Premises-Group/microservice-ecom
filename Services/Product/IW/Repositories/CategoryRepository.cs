using IW.Common;
using IW.Interfaces.Repositories;
using IW.Models;
using IW.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace IW.Repositories
{
    internal class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext context) : base(context)
        {
        }

        public override async Task<IEnumerable<Category>> GetAll(int page, int amount)
        {
            return await dbSet.AsNoTracking().Skip((page - 1) * amount).Take(amount).ToListAsync();
        }
    }
}
