using IW.Models.DTOs.User;
using IW.Models.Entities;
using RestSharp;

namespace IW.Interfaces
{
    public interface IUserService
    {
        Task<string> LogIn(CreateUser model);
        Task<string> RenewToken(CreateUser model);
        Task<string> UpdateUserRole(Guid userId,Role role);
        Task<UserDto> GetUser(Guid id);
        Task<IEnumerable<UserDto>> GetUsers(int offset, int amount);
        Task<IEnumerable<UserDto>> GetUsers(GetUser query, int offset , int amount );
        Task UpdateUser(Guid id, UpdateUser model);
        Task DeleteUser(Guid id);
    }
}
