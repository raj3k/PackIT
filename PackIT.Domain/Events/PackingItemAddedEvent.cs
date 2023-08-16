using PackIT.Domain.Entities;
using PackIT.Domain.ValueObjects;
using PackIT.Shared.Abstractions.Domain;

namespace PackIT.Domain.Events;

// record, because this is immutable
public record PackingItemAddedEvent(PackingList PackingList, PackingItem PackingItem) : IDomainEvent
{
    
}