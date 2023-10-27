using IW.Exceptions.ReadRoleError;
using IW.Interfaces;
using IW.Interfaces.Services;
using IW.Models.DTOs.Role;
using IW.Models.Entities;
using Mapster;

namespace IW.Services
{
    public class RoleService : IRoleService
    {
        public readonly IUnitOfWork _unitOfWork;
        public RoleService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task CreateRole(CreateRole role)
        {
            Role newRole = role.Adapt<Role>();

            RoleValidator validator = new ();
            validator.ValidateAndThrowException(newRole);

            _unitOfWork.Roles.Add(newRole);
            await _unitOfWork.CompleteAsync();
        }

        public async Task DeleteRole(int id)
        {
            var role = await RoleExist(id);
            if (Equals(role, null)) throw new RoleNotFoundException(id);

            _unitOfWork.Roles.Remove(role);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<RoleDto> GetRole(int id)
        {
            var role = await RoleExist(id);
            if (Equals(role, null))
            {
                throw new RoleNotFoundException(id);
            }
            RoleDto result = new()
            {
                Id = id,
                Name = role.Name,
                Description = role.Description,
            };
            return result;
        }

        public async Task<IEnumerable<RoleDto>> GetRoles()
        {
            var roles = await _unitOfWork.Roles.GetAll(0,0);
            ICollection<RoleDto> result = new List<RoleDto>();
            foreach (var role in roles)
            {
                RoleDto item = new()
                {
                    Id = role.Id,
                    Name = role.Name,
                    Description=role.Description,
                };
                result.Add(item);
            }
            return result;
        }

        public async Task UpdateRole(int id, UpdateRole model)
        {
            var role = await RoleExist(id);
            if (Equals(role, null)) throw new RoleNotFoundException(id);

            role.Name = model.Name ?? role.Name;
            role.Description = model.Description ?? role.Description;

            RoleValidator validator = new ();
            validator.ValidateAndThrowException(role);

            _unitOfWork.Roles.Update(role);
            await _unitOfWork.CompleteAsync();
        }

        private async Task<Role?> RoleExist(int id)
        {
            if (id.ToString() == String.Empty) return null;
            var role = await _unitOfWork.Roles.GetById(id);
            return role;
        }
    }
}
