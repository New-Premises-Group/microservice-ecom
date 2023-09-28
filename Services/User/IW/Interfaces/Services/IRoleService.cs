using IW.Models.DTOs.Role;

namespace IW.Interfaces.Services
{
    public interface IRoleService
    {
        Task<RoleDto> GetRole(int id);
        Task<IEnumerable<RoleDto>> GetRoles();
        Task UpdateRole(int id, UpdateRole model);
        Task DeleteRole(int id);
        Task CreateRole(CreateRole role);
    }
}
