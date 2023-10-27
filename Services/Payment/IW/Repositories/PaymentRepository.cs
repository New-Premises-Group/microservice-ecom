using IW.Common;
using IW.Interfaces.Repositories;
using IW.Models;
using IW.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace IW.Repositories
{
    internal class PaymentRepository : GenericRepository<Payment>, IPaymentRepository
    {
        public PaymentRepository(AppDbContext context) : base(context)
        {
        }

        public override async Task<IEnumerable<Payment>> GetAll(int offset, int amount)
        {
            return await dbSet.AsNoTracking().ToListAsync();
        }

        public override async Task<Payment?> FindByCondition(Expression<Func<Payment, bool>> expression)
        {
            return await dbSet.Where(expression).Include(u => u.ID).FirstOrDefaultAsync();
        }

    }
}