# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Commands

```bash
# Run locally
dotnet run --project CvApi

# Build
dotnet build

# Apply migrations to local DB
dotnet ef database update --project CvApi

# Add a new migration
dotnet ef migrations add <MigrationName> --project CvApi

# Run tests (no test project currently)
```

Swagger UI is available at `/swagger` when running locally.

## Architecture

ASP.NET Core 8 Web API targeting .NET 8. Single project: `CvApi/`.

**Auth:** Single-admin JWT authentication. No ASP.NET Identity — credentials (`AdminUsername`/`AdminPassword`) are read directly from `JwtSettings` configuration and compared in `AuthController`. Tokens expire after 8 hours. All write endpoints (`POST`/`PUT`/`DELETE`) are `[Authorize]`; all reads are public.

**Data layer:** EF Core with SQL Server. One `CvDbContext` in `Data/DBContext.cs` exposing six `DbSet`s. No repositories — controllers query `CvDbContext` directly.

**Pattern per resource:** Model → DTO(s) → Controller. Each resource has a `Create<X>Dto` and `Update<X>Dto` in `Dtos/`. Controllers map DTOs to models manually (no AutoMapper).

**Configuration:**
- Local: `appsettings.json` contains placeholder values; `appsettings.Development.json` only overrides log levels.
- Production (Azure App Service): all secrets injected as environment variables using double-underscore notation, e.g. `JwtSettings__AdminPassword`.

**CORS:** Configured for `localhost:5173`, `localhost:3000`, `ghogsted.dk`, and `www.ghogsted.dk`.

**Deployment:** Published to Azure App Service. Connection string points to Azure SQL via `ConnectionStrings__DefaultConnection`.
