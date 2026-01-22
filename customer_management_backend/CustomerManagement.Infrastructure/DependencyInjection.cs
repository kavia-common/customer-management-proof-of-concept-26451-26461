using CustomerManagement.Domain.Interfaces;
using CustomerManagement.Infrastructure.Persistence;
using CustomerManagement.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CustomerManagement.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // Env var from "database" container: SQLITE_DB. If it's a file path, build a connection string.
        // Fallback to local file in App_Data.
        var sqliteEnv = Environment.GetEnvironmentVariable("SQLITE_DB");
        var defaultConn = "Data Source=./App_Data/app.db";

        var connectionString =
            !string.IsNullOrWhiteSpace(sqliteEnv)
                ? (sqliteEnv.Contains("Data Source=", StringComparison.OrdinalIgnoreCase)
                    ? sqliteEnv
                    : $"Data Source={sqliteEnv}")
                : configuration.GetConnectionString("Sqlite") ?? defaultConn;

        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlite(connectionString);
        });

        services.AddScoped<ICustomerRepository, CustomerRepository>();

        return services;
    }
}
