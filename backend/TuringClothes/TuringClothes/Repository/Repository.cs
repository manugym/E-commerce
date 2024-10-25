
using Microsoft.EntityFrameworkCore;
using TuringClothes.Database;

namespace TuringClothes.Repository
{
    public abstract class Repository<TEntity, TId> : IRepository<TEntity, TId> where TEntity : class
    {
        protected MyDatabase _myDatabase { get; init; }

        public Repository(MyDatabase myDatabase)
        {
            _myDatabase = myDatabase;
        }

        public async Task<ICollection<TEntity>> GetAllAsync()
        {
            return await _myDatabase.Set<TEntity>().ToArrayAsync();
        }
        public Task<TEntity> DeleteAsync(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ExistAsync(TId id)
        {
            throw new NotImplementedException();
        }

        

        public Task<TEntity> GetByIdAsync(TId id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<TEntity> GetQueryable(bool asNoTracking = true)
        {
            DbSet<TEntity> entities = _myDatabase.Set<TEntity>();
            return asNoTracking ? entities.AsNoTracking() : entities;
        }

        public Task<TEntity> InsertAsync(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SaveAsync()
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> UpdateAsync(TEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
