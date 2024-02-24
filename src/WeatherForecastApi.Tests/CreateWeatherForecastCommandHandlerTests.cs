using System.Linq.Expressions;
using AutoMapper;
using Moq;
using FluentAssertions;
using AutoFixture;
using WeatherForecastApi.Data.Interfaces;
using WeatherForecastApi.Services.Mappings;
using WeatherForecastApi.Commands;
using WeatherForecastApi.CrosscuttingInfrastructure.Exceptions;
using WeatherForecastApi.Domain;

namespace WeatherForecastApi.Tests
{
    public class CreateWeatherForecastCommandHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IWeatherForecastRepository> _weatherForecastRepositoryMock;
        
        public CreateWeatherForecastCommandHandlerTests()
        {
            this._mapper = new MapperConfiguration(cfg => cfg.AddMaps(typeof(WeatherForecastProfile).Assembly)).CreateMapper();
            ;
            this._weatherForecastRepositoryMock = new Mock<IWeatherForecastRepository>();
        }

        [Fact]
        public void Constructor_WithNullMapper_ThrowsArgumentNullException()
        {
            var action = () => new CreateWeatherForecastCommandHandler(
                default!,
                this._weatherForecastRepositoryMock.Object);
            action.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Constructor_WithNullWeatherForecastRepository_ThrowsArgumentNullException()
        {
            var action = () => new CreateWeatherForecastCommandHandler(
                _mapper,
                default!);
            action.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public async Task HandleAsync_NoWeatherForecastExists_ReturnsValidResponse()
        {
            // Arrange
            var createdWeatherForecast = new Fixture().Build<WeatherForecast>().Create();
            var command = new Fixture().Build<CreateWeatherForecastCommand>().Create();
            this._weatherForecastRepositoryMock
                .Setup(x => x.ExistsAsync(It.IsAny<Expression<Func<WeatherForecast, bool>>>()))
                .ReturnsAsync(false);
            this._weatherForecastRepositoryMock
                .Setup(x => x.AddAsync(It.IsAny<WeatherForecast>()))
                .ReturnsAsync(createdWeatherForecast);

            // Act
            var handler = new CreateWeatherForecastCommandHandler(_mapper, this._weatherForecastRepositoryMock.Object);
            var result = await handler.HandleAsync(command);

            // Assert
            this._weatherForecastRepositoryMock.Verify(r => r.ExistsAsync(It.IsAny<Expression<Func<WeatherForecast, bool>>>()), Times.Once);
            this._weatherForecastRepositoryMock.Verify(r => r.AddAsync(It.IsAny<WeatherForecast>()), Times.Once);
            result.Should().Be(createdWeatherForecast.Id);
        }

        [Fact]
        public async Task HandleAsync_WeatherForecastExists_ThrowsConflictException()
        {
            // Arrange
            var command = new Fixture().Build<CreateWeatherForecastCommand>().Create();
            this._weatherForecastRepositoryMock
                .Setup(x => x.ExistsAsync(It.IsAny<Expression<Func<WeatherForecast, bool>>>()))
                .ReturnsAsync(true);

            // Act && Assert
            var handler = new CreateWeatherForecastCommandHandler(_mapper, this._weatherForecastRepositoryMock.Object);
            var action = async () => await handler.HandleAsync(command);

            await action.Should().ThrowAsync<ConflictException>();
            this._weatherForecastRepositoryMock.Verify(r => r.ExistsAsync(It.IsAny<Expression<Func<WeatherForecast, bool>>>()), Times.Once);
            this._weatherForecastRepositoryMock.Verify(r => r.AddAsync(It.IsAny<WeatherForecast>()), Times.Never);
        }
    }
}
