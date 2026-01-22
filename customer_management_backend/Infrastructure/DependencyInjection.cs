using CustomerManagement.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CustomerManagement.Infrastructure;

/// <summary>
/// Infrastructure dependency injection.
/// </summary>
public static class DependencyInjection
{
    // PUBLIC_INTERFACE
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        /**
         * Registers infrastructure services (EF Core DbContext).
         *
         * Configuration precedence:
         *  1) Environment variable SQLITE_DB (provided by the database container)
         *  2) ConnectionStrings:DefaultConnection from appsettings
         *  3) Fallback to known local path used by the database container
         */
        var envSqlitePath = Environment.GetEnvironmentVariable("SQLITE_DB");

        // If SQLITE_DB is a file path, build a connection string.
        // If it is already a connection string (contains '=') we accept it as-is.
        var defaultConnStr =
            !string.IsNullOrWhiteSpace(envSqlitePath)
                ? (envSqlitePath.Contains('=') ? envSqlitePath : $"Data Source={envSqlitePath}")
                : (configuration.GetConnectionString("DefaultConnection")
                   ?? "Data Source=/home/kavia/workspace/code-generation/customer-management-proof-of-concept-26451-26463/database/myapp.db");

        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlite(defaultConnStr);
        });

        return services;
    }
}
