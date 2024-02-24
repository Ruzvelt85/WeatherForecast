using FluentValidation.TestHelper;
using WeatherForecastApi.Dto;
using WeatherForecastApi.Dto.Validators;

namespace WeatherForecastApi.Tests
{
    public class CreateWeatherForecastRequestDtoValidatorTests
    {
        private readonly CreateWeatherForecastRequestDtoValidator _dtoValidator;

        public CreateWeatherForecastRequestDtoValidatorTests()
        {
            _dtoValidator = new CreateWeatherForecastRequestDtoValidator();
        }

        [Fact]
        public async Task Correct_ShouldNotHaveValidationError()
        {
            var model = new CreateWeatherForecastRequestDto(DateTime.Today.AddDays(1), 0);

            var result = await _dtoValidator.TestValidateAsync(model);

            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public async Task DateInThePast_ShouldHaveValidationError()
        {
            var model = new CreateWeatherForecastRequestDto(DateTime.Today.AddDays(-1), 0);

            var result = await _dtoValidator.TestValidateAsync(model);

            result.ShouldHaveValidationErrorFor(_ => _.Date);
        }

        [Fact]
        public async Task DateIsToday_ShouldHaveValidationError()
        {
            var model = new CreateWeatherForecastRequestDto(DateTime.Today, 0);

            var result = await _dtoValidator.TestValidateAsync(model);

            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public async Task ValueIsTooSmall_ShouldHaveValidationError()
        {
            var model = new CreateWeatherForecastRequestDto(DateTime.Today.AddDays(1), -61);

            var result = await _dtoValidator.TestValidateAsync(model);

            result.ShouldHaveValidationErrorFor(_ => _.Value);
        }

        [Fact]
        public async Task ValueIsTooBig_ShouldHaveValidationError()
        {
            var model = new CreateWeatherForecastRequestDto(DateTime.Today.AddDays(1), +61);

            var result = await _dtoValidator.TestValidateAsync(model);

            result.ShouldHaveValidationErrorFor(_ => _.Value);
        }
    }
}
