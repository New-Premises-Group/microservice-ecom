using IW.Common;
using IW.Interfaces.Repositories;
using IW.Models;
using IW.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace IW.Repositories
{
    internal class TransactionRepository : GenericRepository<Transaction>, ITransactionRepository
    {
        public TransactionRepository(AppDbContext context) : base(context)
        {
        }

        public override async Task<IEnumerable<Transaction>> GetAll(int offset, int amount)
        {
            return await dbSet.AsNoTracking().ToListAsync();
        }

        public override async Task<Transaction?> FindByCondition(Expression<Func<Transaction, bool>> expression)
        {
            return await dbSet.Where(expression).Include(u => u.Inventory).FirstOrDefaultAsync();
        }
    }
}
