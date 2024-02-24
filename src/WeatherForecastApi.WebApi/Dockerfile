#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Directory.Build.props", "."]
COPY ["src/WeatherForecastApi.WebApi/WeatherForecastApi.WebApi.csproj", "src/WeatherForecastApi.WebApi/"]
COPY ["src/WeatherForecastApi.Commands/WeatherForecastApi.Commands.csproj", "src/WeatherForecastApi.Commands/"]
COPY ["src/WeatherForecastApi.CrosscuttingInfrastructure/WeatherForecastApi.CrosscuttingInfrastructure.csproj", "src/WeatherForecastApi.CrosscuttingInfrastructure/"]
COPY ["src/WeatherForecastApi.Data/WeatherForecastApi.Data.csproj", "src/WeatherForecastApi.Data/"]
COPY ["src/WeatherForecastApi.Domain/WeatherForecastApi.Domain.csproj", "src/WeatherForecastApi.Domain/"]
COPY ["src/WeatherForecastApi.Dto/WeatherForecastApi.Dto.csproj", "src/WeatherForecastApi.Dto/"]
COPY ["src/WeatherForecastApi.Queries/WeatherForecastApi.Queries.csproj", "src/WeatherForecastApi.Queries/"]
COPY ["src/WeatherForecastApi.Services/WeatherForecastApi.Services.csproj", "src/WeatherForecastApi.Services/"]
RUN dotnet restore "./src/WeatherForecastApi.WebApi/./WeatherForecastApi.WebApi.csproj"
COPY . .
WORKDIR "/src/src/WeatherForecastApi.WebApi"
RUN dotnet build "./WeatherForecastApi.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./WeatherForecastApi.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WeatherForecastApi.WebApi.dll"]