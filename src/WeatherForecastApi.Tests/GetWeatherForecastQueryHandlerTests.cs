using System.Linq.Expressions;
using AutoMapper;
using Moq;
using FluentAssertions;
using AutoFixture;
using WeatherForecastApi.Data.Interfaces;
using WeatherForecastApi.Services.Mappings;
using WeatherForecastApi.Queries;
using WeatherForecastApi.Domain;

namespace WeatherForecastApi.Tests
{
    public class GetWeatherForecastQueryHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IWeatherForecastRepository> _weatherForecastRepositoryMock;
        
        public GetWeatherForecastQueryHandlerTests()
        {
            this._mapper = new MapperConfiguration(cfg => cfg.AddMaps(typeof(WeatherForecastProfile).Assembly))
                .CreateMapper();
            this._weatherForecastRepositoryMock = new Mock<IWeatherForecastRepository>();
        }

        [Fact]
        public void Constructor_WithNullMapper_ThrowsArgumentNullException()
        {
            var action = () => new GetWeatherForecastQueryHandler(
                default!,
                this._weatherForecastRepositoryMock.Object);
            action.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Constructor_WithNullVehicleRepository_ThrowsArgumentNullException()
        {
            var action = () => new GetWeatherForecastQueryHandler(
                _mapper,
                default!);
            action.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public async Task HandleAsync_ReturnsValidResponse()
        {
            // Arrange
            var expectedWeatherForecast = new Fixture().Build<WeatherForecast>().With(_ => _.Value, new Random().Next(-60, 60)).CreateMany().ToList();

            this._weatherForecastRepositoryMock
                .Setup(x => x.Find(It.IsAny<Expression<Func<WeatherForecast, bool>>>()))
                .Returns(expectedWeatherForecast);

            // Act
            var handler = new GetWeatherForecastQueryHandler(_mapper, this._weatherForecastRepositoryMock.Object);
            var result = await handler.HandleAsync(new GetWeatherForecastQuery());

            // Assert
            this._weatherForecastRepositoryMock.Verify(r => r.Find(It.IsAny<Expression<Func<WeatherForecast, bool>>>()), Times.Once);
            result.Items.Count.Should().BeGreaterThan(0);
            result.Items.Count.Should().BeLessThan(8);
        }
    }
}
