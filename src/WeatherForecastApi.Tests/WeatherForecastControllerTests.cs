using AutoFixture;
using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WeatherForecastApi.Commands;
using WeatherForecastApi.Commands.Interfaces;
using WeatherForecastApi.Dto;
using WeatherForecastApi.Queries;
using WeatherForecastApi.Queries.Interfaces;
using WeatherForecastApi.Services.Mappings;
using WeatherForecastApi.WebApi.Controllers;

namespace WeatherForecastApi.Tests
{
    public class WeatherForecastControllerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IQueryHandler<GetWeatherForecastQuery, WeatherForecastListResponseDto>> _getWeatherForecastQueryHandlerMock;
        private readonly Mock<ICommandHandler<CreateWeatherForecastCommand, int>> _createWeatherForecastCommandHandlerMock;

        public WeatherForecastControllerTests()
        {
            this._mapper = new MapperConfiguration(cfg => cfg.AddMaps(typeof(WeatherForecastProfile).Assembly))
                .CreateMapper();
            this._getWeatherForecastQueryHandlerMock = new Mock<IQueryHandler<GetWeatherForecastQuery, WeatherForecastListResponseDto>>();
            this._createWeatherForecastCommandHandlerMock = new Mock<ICommandHandler<CreateWeatherForecastCommand, int>>();
        }

        [Fact]
        public void Constructor_WithNullMapper_ThrowsArgumentNullException()
        {
            var controller = () => new WeatherForecastController(
                default!,
                this._getWeatherForecastQueryHandlerMock.Object,
                this._createWeatherForecastCommandHandlerMock.Object);
            controller.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Constructor_WithNullQueryHandler_ThrowsArgumentNullException()
        {
            var controller = () => new WeatherForecastController(
                this._mapper,
                default!,
                this._createWeatherForecastCommandHandlerMock.Object);
            controller.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Constructor_WithNullCommandHandler_ThrowsArgumentNullException()
        {
            var controller = () => new WeatherForecastController(
                this._mapper,
                this._getWeatherForecastQueryHandlerMock.Object,
                default!);
            controller.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public async Task GetWeatherForecastAsync_ValidRequestDto_ReturnsOkContent()
        {
            // Arrange
            var targetController = new WeatherForecastController(_mapper, this._getWeatherForecastQueryHandlerMock.Object, this._createWeatherForecastCommandHandlerMock.Object);
            var expectedResponse = new WeatherForecastListResponseDto
            {
                Items = new[] { new Fixture().Build<WeatherForecastResponseDto>().Create() },
            };
            this._getWeatherForecastQueryHandlerMock
                .Setup(m => m.HandleAsync(It.IsAny<GetWeatherForecastQuery>()))
                .ReturnsAsync(expectedResponse);

            // Act
            var actionResult = await targetController.GetWeatherForecastAsync();

            // Assert
            var result = actionResult.Result as OkObjectResult;
            result.Should().NotBeNull();
            Assert.IsType<WeatherForecastListResponseDto>(result!.Value);
            this._getWeatherForecastQueryHandlerMock.Verify(
                queryHandler => queryHandler.HandleAsync(It.IsAny<GetWeatherForecastQuery>()),
                Times.Once);
            this._getWeatherForecastQueryHandlerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task AddWeatherForecastAsync_ValidRequestDto_ReturnsCorrectValue()
        {
            // Arrange
            var targetController = new WeatherForecastController(_mapper, this._getWeatherForecastQueryHandlerMock.Object, this._createWeatherForecastCommandHandlerMock.Object);
            this._createWeatherForecastCommandHandlerMock
                .Setup(m => m.HandleAsync(It.IsAny<CreateWeatherForecastCommand>()))
                .ReturnsAsync(1);

            // Act
            var result = await targetController.AddWeatherForecastAsync(new Fixture().Build<CreateWeatherForecastRequestDto>().Create());

            // Assert
            result.Should().NotBeNull();
            Assert.IsType<int>(result);
            this._createWeatherForecastCommandHandlerMock.Verify(
                commandHandler => commandHandler.HandleAsync(It.IsAny<CreateWeatherForecastCommand>()),
                Times.Once);
            this._createWeatherForecastCommandHandlerMock.VerifyNoOtherCalls();
        }
    }
}
