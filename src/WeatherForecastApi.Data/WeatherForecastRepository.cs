using WeatherForecastApi.Data.Interfaces;
using WeatherForecastApi.Domain;

namespace WeatherForecastApi.Data
{
    public class WeatherForecastRepository : BaseRepository<WeatherForecast>, IWeatherForecastRepository
    {
        public WeatherForecastRepository(InMemoryEfCoreContext context) : base(context)
        {
        }
    }
}
