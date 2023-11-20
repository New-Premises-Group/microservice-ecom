using IW.Interfaces;
using IW.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Linq.Expressions;

namespace IW.Common
{
    internal abstract class GenericRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        protected readonly AppDbContext _context;
        internal DbSet<TEntity> dbSet;
        public GenericRepository(AppDbContext context)
        {
            _context = context;
            dbSet = context.Set<TEntity>();
        }

        public void Add(TEntity entity)
        {
            dbSet.Add(entity);
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            dbSet.AddRange(entities);
        }

        public void Update(TEntity entity)
        {
            dbSet.Update(entity);
        }

        public async Task<ICollection<TEntity>> FindByConditionToList(Expression<Func<TEntity, bool>> expression,int offset, int amount)
        {
            ICollection<TEntity> results= await dbSet.AsNoTracking().Where(expression).Skip(offset).Take(amount).ToListAsync();
            return results;
        }

        public virtual async Task<TEntity?> FindByCondition(Expression<Func<TEntity, bool>> expression)
        {
            return await dbSet.Where(expression).FirstOrDefaultAsync();
        }

        public virtual async Task<IEnumerable<TEntity>> GetAll(int offset, int amount)
        {
            return await dbSet.AsNoTracking().Skip(offset).Take(amount).ToListAsync();
        }

        public virtual async Task<TEntity?>GetById<T>(T id)
        {
            return await dbSet.FindAsync(id);
        }

        public void Remove(TEntity entity)
        {
            dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            dbSet.RemoveRange(entities);
        }

        public void UpdateRange(ICollection<TEntity> entities)
        {
            dbSet.UpdateRange(entities);
        }

        public virtual void Upsert(TEntity entity)
        {
           
        }
    }
}
