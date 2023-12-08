using IW.Common;
using IW.Interfaces;
using IW.Models;
using IW.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
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
                p=>p.SetProperty(u=>u.RoleId,role.Id));
        }

        public override async Task<User?> FindByCondition(Expression<Func<User, bool>> expression)
        {
            return await dbSet.Where(expression)
                .Include(u=>u.Role)
                .Include(u=>u.Addresses).FirstOrDefaultAsync();
        }

        public override async Task<User?> GetById <Guid>(Guid id)
        {
            return await dbSet.Where(u=>u.Id.Equals(id))
                .Include(u => u.Role)
                .Include(u => u.Addresses)
                .Include(u => u.LoyaltyPoint)
                .FirstAsync();
        }
        public override async Task<ICollection<User>> GetAll( int amount, int page)
        {
            return await dbSet
                .Include(u => u.Role)
                .Include(
                u => u.Addresses)
                .AsNoTracking()
                .Skip((page-1)*amount)
                .OrderBy(u=>u.Id)
                .Take(amount).ToListAsync();
        }
    }
}
