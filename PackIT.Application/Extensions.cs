using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using PackIT.Domain.Factories;
using PackIT.Domain.Policies;
using PackIT.Shared.Commands;
using PackIT.Shared.Queries;

namespace PackIT.Application;

public static class Extensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddCommands();
        services.AddQueries();
        // services.AddMediatR(cfg =>
        // {
        //     cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
        // });
        services.AddSingleton<IPackingListFactory, PackingListFactory>();
        services.Scan(s => s.FromAssemblies(typeof(IPackingItemPolicy).Assembly)
            .AddClasses(c => c.AssignableTo<IPackingItemPolicy>())
            .AsImplementedInterfaces()
            .WithSingletonLifetime());
        
        return services;
    }
}