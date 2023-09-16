using IW.Models.DTOs;
using IW.Models.Entities;

namespace IW.Interfaces
{
    public interface IUserService
    {
        Task Login(CreateUser model);
        Task Logout();
        Task<User?> Authenticate(string tokens);
        Task<User> GetUser(int id);
        Task<User> UpdateUser(int id, User model);
        void DeleteUser(int id);
    }
}
