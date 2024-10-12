using BANK.DataLayer.Data.Repositories.Interfaces;
using BANK.DataLayer.Entities;
using DataLayer.Data.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DataLayer.Data.Repositories.Realization
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : IEntity
    {
        private readonly BankDbContext _dbContext;

        public Repository(BankDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<TEntity> Create(TEntity entity)
        {
            var createdEntity = await _dbContext.Set<TEntity>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return createdEntity.Entity;
        }

        public async Task Delete(Guid Id)
        {
            var deleteEntity = _dbContext.Set<TEntity>().Find(Id);
            if (deleteEntity != null)
            {
                _dbContext.Set<TEntity>().Remove(deleteEntity);
            }
            await _dbContext.SaveChangesAsync();
        }

        public async Task<TEntity> Find(Guid Id)
        {
            return await _dbContext.Set<TEntity>().FindAsync(Id);
        }

        public IQueryable<TEntity> GetAll()
        {
            return _dbContext.Set<TEntity>();
        }

        public async Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> expression)
        {
            return await _dbContext.Set<TEntity>().Where(expression).ToListAsync();
        }

        public async Task<TEntity> Update(TEntity entity)
        {
            var updatedEntity = _dbContext.Set<TEntity>().Update(entity);
            await _dbContext.SaveChangesAsync();
            return updatedEntity.Entity;
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
