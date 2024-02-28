# Weather Forecast API

This solution represents REST API for storing and getting weather forecasts.

It doesn't contain any user interface - requests can be tested via Swagger UI (or Postman).

InMemory Database is used for storing data.

###  Solution structure

The solution consists of 6 projects:

**WebApi**: Contains the API controller, action filter and middleware for exception handling.

**Domain**: Defines domain model

**Dto**: Contains DTO models for requests and responses and its validators

**Commands**: Contains the command and the command handler for storing weather forecasts.

**Queries**: Contains the query and the query handler for receiving weather forecasts.

**CrosscuttingInfrastructure**: Contains custom exceptions and common helper files.

**Data**: Contains interaction with data layer (EF Core context, repositories, unit of work).

**Tests**: Contains unit tests for controllers, query handler, command handler, validators, and mapping.


This structure can be a little bit extensive or even redundant for the scope of the given task, but it follows DDD approach and onion architecture principles and will be able to accomodate the following features well.

###  Requests

**POST /WeatherForecast**: Adds a weather forecast for the specific date. It is not allowed to store weather forecast for the same date.

**GET /WeatherForecast**: Returns existing weather forecasts for the next 7 days starting from today. In case of absence of the forecasts it returns an empty array.

###  Build and Run

To build and run the solution, please perform the following steps:

1) Open the solution in MS Visual Studio
2) Execute command Build - Build solution
3) To run API press F5 (or, press on Docker Compose)

API will be explored through Swagger at https://localhost:7262/swagger/index.html

Alternatively, the solution can be built and run through CLI or docker-compose. 
