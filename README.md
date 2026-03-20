# Alexandria

Alexandria is a modern Web API built with ASP.NET Core 10.0, designed as a foundation for managing library resources such as authors, books, and related entities.

## Tech Stack

- **.NET 10.0** - Latest LTS release with C# 14
- **ASP.NET Core 10.0** - Web API framework
- **Swashbuckle** - OpenAPI/Swagger documentation
- **xUnit** - Testing framework
- **Testcontainers** - Docker-based integration testing

## Getting Started

### Prerequisites

- [.NET 10.0 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)
- [Docker](https://www.docker.com/) (for integration tests)

### Build & Run

```bash
# Restore dependencies
dotnet restore

# Build the solution
dotnet build

# Run the API
dotnet run --project Alexandria.Api

# Run tests
dotnet test
```

## Project Structure

```
Alexandria/
├── Alexandria.Api/              # Main Web API project
│   ├── Controllers/             # API controllers
│   │   └── Dtos/                # Request/Response DTOs
│   └── Program.cs               # Application entry point
├── tst/
│   └── integration/
│       └── Alexandria.Api.IntegrationTest/  # Integration tests
└── .github/workflows/           # CI/CD pipelines
```

## API Documentation

When running in development mode, Swagger UI is available at:
- `/swagger` - Interactive API documentation

## License

MIT