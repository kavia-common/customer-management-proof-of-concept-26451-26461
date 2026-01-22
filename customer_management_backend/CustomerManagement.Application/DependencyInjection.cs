using System.Reflection;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace CustomerManagement.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

        services.AddAutoMapper(cfg =>
        {
            cfg.AddMaps(Assembly.GetExecutingAssembly());
        });

        // Register all FluentValidation validators from this assembly without relying on extra DI extension packages.
        // This scans for types implementing IValidator<T> and registers them as transient.
        var validatorInterfaceType = typeof(IValidator<>);
        var validators = Assembly.GetExecutingAssembly()
            .GetTypes()
            .Where(t => !t.IsAbstract && !t.IsInterface)
            .Select(t => new
            {
                ImplementationType = t,
                ServiceTypes = t.GetInterfaces()
                    .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == validatorInterfaceType)
                    .ToArray()
            })
            .Where(x => x.ServiceTypes.Length > 0)
            .ToList();

        foreach (var v in validators)
        {
            foreach (var serviceType in v.ServiceTypes)
            {
                services.AddTransient(serviceType, v.ImplementationType);
            }
        }

        // Pipeline behavior for validation -> ProblemDetails is produced in API layer via exception mapping middleware.
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(Behaviors.ValidationBehavior<,>));

        return services;
    }
}
