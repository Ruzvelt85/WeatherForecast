using AutoMapper;
using WeatherForecastApi.Data.Interfaces;
using WeatherForecastApi.Dto;
using WeatherForecastApi.Queries.Interfaces;

namespace WeatherForecastApi.Queries
{
    public class GetWeatherForecastQueryHandler : IQueryHandler<GetWeatherForecastQuery, WeatherForecastListResponseDto>
    {
        private readonly IMapper _mapper;
        private readonly IWeatherForecastRepository _weatherForecastRepository;

        public GetWeatherForecastQueryHandler(IMapper mapper, IWeatherForecastRepository vehicleRepository)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _weatherForecastRepository = vehicleRepository ?? throw new ArgumentNullException(nameof(vehicleRepository));
        }

        public async Task<WeatherForecastListResponseDto> HandleAsync(GetWeatherForecastQuery query)
        {
            var forecasts = _weatherForecastRepository.Find(forecast =>
                forecast.Date >= DateTime.Now.Date && forecast.Date < DateTime.Now.AddDays(7).Date).OrderBy(forecast => forecast.Date);

            return await Task.FromResult(_mapper.Map<WeatherForecastListResponseDto>(forecasts));
        }
    }
}
