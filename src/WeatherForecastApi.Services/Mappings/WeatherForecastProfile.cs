using AutoMapper;
using WeatherForecastApi.Commands;
using WeatherForecastApi.Domain;
using WeatherForecastApi.Dto;

namespace WeatherForecastApi.Services.Mappings
{
    public class WeatherForecastProfile : Profile
    {
        public WeatherForecastProfile()
        {
            CreateMap<CreateWeatherForecastRequestDto, CreateWeatherForecastCommand>();

            CreateMap<CreateWeatherForecastCommand, WeatherForecast>()
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date.Date))
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<WeatherForecast, WeatherForecastResponseDto>(MemberList.Destination)
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date.ToShortDateString()))
                .ForMember(dest => dest.Description, expression => expression.MapFrom(src => src.Value));

            CreateMap<int, WeatherDescription>().ConvertUsing(new TemperatureValueToDescriptionConverter());
            CreateMap<IEnumerable<WeatherForecast>, WeatherForecastListResponseDto>()
                .ForMember(dest => dest.Items, opt =>
                    opt.MapFrom(src => new List<WeatherForecast>(src)));
        }
    }
}
