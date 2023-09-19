using IW.Exceptions.CreateUserError;
using IW.Interfaces;
using IW.Models.DTOs;
using IW.Models.Entities;

namespace IW.Controllers;

public class Query
{
    public async Task<User> GetUser(Guid id, [Service] IUserService userService)
    {
        var user=await userService.GetUser(id);
        return user;
    }
    public async Task<IEnumerable<User>> GetUsers([Service] IUserService userService)
    {
        var users= await userService.GetUsers();
        return users;
    }
}

//CREATE A BOOK
public class Mutation
{
    [Error(typeof(CreateUserErrorFactory))]
    public async Task<UserCreatedPayload> CreateUser(CreateUser input, [Service] IUserService userService)
    {
        await userService.CreateUser(input);
        var payload=new UserCreatedPayload()
        {
            Message="User successfully created",
        };
        return payload;
    }
    public async Task<UserUpdatedPayload> UpdateUser(Guid id,UpdateUser input, [Service] IUserService userService)
    {
        await userService.UpdateUser(id,input);
        var payload = new UserUpdatedPayload()
        {
            Message = "User successfully updated",
        };
        return payload;
    }

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
