using WeatherForecastApi.Commands.Interfaces;

namespace WeatherForecastApi.Commands
{
    public record CreateWeatherForecastCommand(DateTime Date, int Value) : ICommand<int>;
}
