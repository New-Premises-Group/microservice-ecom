using FluentValidation.Results;
using IW.Exceptions.CreateUserError;
using IW.Exceptions.ReadUserError;
using IW.Interfaces;
using IW.Models.DTOs;
using IW.Models.Entities;

namespace IW.Services
{
    public class UserService : IUserService
    {
        public IUnitOfWork _unitOfWork;
        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public Task<User?> Authenticate(string tokens)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteUser(Guid id)
        {
            var user = await UserExist(id);
            if (Equals(user, null)) throw new UserNotFoundException(id);

            _unitOfWork.Users.Remove(user);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<User> GetUser(Guid id)
        {
            var user=await UserExist(id);
            if(Equals(user, null))
            {
                throw new UserNotFoundException(id);
            }
            return user;
        }

        public async Task CreateUser(CreateUser model)
        {
            if(!Equals(model.Email, null)) {
                if (await EmailExist(model.Email)) throw new UserEmailTakenException(model.Email);
            }

            User newUser = new() {
                Name = model.Name,
                Email = model.Email,
                ImageURL = model.ImageURL,
                Token = model.Token,
                RoleId = 1
            };

            UserValidator validator = new UserValidator();
            validator.ValidateAndThrowException(newUser);

            _unitOfWork.Users.Add(newUser);
            await _unitOfWork.CompleteAsync();
        }

        public async Task UpdateToken(Guid id,string token)
        {
            var user = await UserExist(id);
            if (Equals(user,null)) throw new UserNotFoundException(id);

            user.Token = token;
            _unitOfWork.Users.Update(user);
            await _unitOfWork.CompleteAsync();
        }

        public async Task UpdateUser(Guid id, UpdateUser model)
        {
            var user=await UserExist(id);
            if (Equals(user,null)) throw new UserNotFoundException(id);
            if (!Equals(model.Email, null))
            {
                if (await EmailExist(model.Email)) throw new UserEmailTakenException(model.Email);
            }

            user.Name = model.Name?? user.Name;
            user.Email = model.Email ?? user.Email;
            user.ImageURL = model.ImageURL ?? user.ImageURL;
            user.Token = model.Token ?? user.Token;

            UserValidator validator = new UserValidator();
            validator.ValidateAndThrowException(user);

            _unitOfWork.Users.Update(user);
            await _unitOfWork.CompleteAsync();
        }

        private async Task<bool> EmailExist(string mail)
        {
            if(mail==String.Empty) return false;
            var results=await _unitOfWork.Users.FindByConditionToList(u => u.Email== mail);
            return results.Count() == 1;
        }
        private async Task<User?> UserExist(Guid id)
        {
            if (id.ToString() == String.Empty) return null;
            var user = await _unitOfWork.Users.GetById(id);
            return user;
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            var users=await _unitOfWork.Users.GetAll();
            return users;
        }
    }
}
