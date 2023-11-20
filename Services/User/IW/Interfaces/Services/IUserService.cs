using IW.Models.DTOs.User;
using IW.Models.Entities;
using RestSharp;

namespace IW.Interfaces
{
    public interface IUserService
    {
        Task<UserCreatedPayload> LogIn(CreateUser model);
        Task<string> RenewToken(Guid id);
        Task<string> UpdateUserRole(Guid userId,Role role);
        Task<UserDto> GetUser(Guid id);
        Task<IEnumerable<UserDto>> GetUsers( int amount, int page);
        Task<IEnumerable<UserDto>> GetUsers(GetUser query, int amount, int page);
        Task UpdateUser(Guid id, UpdateUser model);
        Task DeleteUser(Guid id);
    }
}
