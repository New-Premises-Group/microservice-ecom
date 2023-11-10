using IW.Common;
using IW.Interfaces;
using IW.Models;
using IW.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
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
