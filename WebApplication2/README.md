# WebApplication2
WebApplication2 is an ASP.NET Core MVC business platform that combines CRM and ERP-style modules in one application.  
The project uses Entity Framework Core (SQL Server), ASP.NET Core Identity, localization (`en`/`ar`), and modular services/controllers for different business domains.

## Tech Stack
- ASP.NET Core MVC (`Microsoft.NET.Sdk.Web`)
- .NET `10.0` (`net10.0`)
- Entity Framework Core + SQL Server
- ASP.NET Core Identity
- AutoMapper
- EPPlus (Excel support)
- QuestPDF (PDF generation)

## Main Modules
- Accounting
- Banking
- Customer Service
- Document Management
- Human Resources
- Inventory Management
- Marketing Automation
- Project Management
- Sales Management
- Supply Chain Management

## Project Structure
- `Program.cs` – app startup, DI registrations, middleware pipeline, localization, security, routing
- `Controllers/` – MVC controllers grouped by module
- `Services/` – business logic services
- `Repositories/`, `UnitOfWork/` – data access abstractions
- `Configurations/` – EF Core entity configurations
- `DTOs/` – data transfer objects
- `appsettings.json`, `appsettings.Development.json` – runtime configuration

## Prerequisites
- .NET SDK 10.0+
- SQL Server instance (local or remote)
- Visual Studio 2022+ or VS Code (optional)

## Configuration
1. Open `appsettings.Development.json`.
2. Set `ConnectionStrings:defaultConnection` to your SQL Server connection string.
3. Do not commit real credentials to source control.

Example format:
```json path=null start=null
"ConnectionStrings": {
  "defaultConnection": "Server=YOUR_SERVER;Database=YOUR_DB;User Id=YOUR_USER;Password=YOUR_PASSWORD;Encrypt=True;TrustServerCertificate=True;MultipleActiveResultSets=True;"
}
```

## Run the Project
From the project directory:
```powershell path=null start=null
dotnet restore
dotnet build
dotnet run --project WebApplication2.csproj
```

Then open the local URL shown in terminal output (typically `https://localhost:<port>`).

## Notes
- The app seeds initial data during startup (`DbInitializer.Initialize` in `Program.cs`).
- Security middleware and headers are enabled in the request pipeline.
- Localization is configured for English and Arabic.

