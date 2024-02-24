using Microsoft.EntityFrameworkCore;
using WeatherForecastApi.Domain;

namespace WeatherForecastApi.Data
{
    public class InMemoryEfCoreContext : DbContext
    {
#nullable disable
        public DbSet<WeatherForecast> WeatherForecasts { get; set; }

#nullable restore

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: "WeatherForecastDb");
        }
    }
}
