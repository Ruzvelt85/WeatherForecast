﻿using AutoMapper;
using WeatherForecastApi.Services.Mappings;

namespace WeatherForecastApi.Tests
{
    public class MappingTests
    {
        private readonly IMapper _mapper;

        public MappingTests()
        {
            var assemblyWithMapperProfiles = typeof(WeatherForecastProfile).Assembly;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddMaps(assemblyWithMapperProfiles);
                cfg.ShouldMapProperty = p => p.GetMethod?.IsPublic == true || p.GetMethod?.IsPrivate == true;
            });

            _mapper = config.CreateMapper();
        }

        [Fact]
        public void AutomapperConfigurationTest()
        {
            _mapper.ConfigurationProvider.AssertConfigurationIsValid();
        }
    }
}
