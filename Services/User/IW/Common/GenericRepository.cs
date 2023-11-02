using IW.Interfaces;
using IW.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace IW.Common
{
    public abstract class GenericRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
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

        public void AddRange(ICollection<TEntity> entities)
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

        public virtual async Task<ICollection<TEntity>> GetAll(int offset, int amount)
        {
            return await dbSet.AsNoTracking().Skip(offset).Take(amount).ToListAsync();
        }

        public async Task<TEntity?>GetById<T>(T id)
        {
            return await dbSet.FindAsync(id);
        }

        public void Remove(TEntity entity)
        {
            dbSet.Remove(entity);
        }

        public void RemoveRange(ICollection<TEntity> entities)
        {
            dbSet.RemoveRange(entities);
        }

    }
}
