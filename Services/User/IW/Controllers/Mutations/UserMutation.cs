using IW.Exceptions.CreateUserError;
using IW.Interfaces.Services;
using IW.Interfaces;
using IW.Models.DTOs.User;
using HotChocolate.Authorization;
using IW.Common;
using IW.Models.Entities;
using Mapster;

namespace IW.Controllers.Mutations
{
    [ExtendObjectType("Mutation")]
    public class UserMutation
    {
        [Error(typeof(CreateUserErrorFactory))]
        public async Task<UserCreatedPayload> LoginUser(CreateUser input, [Service] IUserService userService)
        {
            var payload = await userService.LogIn(input);
            payload.Message = "User successfully login";
            return payload;
        }

        public async Task<UserCreatedPayload> RenewToken(Guid id, [Service] IUserService userService)
        {
            var token = await userService.RenewToken(id);
            var payload = new UserCreatedPayload()
            {
                Message = "Renew token successful",
                ApiToken = token
            };
            return payload;
        }

        [Error(typeof(CreateUserErrorFactory))]
        [Authorize]
        public async Task<UserUpdatedPayload> UpdateUser(Guid id, UpdateUser input, [Service] IUserService userService)
        {
            await userService.UpdateUser(id, input);
            var payload = new UserUpdatedPayload()
            {
                Message = "User successfully updated",
            };
            return payload;
        }

        public async Task<UserUpdatedPayload> SetUserRole(Guid userId, int roleId, [Service] IUserService userService, [Service] IRoleService roleService)
        {
            var roleDto = await roleService.GetRole(roleId);
            var newToken = await userService.UpdateUserRole(userId, roleDto.Adapt<Role>());
            var payload = new UserUpdatedPayload()
            {
                Message = "User successfully updated"
            };
            return payload;
        }

        [Authorize(Roles = new[] { nameof(ROLE.Admin) })]
        public async Task<UserDeletedPayload> DeleteUser(Guid id, [Service] IUserService userService)
        {
            await userService.DeleteUser(id);
            var payload = new UserDeletedPayload()
            {
                Message = "User successfully deleted"
            };
            return payload;
        }
    }
}
