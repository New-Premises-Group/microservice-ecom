﻿using IW.Models.Entities;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;

namespace IW.Interfaces;

public interface IBaseRepository<TEntity> where TEntity : class
{
    Task<TEntity?> GetById<T>(T id);
    Task<TEntity?> GetById(int id);
    Task<IEnumerable<TEntity>> GetAll(int page, int amount);
    Task<IEnumerable<TEntity>> FindByConditionToList(
        Expression<Func<TEntity, bool>> expression, int page , int amount );
    public IQueryable<TEntity> FindByConditionToQuery(
        Expression<Func<TEntity, bool>> expression, int page, int amount);
    Task<TEntity?> FindByCondition(Expression<Func<TEntity, bool>> expression);
    void Update(TEntity entity);
    void Add(TEntity entity);
    void AddRange(IEnumerable<TEntity> entities);
    void Remove(TEntity entity);
    void RemoveRange(IEnumerable<TEntity> entities);
}
