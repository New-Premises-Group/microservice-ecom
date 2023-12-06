using IW.Common;
using IW.Interfaces;
using IW.Models;
using IW.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace IW.Repositories
{
    internal class DiscountRepository : GenericRepository<Discount>, IDiscountRepository
    {
        public DiscountRepository(AppDbContext context) : base(context)
        {
        }

        //public async Task ChangeAmount(string code,int amount)
        //{
        //    var p = await dbSet
        //        .Where(discount => discount.Code == code)
        //        .ExecuteUpdateAsync(u => u
        //        .SetProperty(property => property., ORDER_STATUS.Done));
        //}
    }
}
