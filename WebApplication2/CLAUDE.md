# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

WebApplication2 is an ASP.NET Core MVC CRM/ERP business platform with modular services covering Accounting, Banking, Customer Service, Document Management, HR, Inventory, Marketing Automation, Project Management, Sales, and Supply Chain Management.

**Tech Stack:** .NET 10.0, Entity Framework Core (SQL Server), ASP.NET Core Identity, AutoMapper, EPPlus (Excel), QuestPDF (PDF), localization (English/Arabic).

**Architecture:** The project follows a layered architecture with Controllers → Services → Repository/UnitOfWork → EF Core DbContext.

## Commands

### Build and Run
```powershell
dotnet restore
dotnet build
dotnet run
```

### Database Migrations
```powershell
# Create a new migration
dotnet ef migrations add <MigrationName> --project WebApplication2

# Apply migrations to database
dotnet ef database update --project WebApplication2
```

### EF Core Tools
The project uses `Microsoft.EntityFrameworkCore.Tools` for migrations. Commands must be run from the solution root.

## Project Structure

### Key Folders and Patterns

- **Program.cs** - Application startup, DI registration, middleware pipeline, security configuration, localization setup
- **Controllers/** - MVC controllers organized by business module (SalesManagement, HumanResources, Banking, etc.)
- **Services/** - Business logic layer with interfaces in `Services/Interfaces/` and implementations by module
- **Repositories/** - Generic `Repository<T>` pattern with async methods
- **UnitOfWork/** - `UnitOfWork` pattern exposing typed repositories for domain entities
- **DbContext/** - `ApplicationContext` (inherits from `IdentityDbContext`) and `DbInitializer` for seeding
- **Configurations/** - EF Core `IEntityTypeConfiguration<T>` implementations organized by module
- **MappingProfiles/** - AutoMapper profiles organized by module
- **Security/** - OWASP-based security: `AuthorizationPolicies`, `InputValidationService`, `SecurityAuditService`, `SecureConfigurationService`
- **Middlewares/** - `GlobalExceptionMiddleware`, `RateLimitingMiddleware`, `RequestLoggingMiddleware`
- **Extensions/** - Helper extensions (validation, HTTP context, claims principal, etc.)
- **Localization/** - JSON-based localization factory for English/Arabic resources
- **Hubs/** - SignalR hubs (ChatHub, DepartmentHub)

### CRM.Domain Project
The `CRM.Domain` project (sibling directory) contains:
- `Base/` - Base entity classes
- `Entities/` - Domain entities organized by module
- `IdentityEntity/` - Identity entities (`ApplicationUser`, `ApplicationRole`)
- `Enums/` - Domain enumerations

WebApplication2 references CRM.Domain via `<ProjectReference>`.

## Architecture Patterns

### Repository Pattern
```csharp
// Generic repository with async CRUD, pagination, includes
public interface IRepository<T> where T : class
{
    IQueryable<T> GetAll();
    Task<PaginatedList<T>> GetPaginatedAsync(...);
    Task<T?> GetByIdAsync(int id);
    Task AddAsync(T entity);
    // ...
}
```

### Service Layer Pattern
Services are registered in Program.cs with `AddScoped` lifetime. Most services follow:
- Interface in `Services/Interfaces/`
- Implementation in module subfolder (e.g., `Services/Finance_Accounting/`)

### UnitOfWork Pattern
`IUnitOfWork` exposes typed repositories for domain entities (e.g., `Warehouses`, `Products`, `Campaigns`).

### Authorization
Role-based policies defined in `Security/AuthorizationPolicies.cs`:
- `RequireAdminRole`, `RequireManagerRole`, `RequireHRRole`, etc.
- Custom `DataOwnership` policy for resource-based authorization

### Entity Configuration
EF Core configurations use `IEntityTypeConfiguration<T>` pattern:
```csharp
public class CompanyConfiguration : IEntityTypeConfiguration<Company>
{
    public void Configure(EntityTypeBuilder<Company> builder) { ... }
}
```
Configurations are auto-applied via `builder.ApplyConfigurationsFromAssembly()`.

### Localization
JSON-based localization with `JsonStringLocalizerFactory`. Supported cultures: `en`, `ar`. Resources stored in `Resources/` path.

### AutoMapper
AutoMapper is configured with `AddMaps(AppDomain.CurrentDomain.GetAssemblies())`. Profiles in `MappingProfiles/` define DTO ↔ Entity mappings.

### Security Middleware (OWASP Top 10)
Request pipeline order in Program.cs:
1. Security headers (`AddSecurityHeaders` extension)
2. Rate limiting (optional, commented out)
3. Global exception handling (`GlobalExceptionMiddleware`)
4. HTTPS redirection
5. Static files
6. Session
7. Routing
8. Request localization
9. Authentication → Authorization
10. Controller endpoints

## Configuration

### Connection String
Set in `appsettings.Development.json`:
```json
"ConnectionStrings": {
  "defaultConnection": "Server=...;Database=...;User Id=...;Password=...;Encrypt=True;TrustServerCertificate=True;MultipleActiveResultSets=True;"
}
```

### Database Initialization
`DbInitializer.Initialize(context)` seeds initial data on application startup (in Program.cs).

## Adding New Features

### New Domain Entity
1. Add entity class in `CRM.Domain/Entities/[Module]/`
2. Add `DbSet<TEntity>` to `ApplicationContext.cs`
3. Create `IEntityTypeConfiguration<TEntity>` in `Configurations/[Module]/`
4. Run `dotnet ef migrations add` and `dotnet ef database update`

### New Service
1. Create interface in `Services/Interfaces/I[Name]Service.cs`
2. Create implementation in `Services/[Module]/[Name]Service.cs`
3. Register in Program.cs: `builder.Services.AddScoped<I[Name]Service, [Name]Service>();`

### New Controller
Follow existing pattern in `Controllers/[Module]/[Name]Controller.cs`. Controllers use constructor injection for services and authorization attributes (e.g., `[Authorize(Policy = AuthorizationPolicies.RequireAdminRole)]`).

### New Mapping Profile
Create class inheriting `Profile` in `MappingProfiles/[Module]/[Name]Profile.cs`. AutoMapper will auto-discover it.

## Testing Notes

No test projects currently exist in the solution.

## Notes

- Third-party library licenses are configured at startup (EPPlus, QuestPDF)
- Session timeout is 30 minutes with secure cookie settings
- Identity is configured with OWASP password requirements (12 chars, mixed case, digit, special char)
- Health checks are defined but commented out in Program.cs
- Multiple result sets are enabled in SQL Server connection string
