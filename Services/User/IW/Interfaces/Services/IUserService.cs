using IW.Models.DTOs;
using IW.Models.Entities;

namespace IW.Interfaces
{
    public interface IUserService
    {
        Task CreateUser(CreateUser model);
        Task UpdateToken(Guid id, string token);
        Task<User?> Authenticate(string tokens);
        Task<User> GetUser(Guid id);
        Task<IEnumerable<User>> GetUsers();
        Task UpdateUser(Guid id, UpdateUser model);
        Task DeleteUser(Guid id);
    }
}
