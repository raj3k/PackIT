using MediatR;
using PackIT.Domain.Events;

namespace PackIT.Application.EventHandlers;

public class PackingItemAddedEventHandler : INotificationHandler<PackingItemAddedEvent>
{
    public Task Handle(PackingItemAddedEvent notification, CancellationToken cancellationToken)
    {
        Console.WriteLine($"PackIT Application Domain Event: {notification.GetType().Name}");
        
        return Task.CompletedTask;
    }
}