using IW.Interfaces;
using IW.Interfaces.Repositories;
using IW.Models;
using Microsoft.EntityFrameworkCore.Storage;

namespace IW.Repository
{
    internal class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public IInventoryRepository Inventories { get; }

        public ITransactionRepository Transactions { get; }

        public UnitOfWork(AppDbContext context,IInventoryRepository inventoryRepository, ITransactionRepository transactionRepository)
        {
            _context = context;
            Inventories= inventoryRepository;
            Transactions= transactionRepository;
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

        /// <summary>
        /// Create a database transaction instance
        /// </summary>
        /// <returns>IDbContextTransaction</returns>
        public IDbContextTransaction StartTrasaction()
        {
            return _context.Database.BeginTransaction();
        }

        /// <summary>
        /// Commit the transactino passed in 
        /// </summary>
        /// <param name="trans"></param>
        public static void Commit(IDbContextTransaction trans)
        {
            trans.Commit();
        }

        /// <summary>
        /// Rollback the transactino passed in. Use when you have 
        /// uncaught db exception or when excuting buisness logic.
        /// </summary>
        /// <param name="trans"></param>
        /// <returns></returns>
        public async static Task Rollback(IDbContextTransaction trans)
        {
            await trans.RollbackAsync();
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
