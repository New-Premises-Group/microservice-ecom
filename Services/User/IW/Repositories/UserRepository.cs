using IW.Common;
using IW.Interfaces;
using IW.Models;
using IW.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace IW.Repositories
{
    public class UserRepository:GenericRepository<User>,IUserRepository
    {
        public UserRepository(AppDbContext context):base(context)
        {
        }

        public async Task UpdateUserRole(Guid guid, Role role)
        {
            await _context.Users.Where(u=>u.Id==guid)
                .ExecuteUpdateAsync(
                p=>p.SetProperty(u=>u.Role, role)
                .SetProperty(u=>u.RoleId,role.Id));
        }

        public virtual async Task<User?> FindByCondition(Expression<Func<User, bool>> expression)
        {
            return await dbSet.Where(expression).Include(u=>u.Role).FirstOrDefaultAsync();
        }
    }
}
