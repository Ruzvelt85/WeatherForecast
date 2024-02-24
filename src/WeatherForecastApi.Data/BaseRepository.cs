using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using WeatherForecastApi.Data.Interfaces;
using WeatherForecastApi.Domain;

namespace WeatherForecastApi.Data
{
    /// <summary>
    /// Base implementation of pattern Repository
    /// </summary>
    public abstract class BaseRepository<TEntity> : IRepository<TEntity>
        where TEntity : class, IEntity
    {
        protected readonly InMemoryEfCoreContext Context;

        protected BaseRepository(InMemoryEfCoreContext context)
        {
            Context = context;
        }

        protected IQueryable<TEntity> DbSet => Context.Set<TEntity>();

        public virtual IList<TEntity> Find(Expression<Func<TEntity, bool>> predicate) => DbSet.Where(predicate).ToList();

        public virtual Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate) =>
            DbSet.AnyAsync(predicate);

        public virtual async Task<TEntity> AddAsync(TEntity entity) =>
            (await Context.Set<TEntity>().AddAsync(entity)).Entity;
    }
}
