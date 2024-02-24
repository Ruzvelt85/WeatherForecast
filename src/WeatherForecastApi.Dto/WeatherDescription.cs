using System.Text.Json.Serialization;

namespace WeatherForecastApi.Dto;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum WeatherDescription
{
    Freezing = 0,

    Bracing = 1,

    Chilly = 2,

    Cool = 3,

    Mild = 4,

    Warm = 5,

    Balmy = 6,

    Hot = 7,

    Sweltering = 8,

    Scorching = 9,
}
