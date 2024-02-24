using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WeatherForecastApi.Commands;
using WeatherForecastApi.Commands.Interfaces;
using WeatherForecastApi.Dto;
using WeatherForecastApi.Queries;
using WeatherForecastApi.Queries.Interfaces;
using WeatherForecastApi.WebApi.Filters;

namespace WeatherForecastApi.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IQueryHandler<GetWeatherForecastQuery, WeatherForecastListResponseDto> _getWeatherForecastQueryHandler;
        private readonly ICommandHandler<CreateWeatherForecastCommand, int> _createWeatherForecastCommandHandler;

        public WeatherForecastController(IMapper mapper, IQueryHandler<GetWeatherForecastQuery, WeatherForecastListResponseDto> getWeatherForecastQueryHandler, ICommandHandler<CreateWeatherForecastCommand, int> createWeatherForecastCommandHandler)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _getWeatherForecastQueryHandler = getWeatherForecastQueryHandler ?? throw new ArgumentNullException(nameof(getWeatherForecastQueryHandler));
            _createWeatherForecastCommandHandler = createWeatherForecastCommandHandler ?? throw new ArgumentNullException(nameof(createWeatherForecastCommandHandler));
        }

        [HttpGet]
        public async Task<ActionResult<WeatherForecastListResponseDto>> GetWeatherForecastAsync()
        {
            var forecast = await _getWeatherForecastQueryHandler.HandleAsync(new GetWeatherForecastQuery());
            return Ok(forecast);
        }

        [HttpPost]
        [ServiceFilter(typeof(SaveChangesActionFilterAttribute))]
        public async Task<int?> AddWeatherForecastAsync([FromBody] CreateWeatherForecastRequestDto request)
        {
            var command = _mapper.Map<CreateWeatherForecastCommand>(request);
            var result = await _createWeatherForecastCommandHandler.HandleAsync(command);
            return result;
        }
    }
}
