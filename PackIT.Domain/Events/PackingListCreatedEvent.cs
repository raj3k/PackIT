using PackIT.Domain.Entities;
using PackIT.Shared.Abstractions.Domain;

namespace PackIT.Domain.Events;

public record PackingListCreatedEvent(PackingList PackingList) : IDomainEvent;