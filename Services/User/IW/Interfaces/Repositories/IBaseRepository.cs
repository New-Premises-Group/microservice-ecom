using IW.Models.Entities;
using System.Linq.Expressions;

namespace IW.Interfaces;

public interface IBaseRepository<TEntity> where TEntity : class
{
    Task<TEntity?> GetById<T>(T id);
    Task<ICollection<TEntity>> GetAll(int offset, int amount);
    Task<ICollection<TEntity>> FindByConditionToList(Expression<Func<TEntity, bool>> expression, int offset , int amount );
    Task<TEntity?> FindByCondition(Expression<Func<TEntity, bool>> expression);
    void Update(TEntity entity);
    void Add(TEntity entity);
    void AddRange(ICollection<TEntity> entities);
    void Remove(TEntity entity);
    void RemoveRange(ICollection<TEntity> entities);
}
