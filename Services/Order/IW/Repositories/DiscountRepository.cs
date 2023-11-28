using IW.Common;
using IW.Interfaces;
using IW.Models;
using IW.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace IW.Repositories
{
    internal class DiscountRepository : GenericRepository<Discount>, IDiscountRepository
    {
        public DiscountRepository(AppDbContext context) : base(context)
        {
        }
    }
}
