using Microsoft.Extensions.DependencyInjection;
using PackIT.Shared.Abstractions.Queries;

namespace PackIT.Shared.Queries;

internal sealed class InMemoryQueryDispatcher : IQueryDispatcher
{
    private readonly IServiceProvider _serviceProvider;

    public InMemoryQueryDispatcher(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task<TResult> DispatchQueryAsync<TResult>(IQuery<TResult> query)
    {
        using var scope = _serviceProvider.CreateScope();
        var handlerType = typeof(IQueryHandler<,>).MakeGenericType(query.GetType(), typeof(TResult));
        var handler = scope.ServiceProvider.GetRequiredService(handlerType);
        
        return await (Task<TResult>)handlerType.GetMethod("HandleAsync")?
            .Invoke(handler, new []{query});
        
        // var handler = scope.ServiceProvider.GetRequiredService<IQueryHandler<IQuery<TResult>,TResult>>();
        //
        // return await handler.HandleAsync(query);
    }
}