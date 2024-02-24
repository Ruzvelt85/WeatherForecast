namespace WeatherForecastApi.Dto;

public record WeatherForecastResponseDto
{
    public string Date { get; init; }

    public WeatherDescription Description { get; init; }
}
