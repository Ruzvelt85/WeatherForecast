using AutoMapper;
using WeatherForecastApi.Commands.Interfaces;
using WeatherForecastApi.CrosscuttingInfrastructure.Exceptions;
using WeatherForecastApi.Data.Interfaces;
using WeatherForecastApi.Domain;

namespace WeatherForecastApi.Commands
{
    public class CreateWeatherForecastCommandHandler : ICommandHandler<CreateWeatherForecastCommand, int>
    {
        private readonly IMapper _mapper;
        private readonly IWeatherForecastRepository _weatherForecastRepository;

        public CreateWeatherForecastCommandHandler(IMapper mapper, IWeatherForecastRepository weatherForecastRepository)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _weatherForecastRepository = weatherForecastRepository ?? throw new ArgumentNullException(nameof(weatherForecastRepository));
        }

        /// <inheritdoc />
        public async Task<int> HandleAsync(CreateWeatherForecastCommand command)
        {
            var forecastExists = await _weatherForecastRepository.ExistsAsync(forecast =>
                forecast.Date == command.Date.Date);

            if (forecastExists)
            {
                throw new ConflictException($"The weather forecast for date {command.Date.ToShortDateString()} already exists.");
            }

            var weatherForecastToCreate = _mapper.Map<WeatherForecast>(command);
            var createdWeatherForecast = await _weatherForecastRepository.AddAsync(weatherForecastToCreate);
            
            return createdWeatherForecast.Id;
        }
    }
}
