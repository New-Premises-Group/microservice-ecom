using IW.Exceptions.CreateUserError;
using IW.Interfaces.Services;
using IW.Interfaces;
using IW.Models.DTOs.User;
using HotChocolate.Authorization;
using IW.Common;
using IW.Models.Entities;

namespace IW.Controllers.Mutations
{
    [ExtendObjectType("Mutation")]
    [Authorize]
    public class UserMutation
    {
        [Error(typeof(CreateUserErrorFactory))]
        [AllowAnonymous]
        public async Task<UserCreatedPayload> LoginUser(CreateUser input, [Service] IUserService userService)
        {
            var token = await userService.LogIn(input);
            var payload = new UserCreatedPayload()
            {
                Message = "User successfully created",
                ApiToken = token
            };
            return payload;
        }

        public async Task<UserCreatedPayload> RenewToken(CreateUser input, [Service] IUserService userService)
        {
            var token = await userService.RenewToken(input);
            var payload = new UserCreatedPayload()
            {
                Message = "Renew token successful",
                ApiToken = token
            };
            return payload;
        }

        [Error(typeof(CreateUserErrorFactory))]
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
            Role role = new()
            {
                Description = roleDto.Description,
                Id = roleId,
                Name = roleDto.Name
            };
            var newToken = await userService.UpdateUserRole(userId, role);
            var payload = new UserUpdatedPayload()
            {
                Message = "Renew token successful",
                ApiToken = newToken
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
