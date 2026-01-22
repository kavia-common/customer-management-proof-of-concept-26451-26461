# Customer Management Backend (Clean Architecture)

This backend now includes a Clean Architecture solution under:

- `CustomerManagement.sln`
- `CustomerManagement.Api` (ASP.NET Core Web API, port 3001)
- `CustomerManagement.Application` (CQRS/UseCases, DTOs, validators, mapping)
- `CustomerManagement.Domain` (Entities, Interfaces, Specifications)
- `CustomerManagement.Infrastructure` (EF Core + SQLite + Repositories)

The previous single-project template files in `customer_management_backend/` were the initial scaffold.

## Ardalis.Specification

This project uses **Ardalis.Specification** to standardize read/query logic:

- Specifications live in: `CustomerManagement.Domain/Specifications`
  - `CustomersBySearchAndPagingSpec` (q + paging; AsNoTracking)
  - `CustomersBySearchSpec` (q filter only; used for counts; AsNoTracking)
  - `CustomerByIdSpec` (by id; AsNoTracking)

- Read handlers (CQRS queries) use the spec-aware repository abstraction:
  - `ICustomerReadRepository : IReadRepositoryBase<Customer>`

- Infrastructure provides EF Core implementations via `Ardalis.Specification.EntityFrameworkCore`:
  - `EfRepository<T>` is the generic repository implementation registered for `IReadRepositoryBase<>` / `IRepositoryBase<>`.

> Writes (create/update/delete) continue to use the existing `ICustomerRepository` abstraction for simplicity.

## Configuration

- SQLite connection string is read from env var `SQLITE_DB` if present (either a file path or full `Data Source=...` connection string).
- Fallback connection string: `Data Source=./App_Data/app.db`

Swagger UI:
- `/docs`
OpenAPI JSON:
- `/openapi.json`
