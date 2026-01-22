# Customer Management Backend (Clean Architecture)

This backend now includes a Clean Architecture solution under:

- `CustomerManagement.sln`
- `CustomerManagement.Api` (ASP.NET Core Web API, port 3001)
- `CustomerManagement.Application` (CQRS/UseCases, DTOs, validators, mapping)
- `CustomerManagement.Domain` (Entities, Interfaces)
- `CustomerManagement.Infrastructure` (EF Core + SQLite)

The previous single-project template files in `customer_management_backend/` were the initial scaffold.

## Configuration

- SQLite connection string is read from env var `SQLITE_DB` if present (either a file path or full `Data Source=...` connection string).
- Fallback connection string: `Data Source=./App_Data/app.db`

Swagger UI:
- `/docs`
OpenAPI JSON:
- `/openapi.json`
