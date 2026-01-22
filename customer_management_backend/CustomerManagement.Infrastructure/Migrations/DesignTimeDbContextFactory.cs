using CustomerManagement.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace CustomerManagement.Infrastructure.Migrations;

/// <summary>
/// Enables EF Core tools (migrations) to instantiate AppDbContext.
/// </summary>
public sealed class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var sqliteEnv = Environment.GetEnvironmentVariable("SQLITE_DB");
        var connectionString =
            !string.IsNullOrWhiteSpace(sqliteEnv)
                ? (sqliteEnv.Contains("Data Source=", StringComparison.OrdinalIgnoreCase)
                    ? sqliteEnv
                    : $"Data Source={sqliteEnv}")
                : "Data Source=./App_Data/app.db";

        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        optionsBuilder.UseSqlite(connectionString);

        return new AppDbContext(optionsBuilder.Options);
    }
}
