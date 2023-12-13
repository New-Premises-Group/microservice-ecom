using IW.Common;
using IW.Interfaces;
using IW.Models;
using IW.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace IW.Repositories
{
    public class AddressRepository : GenericRepository<Address>, IAddressRepository
    {
        public AddressRepository(AppDbContext context) : base(context)
        {

        }

        public async Task SetDefaultAddress(int id, string userId)
        {
            //find if there is the id address
            var hasId = await _context
                .Addresses
                .Where(addr =>
               (addr.Id == id && addr.UserId.ToString().Equals(userId)) &&
                addr.IsDefault != true).CountAsync();
            if(hasId == 0)
            {
                return;
            }
            //find the user current default address
            await _context
              .Addresses
              .Where(addr =>
              addr.UserId.ToString().Equals(userId) &&
              addr.IsDefault == true)
              .ExecuteUpdateAsync(addr => addr
              .SetProperty(p =>
              p.IsDefault,
              p => false));
            //adjust new default address
            await _context
                .Addresses
                .Where(addr =>
               (addr.Id == id && addr.UserId.ToString().Equals(userId)) &&
                addr.IsDefault != true)
                .ExecuteUpdateAsync(addr => addr
                .SetProperty(p =>
                p.IsDefault,
                p => true));
        }

    }
}
