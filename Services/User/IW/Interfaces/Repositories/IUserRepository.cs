using IW.Models.Entities;

namespace IW.Interfaces
{
    public interface IUserRepository:IBaseRepository<User>
    {
        Task UpdateUserRole(Guid guid, Role role);
    }
}
