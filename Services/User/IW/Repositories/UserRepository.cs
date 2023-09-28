using IW.Common;
using IW.Interfaces;
using IW.Models;
using IW.Models.Entities;

namespace IW.Repositories
{
    public class UserRepository:GenericRepository<User>,IUserRepository
    {
        public UserRepository(AppDbContext context):base(context)
        {
        }
    }
}
