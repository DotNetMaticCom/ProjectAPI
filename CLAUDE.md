# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

This is an **nArchitecture** project - a .NET 9 Clean Architecture implementation with CQRS pattern, designed for enterprise-level applications. The project consists of two main directories:

- **nArchitecture.Core/** - Core packages and infrastructure components (git submodule)
- **nArchitecture/** - Main application implementation

## Common Development Commands

### Build Commands
```bash
# Build Core packages
dotnet build nArchitecture.Core/CorePackages.sln

# Build main solution
dotnet build nArchitecture/NArchitecture.sln

# Build in Release mode
dotnet build nArchitecture/NArchitecture.sln -c Release

# Clean, restore and rebuild
dotnet clean nArchitecture/NArchitecture.sln
dotnet restore nArchitecture/NArchitecture.sln
dotnet build nArchitecture/NArchitecture.sln
```

### Running the Application
```bash
# Run WebAPI (default Development profile)
dotnet run --project nArchitecture/src/starterProject/WebAPI/WebAPI.csproj

# Run in Staging environment
dotnet run --project nArchitecture/src/starterProject/WebAPI/WebAPI.csproj --launch-profile WebAPI.Console.Staging
```

The application runs on `http://localhost:5278` with Swagger UI at `http://localhost:5278/swagger`

### Test Commands
```bash
# Run all tests
dotnet test nArchitecture/NArchitecture.sln

# Run tests with detailed output
dotnet test nArchitecture/NArchitecture.sln --verbosity normal

# Run specific test project
dotnet test nArchitecture/tests/StarterProject.Application.Tests/StarterProject.Application.Tests.csproj

# Run single test
dotnet test nArchitecture/NArchitecture.sln --filter "FullyQualifiedName~TestClassName.TestMethodName"

# Run tests with code coverage
dotnet test nArchitecture/NArchitecture.sln --collect:"XPlat Code Coverage"
```

### Database Commands
```bash
# Add migration (run from WebAPI directory)
cd nArchitecture/src/starterProject/WebAPI
dotnet ef migrations add <MigrationName> --project ../Persistence/Persistence.csproj --startup-project WebAPI.csproj

# Update database
dotnet ef database update --project ../Persistence/Persistence.csproj --startup-project WebAPI.csproj

# Remove last migration
dotnet ef migrations remove --project ../Persistence/Persistence.csproj --startup-project WebAPI.csproj
```

### Code Quality
```bash
# Format code with CSharpier
dotnet tool restore
dotnet csharpier nArchitecture/

# Check formatting without making changes
dotnet csharpier nArchitecture/ --check

# Run code analysis with Roslynator
dotnet roslynator analyze nArchitecture/NArchitecture.sln
```

### Code Generation
```bash
# Use nArchitecture Generator
dotnet tool restore
nArchGen
```

## Architecture Overview

### Layer Structure

1. **Domain Layer** (`src/starterProject/Domain/`)
   - Core entities with business logic
   - No dependencies on other layers
   - Key entities: User, OperationClaim, RefreshToken, EmailAuthenticator, OtpAuthenticator, UserOperationClaim

2. **Application Layer** (`src/starterProject/Application/`)
   - CQRS implementation (Commands/Queries)
   - Business rules and validations
   - Feature-based folder organization (Auth, Users, OperationClaims, UserOperationClaims)
   - Pipeline behaviors for cross-cutting concerns
   - Service interfaces and implementations

3. **Infrastructure Layer** (`src/starterProject/Infrastructure/`)
   - External service implementations
   - Third-party integrations (e.g., Cloudinary for image storage)

4. **Persistence Layer** (`src/starterProject/Persistence/`)
   - Entity Framework Core implementation
   - Database context (BaseDbContext)
   - Entity configurations
   - Repository implementations
   - Migration files

5. **WebAPI Layer** (`src/starterProject/WebAPI/`)
   - REST API controllers
   - Program.cs entry point
   - Configuration and middleware setup
   - appsettings.json for different environments

### Core Packages (`nArchitecture.Core/`)

Core packages are referenced as project references (not NuGet packages) for better development experience:

- **Core.Application** - Base CQRS implementations, pipeline behaviors, DTOs, business rules
- **Core.CrossCuttingConcerns** - Exception handling, logging abstractions and implementations
- **Core.ElasticSearch** - Elastic Search integration and search models
- **Core.Persistence** - Generic repository pattern, dynamic querying, pagination support
- **Core.Security** - JWT authentication, authorization, hashing, encryption utilities
- **Core.Mailing** - Email service abstractions and MailKit implementation
- **Core.Localization** - Multi-language support with YAML-based resources
- **Core.Test** - Testing utilities, mock repositories, fake data generators

### Key Patterns and Practices

1. **CQRS Pattern**: All features use Commands for write operations and Queries for read operations
2. **MediatR Pipeline**: Request/response handling with behaviors for:
   - Authorization checks (AuthorizationBehavior)
   - Validation (RequestValidationBehavior)
   - Caching (CachingBehavior, CacheRemovingBehavior)
   - Logging (LoggingBehavior)
   - Performance monitoring (PerformanceBehavior)
   - Transaction management (TransactionScopeBehavior)

3. **Repository Pattern**: Generic repository with async operations and specification pattern
4. **Clean Architecture**: Dependency flow from outer layers to inner layers only
5. **Feature-based Organization**: Each feature has its own folder with Commands, Queries, Rules, Constants, and Resources

### Authentication & Authorization

- JWT tokens with refresh token support
- Role-based access control using OperationClaims
- Secured operations decorator for authorization requirements
- Email and OTP authenticator support
- Google & Microsoft authentication integration ready

### Configuration

Primary configuration in `appsettings.json`:
- Database connection strings (SQL Server by default)
- JWT settings (AccessTokenExpiration, SecurityKey, Issuer, Audience)
- Redis configuration for distributed caching
- Logging configurations for Serilog (File, ElasticSearch, PostgreSQL, MongoDB, etc.)
- Mail settings for SMTP

### Testing Strategy

- Unit tests for Application layer business logic
- Mock repositories for isolation (BaseMockRepository)
- FakeData generators for test data
- xUnit as testing framework with Dependency Injection support
- Moq for mocking dependencies

## .NET 9 Migration Notes

The project has been migrated from .NET 8 to .NET 9:
- All projects target `<TargetFramework>net9.0</TargetFramework>`
- Core packages are referenced as project references instead of NuGet packages
- ASP.NET Core packages are now included via `<FrameworkReference Include="Microsoft.AspNetCore.App" />`
- All package versions have been updated to .NET 9 compatible versions

## Important Development Notes

- Always run `dotnet tool restore` before using code generation or formatting tools
- Database migrations must be run from the WebAPI project directory
- The project uses .NET 9.0 - ensure correct SDK version is installed
- Follow existing CQRS patterns when adding new features
- Use feature-based folder organization in Application layer
- All new entities should have corresponding Entity Type Configurations in Persistence layer
- Business rules should be implemented as separate classes in Application layer
- Use pipeline behaviors for cross-cutting concerns instead of duplicating code
- Localization resources are YAML files in feature-specific Resources/Locales folders