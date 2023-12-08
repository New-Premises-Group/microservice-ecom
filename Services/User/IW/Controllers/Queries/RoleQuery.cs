using HotChocolate.Authorization;
using IW.Common;
using IW.Interfaces.Services;
using IW.Models.DTOs.Role;

namespace IW.Controllers.Queries
{
    [ExtendObjectType("Query")]
    public class RoleQuery
    {
        [AllowAnonymous]
        public async Task<IEnumerable<RoleDto>> GetRoles([Service] IRoleService roleService)
        {
            var role = await roleService.GetRoles();
            return role;
        }

        [Authorize]
        public async Task<RoleDto> GetRole(int id, [Service] IRoleService roleService)
        {
            var role = await roleService.GetRole(id);
            return role;
        }
    }
}
