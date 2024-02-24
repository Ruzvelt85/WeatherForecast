using AutoMapper;
using WeatherForecastApi.Dto;

namespace WeatherForecastApi.Services.Mappings
{
    public class TemperatureValueToDescriptionConverter : ITypeConverter<int, WeatherDescription>
    {
        public WeatherDescription Convert(int source, WeatherDescription destination, ResolutionContext context)
        {
            if (source is >= -60 and < -30)
            {
                return WeatherDescription.Freezing;
            }
            else if (source is >= -30 and <= -10)
            {
                return WeatherDescription.Bracing;
            }
            else if (source is >= -10 and <= 10)
            {
                return WeatherDescription.Chilly;
            }
            else if (source is >= 10 and < 18)
            {
                return WeatherDescription.Cool;
            }
            else if (source is >= 18 and < 25)
            {
                return WeatherDescription.Mild;
            }
            else if (source is >= 25 and < 30)
            {
                return WeatherDescription.Warm;
            }
            else if (source is >= 30 and < 35)
            {
                return WeatherDescription.Balmy;
            }
            else if (source is >= 35 and < 40)
            {
                return WeatherDescription.Hot;
            }
            else if (source is >= 40 and < 45)
            {
                return WeatherDescription.Sweltering;
            }
            else if (source is >= 45 and <= 60)
            {
                return WeatherDescription.Scorching;
            }

            throw new ArgumentOutOfRangeException(nameof(source), source, null);
        }
    }
}
