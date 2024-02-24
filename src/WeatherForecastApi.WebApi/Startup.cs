using System.Reflection;
using AutoMapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using WeatherForecastApi.Commands;
using WeatherForecastApi.Commands.Interfaces;
using WeatherForecastApi.Data;
using WeatherForecastApi.Data.Interfaces;
using WeatherForecastApi.Dto;
using WeatherForecastApi.Dto.Validators;
using WeatherForecastApi.Queries;
using WeatherForecastApi.Queries.Interfaces;
using WeatherForecastApi.Services.Mappings;
using WeatherForecastApi.WebApi.Filters;

namespace WeatherForecastApi.WebApi
{
    public sealed class Startup
    {
        private Assembly ExecutingAssembly => Assembly.GetEntryAssembly() ?? Assembly.GetCallingAssembly();

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDefaultServices();

            services.AddScoped<SaveChangesActionFilterAttribute>();

            services.AddDbContext<InMemoryEfCoreContext>();

            services.AddScoped<IWeatherForecastRepository, WeatherForecastRepository>();

            services.AddScoped<IUnitOfWork, UnitOfWork<InMemoryEfCoreContext>>();

            services.AddScoped<IQueryHandler<GetWeatherForecastQuery, WeatherForecastListResponseDto>, GetWeatherForecastQueryHandler>();
            services.AddScoped<ICommandHandler<CreateWeatherForecastCommand, int>, CreateWeatherForecastCommandHandler>();

            ConfigureAutoMapper(services);
            ConfigureFluentValidation(services);
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseDefaultAppConfig();
        }

        private void ConfigureAutoMapper(IServiceCollection services)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddMaps(typeof(WeatherForecastProfile).Assembly);
                cfg.ShouldMapProperty = p => p.GetMethod?.IsPublic == true || p.GetMethod?.IsPrivate == true;
            });

            services.AddSingleton(config.CreateMapper());
        }
        private void ConfigureFluentValidation(IServiceCollection services)
        {
            services.AddFluentValidationAutoValidation();
            services.AddFluentValidationClientsideAdapters();
            services.AddValidatorsFromAssemblyContaining<CreateWeatherForecastRequestDtoValidator>();
        }
    }
}
