﻿using IW.Interfaces;
using IW.Models;
using Microsoft.EntityFrameworkCore;
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

        public virtual async Task<IEnumerable<TEntity>> FindByConditionToList(Expression<Func<TEntity, bool>> expression,int page, int amount)
        {
            IEnumerable<TEntity> results= await dbSet.AsNoTracking().Where(expression).Skip((page-1)*amount).Take(amount).ToListAsync();
            return results;
        }

        public virtual async Task<TEntity?> FindByCondition(Expression<Func<TEntity, bool>> expression)
        {
            return await dbSet.Where(expression).FirstOrDefaultAsync();
        }

        public virtual async Task<IEnumerable<TEntity>> GetAll(int page, int amount)
        {
            return await dbSet.AsNoTracking().Skip((page - 1) * amount).Take(amount).ToListAsync();
        }

        public virtual async Task<TEntity?> GetById<T>(T id)
        {
            return await dbSet.FindAsync(id);
        }

        public virtual async Task<TEntity?> GetById (int id)
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

    }
}
