using System.Linq.Expressions;

namespace IW.Interfaces;

public interface IBaseRepository<TEntity> where TEntity : class
{
    Task<TEntity?> GetById(int id);
    Task<IEnumerable<TEntity>> GetAll();
    IQueryable<TEntity> FindByCondition(Expression<Func<TEntity, bool>> expression);
    void Update(TEntity entity);
    void Add(TEntity entity);
    void AddRange(IEnumerable<TEntity> entities);
    void Remove(TEntity entity);
    void RemoveRange(IEnumerable<TEntity> entities);
}
