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
│   └── Dtos/
├── Domain/
│   ├── Entities/
│   └── ValueObjects/
├── Repositories/
│   ├── Authors/
│   ├── Books/
│   ├── Categories/
│   └── Mappings/
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
│       ├── Controllers/     # Endpoints HTTP
│       │   └── Dtos/        # Request/Response DTOs
│       ├── Domain/          # Entidades e Value Objects
│       │   ├── Entities/
│       │   └── ValueObjects/
│       └── Repositories/    # Repositórios e Mapeamentos EF Core
│           └── Mappings/
├── tests/
│   └── integration/
│       └── Alexandria.Api.IntegrationTest/
├── doc/
│   └── ARCHITECTURE.md      # Documentação da arquitetura
└── .github/workflows/
    └── dotnet.yml           # CI/CD pipeline
```

## Architecture

Alexandria follows a layered architecture. See [doc/ARCHITECTURE.md](doc/ARCHITECTURE.md) for full details.

**Layers**: Controllers → Dtos → Domain → Repositories

**Key Rules**:
- Domain layer has no external dependencies (pure business logic)
- Entities: Objects with identity (Author, Book, Category)
- Value Objects: Immutable objects without identity (Address, Money)
- Repositories: Data persistence with EF Core

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
1. Create Entity in `Domain/Entities/`
2. Create Value Objects (if needed) in `Domain/ValueObjects/`
3. Create Repository interface in `Repositories/{Resource}/`
4. Create Repository implementation
5. Create EF Core mapping in `Repositories/Mappings/`
6. Create DTOs in `Controllers/Dtos/`
7. Create controller inheriting from `MainController`
8. Register dependencies in `Program.cs`

See [doc/ARCHITECTURE.md](doc/ARCHITECTURE.md) for detailed examples.

### Running locally
```bash
dotnet run --project src/Alexandria.Api
```

### Running specific test project
```bash
dotnet test tests/integration/Alexandria.Api.IntegrationTest/
```