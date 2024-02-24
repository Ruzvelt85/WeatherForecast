using System.ComponentModel.DataAnnotations;

namespace WeatherForecastApi.Domain
{
    public class WeatherForecast : IEntity
    {
        [Key]
        public int Id { get; set; }
        
        public DateTime Date { get; set; }

        public int Value { get; set; }
    }
}
