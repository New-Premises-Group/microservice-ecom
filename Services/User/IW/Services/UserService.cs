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

        public void DeleteUser(int id)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetUser(int id)
        {
            throw new NotImplementedException();
        }

        public Task Login(CreateUser model)
        {
            throw new NotImplementedException();
        }

        public Task Logout()
        {
            throw new NotImplementedException();
        }

        public Task<User> UpdateUser(int id, User model)
        {
            throw new NotImplementedException();
        }
    }
}
