using IW.Common;
using IW.Interfaces;
using IW.Models;
using IW.Models.Entities;
using System.Linq.Expressions;

namespace IW.Repositories
{
    public class AddressRepository : GenericRepository<Address>,IAddressRepository
    {
        public AddressRepository(AppDbContext context):base(context)
        {
            
        }
    }
}
