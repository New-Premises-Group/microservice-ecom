using IW.Common;
using IW.Interfaces;
using IW.Models;
using IW.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace IW.Repositories
{
    public class AddressRepository : GenericRepository<Address>,IAddressRepository
    {
        public AddressRepository(AppDbContext context):base(context)
        {
            
        }

        public async Task SetDefaultAddress(int id)
        {
            await _context
                .Addresses
                .Where(addr => 
                addr.Id == id || 
                addr.IsDefault == true)
                .ExecuteUpdateAsync(addr => addr
                .SetProperty(p => 
                p.IsDefault, 
                p=>!p.IsDefault));
        }

    }
}
