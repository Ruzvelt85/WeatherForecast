namespace WeatherForecastApi.Dto
{
    public record WeatherForecastListResponseDto
    {
        public IReadOnlyCollection<WeatherForecastResponseDto> Items { get; init; } = new List<WeatherForecastResponseDto>();
    }
}
