using IW.Common;
using IW.Interfaces.Repositories;
using IW.Models;
using IW.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace IW.Repositories
{
    public class RoleRepository : GenericRepository<Role>, IRoleRepository
    {
        public RoleRepository(AppDbContext context) : base(context)
        {
        }

        public override async Task<ICollection<Role>> GetAll(int page, int amount)
        {
            return await dbSet.AsNoTracking().ToListAsync();
        }
    }
}
