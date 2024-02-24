using FluentValidation;

namespace WeatherForecastApi.Dto.Validators
{
    public class CreateWeatherForecastRequestDtoValidator : AbstractValidator<CreateWeatherForecastRequestDto>
    {
        public CreateWeatherForecastRequestDtoValidator()
        {
            RuleFor(_ => _.Date).NotEmpty().GreaterThanOrEqualTo(DateTime.Today.Date);
            RuleFor(_ => _.Value).NotNull().GreaterThanOrEqualTo(-60).LessThanOrEqualTo(60);
        }
    }
}
