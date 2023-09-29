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

        public async Task<IEnumerable<UserDto>> GetUsers([Service] IUserService userService)
        {
            var users = await userService.GetUsers(((int)PAGINATING.OffsetDefault), ((int)PAGINATING.AmountDefault));
            return users;
        }

        public async Task<IEnumerable<UserDto>> GetUsers(GetUser query, [Service] IUserService userService)
        {
            var users = await userService.GetUsers(query, ((int)PAGINATING.OffsetDefault), ((int)PAGINATING.AmountDefault));
            return users;
        }
    }
}
