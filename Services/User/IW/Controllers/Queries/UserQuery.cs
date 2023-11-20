using IW.Common;
using IW.Interfaces;
using IW.Models.DTOs.User;

namespace IW.Controllers.Queries
{
    [ExtendObjectType("Query")]
    public class UserQuery
    {
        public async Task<UserDto> GetUser(Guid id, [Service] IUserService userService)
        {
            var user = await userService.GetUser(id);
            return user;
        }

        public async Task<IEnumerable<UserDto>> GetUsers(int page,int amount,[Service] IUserService userService)
        {
            var users = await userService.GetUsers(amount, page);
            return users;
        }

        public async Task<IEnumerable<UserDto>> GetUsers(int page, int amount, GetUser query, [Service] IUserService userService)
        {
            var users = await userService.GetUsers(query, amount, page);
            return users;
        }
    }
}
