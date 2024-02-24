﻿namespace WeatherForecastApi.Data.Interfaces
{
    /// <summary>
    /// The only endpoint for saving data in EF Core context
    /// </summary>
    public interface IUnitOfWork
    {
        Task SaveAsync(CancellationToken cancellationToken = default);
    }
}
