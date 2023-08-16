using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using PackIT.Shared.Abstractions.Queries;

namespace PackIT.Shared.Queries;

public static class Extensions
{
    public static IServiceCollection AddQueries(this IServiceCollection services)
    {
        var assembly = Assembly.GetCallingAssembly();

        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(assembly);
        });
        
        // services.AddSingleton<IQueryDispatcher, InMemoryQueryDispatcher>();
        // services.Scan(s => s.FromAssemblies(assembly)
        //     .AddClasses(c => c.AssignableTo(typeof(IQueryHandler<,>)))
        //     .AsImplementedInterfaces()
        //     .WithScopedLifetime());
        return services;
    }
}