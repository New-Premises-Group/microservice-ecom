using HotChocolate.Authorization;
using IW.Common;
using IW.Exceptions.CreateUserError;
using IW.Interfaces;
using IW.Interfaces.Services;
using IW.Models.DTOs.Role;
using IW.Models.DTOs.User;
using IW.Models.Entities;

namespace IW.Controllers;

public class Query
{
    public async Task<UserDto> GetUser(Guid id, [Service] IUserService userService)
    {
        var user=await userService.GetUser(id);
        return user;
    }

    public async Task<IEnumerable<UserDto>> GetUsers([Service] IUserService userService)
    {
        var users= await userService.GetUsers(((int)PAGINATING.OffsetDefault),((int)PAGINATING.AmountDefault));
        return users;
    }

    public async Task<IEnumerable<UserDto>> GetUsers(GetUser query, [Service] IUserService userService)
    {
        var users = await userService.GetUsers(query,((int)PAGINATING.OffsetDefault), ((int)PAGINATING.AmountDefault));
        return users;
    }

    [Authorize(Roles = new[] { nameof(ROLE.Admin) })]
    public async Task<IEnumerable<RoleDto>> GetRoles([Service] IRoleService roleService)
    {
        var role = await roleService.GetRoles();
        return role;
    }

    [Authorize]
    public async Task<RoleDto> GetRole(int id,[Service] IRoleService roleService)
    {
        var role = await roleService.GetRole(id);
        return role;
    }
}

[Authorize]
public class Mutation
{
    //-----------User--------------
    [Error(typeof(CreateUserErrorFactory))]
    [AllowAnonymous]
    public async Task<UserCreatedPayload> LoginUser(CreateUser input, [Service] IUserService userService)
    {
        var token=await userService.LogIn(input);
        var payload=new UserCreatedPayload()
        {
            Message="User successfully created",
            ApiToken=token
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
    public async Task<UserUpdatedPayload> UpdateUser(Guid id,UpdateUser input, [Service] IUserService userService)
    {
        await userService.UpdateUser(id,input);
        var payload = new UserUpdatedPayload()
        {
            Message = "User successfully updated",
        };
        return payload;
    }

    public async Task<UserUpdatedPayload> SetUserRole(Guid userId,int roleId,IUserService userService,IRoleService roleService)
    {
        var roleDto=await roleService.GetRole(roleId);
        Role role = new()
        {
            Description = roleDto.Description,
            Id = roleId,
            Name = roleDto.Name
        };
        var newToken=await userService.UpdateUserRole(userId, role);
        var payload = new UserUpdatedPayload()
        {
            Message = "Renew token successful",
            ApiToken = newToken
        };
        return payload;
    }

    [Authorize(Roles=new[] { nameof(ROLE.Admin) })]
    public async Task<UserDeletedPayload> DeleteUser(Guid id, [Service] IUserService userService)
    {
        await userService.DeleteUser(id);
        var payload = new UserDeletedPayload()
        {
            Message = "User successfully deleted"
        };
        return payload;
    }

    //-----------Role--------------
    [Authorize(Roles = new[] { nameof(ROLE.Admin) })]
    public async Task<RoleCreatedPayload> CreateRole(CreateRole role, [Service] IRoleService roleService)
    {
        await roleService.CreateRole(role);
        var payload = new RoleCreatedPayload()
        {
            Message = "Role successfully created"
        };
        return payload;
    }

    [Authorize(Roles = new[] { nameof(ROLE.Admin) })]
    public async Task<RoleCreatedPayload> UpdateRole(int id,UpdateRole input, [Service] IRoleService  roleService)
    {
        await roleService.UpdateRole(id,input);
        var payload = new RoleCreatedPayload()
        {
            Message = "Role successfully updated"
        };
        return payload;
    }
}
