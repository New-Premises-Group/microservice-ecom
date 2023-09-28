using HotChocolate.Authorization;
using IW.Common;
using IW.Exceptions.CreateRoleError;
using IW.Interfaces.Services;
using IW.Models.DTOs.Role;
using IW.Models.DTOs.User;

namespace IW.Controllers.Mutations
{
    [ExtendObjectType("Mutation")]
    [Authorize(Roles = new[] { nameof(ROLE.Admin) })]
    public class RoleMutation
    {
        [Error(typeof(CreateRoleErrorFactory))]
        public async Task<RoleCreatedPayload> CreateRole(CreateRole role, [Service] IRoleService roleService)
        {
            await roleService.CreateRole(role);
            var payload = new RoleCreatedPayload()
            {
                Message = "Role successfully created"
            };
            return payload;
        }

        [Error(typeof(CreateRoleErrorFactory))]
        public async Task<RoleCreatedPayload> UpdateRole(int id, UpdateRole input, [Service] IRoleService roleService)
        {
            await roleService.UpdateRole(id, input);
            var payload = new RoleCreatedPayload()
            {
                Message = "Role successfully updated"
            };
            return payload;
        }
    }
}
