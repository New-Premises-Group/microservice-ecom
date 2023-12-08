using IW.Interfaces;
using IW.Interfaces.Repositories;
using IW.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace IW.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        public IUserRepository Users { get; }

        public IRoleRepository Roles { get; }
        public IAddressRepository Addresses { get; }
        public IPointRepository Points { get; }

        public UnitOfWork(AppDbContext context, 
            IUserRepository userRepository,
            IRoleRepository roleRepository, 
            IAddressRepository addresses,
            IPointRepository pointRepository)
        {
            _context = context;
            Users = userRepository;
            Roles = roleRepository;
            Addresses = addresses;
            Points = pointRepository;
        }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }
    }
}
