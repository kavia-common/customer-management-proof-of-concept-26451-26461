using CustomerManagement.Api.Middleware;
using CustomerManagement.Application;
using CustomerManagement.Infrastructure;
using CustomerManagement.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Controllers + ProblemDetails (validation handled in pipeline)
builder.Services.AddControllers();

// Standardized problem details formatting
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    // We'll handle validation problems ourselves (FluentValidation + pipeline) to return consistent ProblemDetails.
    options.SuppressModelStateInvalidFilter = true;
});

builder.Services.AddTransient<ExceptionHandlingMiddleware>();

// OpenAPI/Swagger via NSwag
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApiDocument(config =>
{
    config.Title = "Customer Management API";
    config.Version = "1.0.0";
    config.Description = "Customer Management API built with Clean Architecture, CQRS (MediatR), EF Core, and SQLite.";
});

// CORS (allow frontend container; permissive for PoC)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.SetIsOriginAllowed(_ => true)
            .AllowCredentials()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

// Clean Architecture DI
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

app.UseCors("AllowAll");

app.UseMiddleware<ExceptionHandlingMiddleware>();

// OpenAPI/Swagger
app.UseOpenApi();
app.UseSwaggerUi(config =>
{
    config.Path = "/docs";
});

// Map API controllers
app.MapControllers();

// Health check endpoint
app.MapGet("/", () => new { message = "Healthy" });

// Ensure DB created + migrations applied at startup.
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    await db.Database.EnsureCreatedAsync();
    await db.Database.MigrateAsync();
}

app.Run();
