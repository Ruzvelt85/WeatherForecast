using System.Linq.Expressions;

namespace WeatherForecastApi.Data.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        IList<TEntity> Find(Expression<Func<TEntity, bool>> predicate);

        Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate);

        Task<TEntity> AddAsync(TEntity entity);
    }
}
