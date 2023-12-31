﻿using IW.Common;
using IW.Exceptions.CreateUserError;
using IW.Exceptions.ReadUserError;
using IW.Interfaces;
using IW.Models.DTOs.User;
using IW.Models.Entities;
using Mapster;

namespace IW.Services
{
    public class UserService : IUserService
    {
        public readonly IUnitOfWork _unitOfWork;
        private readonly IJwtProvider _jwtProvider;
        public UserService(IUnitOfWork unitOfWork, IJwtProvider jwtProvider)
        {
            _unitOfWork = unitOfWork;
            _jwtProvider = jwtProvider;
        }

        public async Task DeleteUser(Guid id)
        {
            var user = await UserExist(id);
            if (Equals(user, null)) throw new UserNotFoundException(id);

            _unitOfWork.Users.Remove(user);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<UserDto> GetUser(Guid id)
        {
            var user=await UserExist(id);
            if(Equals(user, null))
            {
                throw new UserNotFoundException(id);
            }
            UserDto result = user.Adapt<UserDto>();
            return result;
        }

        public async Task<UserCreatedPayload> LogIn(CreateUser model)
        {
            var user = await _unitOfWork.Users.FindByCondition(u => u.Name == model.Name && u.Email==model.Email);
            Role? role = !Equals(user,null)? user.Role: await _unitOfWork.Roles.GetById((int)ROLE.User); 

            if (Equals(user, null))
            {
                User newUser = model.Adapt<User>();
                newUser.RoleId = role.Id;
                newUser.Role = role;

                UserValidator validator = new();
                validator.ValidateAndThrowException(newUser);

                _unitOfWork.Users.Add(newUser);
                await _unitOfWork.CompleteAsync();
                string newToken = _jwtProvider.Generate(newUser);
                return new UserCreatedPayload
                {
                    ApiToken = newToken,
                    UserId = newUser.Id.ToString(),
                };
            }

            string token = _jwtProvider.Generate(user);
            return new UserCreatedPayload
            {
                ApiToken = token,
                UserId = user.Id.ToString(),
            };
        }

        public async Task<string> RenewToken(Guid id)
        {
            var user = await UserExist(id);

            string token = _jwtProvider.Generate(user);
            return token;
        }

        public async Task UpdateUser(Guid id, UpdateUser model)
        {
            User? user=await UserExist(id);
            if (Equals(user,null)) throw new UserNotFoundException(id);
            if (!Equals(model.Email, null))
            {
                if (await EmailExist(model.Email)) throw new UserEmailTakenException(model.Email);
            }

            user.Name = model.Name?? user.Name;
            user.Email = model.Email ?? user.Email;
            user.ImageURL = model.ImageURL ?? user.ImageURL;
            user.Token = model.Token ?? user.Token;
            user.PhoneNumber= model.PhoneNumber ?? user.PhoneNumber;

            UserValidator validator = new UserValidator();
            validator.ValidateAndThrowException(user);

            _unitOfWork.Users.Update(user);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<string> UpdateUserRole(Guid userId, Role role)
        {
            User? user = await UserExist(userId);
            if (Equals(user, null)) throw new UserNotFoundException(userId);

            await _unitOfWork.Users.UpdateUserRole(userId, role);
            user.Role = role;
            user.RoleId=role.Id;
            string token = _jwtProvider.Generate(user);
            return token;
        }

        private async Task<bool> EmailExist(string mail)
        {
            if(mail==String.Empty) return false;
            var results=await _unitOfWork.Users.FindByConditionToList(u => u.Email== mail,0,1);
            return results.Count() == 1;
        }
        private async Task<User?> UserExist(Guid id)
        {
            if (id.ToString() == String.Empty) return null;
            var user = await _unitOfWork.Users.GetById(id);
            return user;
        }

        public async Task<IEnumerable<UserDto>> GetUsers(int page = ((int)PAGINATING.OffsetDefault), int amount = ((int)PAGINATING.AmountDefault))
        {
            ICollection<User> users =await _unitOfWork.Users.GetAll(page, amount);
            ICollection<UserDto> result = users.Adapt<ICollection<UserDto>>();
            
            return result;
        }

        public async Task<IEnumerable<UserDto>> GetUsers(GetUser query, int page=((int)PAGINATING.OffsetDefault),int amount=((int)PAGINATING.AmountDefault))
        {
            var users = await _unitOfWork.Users.FindByConditionToList(
                u=>u.Name==query.Name ||
                u.Email == query.Email
                , page,amount);
            ICollection<UserDto> result = users.Adapt<ICollection<UserDto>>();

            return result;
        }
    }
}
