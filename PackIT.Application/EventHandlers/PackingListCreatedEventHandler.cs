using MediatR;
using PackIT.Domain.Events;

namespace PackIT.Application.EventHandlers;

public class PackingListCreatedEventHandler : INotificationHandler<PackingListCreatedEvent>
{
    public Task Handle(PackingListCreatedEvent notification, CancellationToken cancellationToken)
    {
        // TODO: Save Packing List to 'packing_read' scheme
        
        return Task.CompletedTask;
    }
}