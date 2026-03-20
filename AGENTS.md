# AGENTS.md - Alexandria Development Guide

## Overview

Alexandria is a .NET 10.0 ASP.NET Core Web API application. This guide provides essential information for agents working on this codebase.

## Build Commands

```bash
# Restore dependencies
dotnet restore

# Build the solution
dotnet build

# Build without restore
dotnet build --no-restore
```

## Testing

```bash
# Run all tests
dotnet test

# Run all tests without building
dotnet test --no-build

# Run tests with verbosity
dotnet test --verbosity normal

# Run a single test class
dotnet test --filter "FullyQualifiedName~AuthorTest"

# Run a specific test method
dotnet test --filter "FullyQualifiedName~AuthorTest.Create_ShouldReturnOk"
```

### Test Projects
- **Integration Tests**: `tests/integration/Alexandria.Api.IntegrationTest/`
- **Framework**: xUnit
- **Test Data**: Bogus
- **Containers**: Testcontainers (for SQL Server)

## Code Style Guidelines

### Language & Configuration
- **.NET 10.0** with **ImplicitUsings** enabled (no explicit `using` statements needed)
- **Nullable reference types** enabled (`<Nullable>enable</Nullable>`)
- **File-scoped namespaces** preferred (e.g., `namespace Alexandria.Api.Controllers;`)

### Naming Conventions
- **Classes/Methods/Properties**: PascalCase (e.g., `AuthorController`, `CreateAsync`)
- **Local variables**: camelCase (e.g., `authorRequest`, `cancellationToken`)
- **Parameters**: camelCase
- **Constants**: PascalCase
- **Interfaces**: Prefix with `I` (e.g., `IController`)

### File Organization
```
Alexandria.Api/
├── Controllers/
│   ├── MainController.cs (base controller)
│   ├── AuthorController.cs
│   └── Dtos/
│       └── CreateAuthorRequest.cs
└── Program.cs
```

### Error Handling
- Use `ActionResult` return types in controllers
- Use `[ApiController]` attribute
- Use `[HttpPost]`, `[HttpGet]`, etc. for routing
- Use `CancellationToken` for async operations

### DTOs and Validation
- Use `[Required]` for mandatory fields
- Use `[MaxLength]` for string length limits
- Use `ErrorMessage` for custom validation messages
- Place DTOs in `Controllers/Dtos/` folder

### Testing Patterns
- Use xUnit's `[Fact]` attribute
- Use `Testcontainers.MsSql` for database integration tests
- Use `Bogus` for generating test data
- Test files go in `tests/integration/`

## Project Structure

```
Alexandria/
├── Alexandria.sln
├── src/
│   └── Alexandria.Api/      # Main web API project
├── tests/
│   └── integration/
│       └── Alexandria.Api.IntegrationTest/
└── .github/workflows/
    └── dotnet.yml           # CI/CD pipeline
```

## Dependencies

- **ASP.NET Core 10.0** (Web API)
- **Swashbuckle** (Swagger/OpenAPI)
- **Microsoft.AspNetCore.OpenApi**
- **xUnit** (testing)
- **Bogus** (fake data)
- **Testcontainers** (Docker containers for tests)

## Git Conventions

### Commit Message Format
All commits MUST use gitmoji format:

```
<emoji> <type>: <description>

- <optional detailed changes>
```

### Common Gitmoji
- `:rocket:` `feat:` - New feature
- `:bug:` `fix:` - Bug fix
- `:recycle:` `refactor:` - Code refactoring
- `:memo:` `docs:` - Documentation changes
- `:white_check_mark:` `test:` - Adding or updating tests
- `:lipstick:` `style:` - Formatting, missing semicolons, etc.
- `:construction:` `chore:` - Maintenance tasks, dependencies
- `:arrow_up:` `upgrade:` - Dependency or framework upgrades
- `:sparkles:` `perf:` - Performance improvements

### Commit Example
```
🚀 feat: add author creation endpoint

- Add CreateAuthorRequest DTO
- Implement AuthorController.CreateAsync
- Add validation for required fields
```

## Common Tasks

### Adding a new endpoint
1. Create DTO in `Controllers/Dtos/`
2. Create controller inheriting from `MainController`
3. Add HTTP attribute (`[HttpPost]`, `[HttpGet]`, etc.)
4. Return appropriate `ActionResult` type

### Running locally
```bash
dotnet run --project src/Alexandria.Api
```

### Running specific test project
```bash
dotnet test tests/integration/Alexandria.Api.IntegrationTest/
```