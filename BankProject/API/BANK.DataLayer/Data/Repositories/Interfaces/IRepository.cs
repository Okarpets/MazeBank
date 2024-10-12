using BANK.DataLayer.Entities;
using System.Linq.Expressions;

namespace BANK.DataLayer.Data.Repositories.Interfaces;

public interface IRepository<TEntity> where TEntity : IEntity
{
    public Task<TEntity> Create(TEntity entity);

    public Task SaveChangesAsync();

    public Task Delete(Guid Id);

    public Task<TEntity> Find(Guid Id);

    public IQueryable<TEntity> GetAll();

    public Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> expression);

    public Task<TEntity> Update(TEntity entity);
}
