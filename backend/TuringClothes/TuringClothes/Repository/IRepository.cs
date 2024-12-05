﻿namespace TuringClothes.Repository
{
    public interface IRepository<TEntity, TId> where TEntity : class
    {
        Task<ICollection<TEntity>> GetAllAsync();
        IQueryable<TEntity> GetQueryable(bool asNoTracking = true);
        Task<TEntity>GetByIdAsync(TId id);
        Task<TEntity>InsertAsync(TEntity entity);
        Task<TEntity>UpdateAsync(TEntity entity);
        Task<TEntity>DeleteAsync(TEntity entity);
        Task<bool> ExistAsync(TId id);
    }
}
