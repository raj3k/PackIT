using PackIT.Shared.Abstractions.Exceptions;

namespace PackIT.Application.Exceptions;

public class PackingListNotFoundException : PackItException
{
    public Guid PackingListId { get; }

    public PackingListNotFoundException(Guid packingListId) : base($"Packing List with id: {packingListId} not found.")
    {
        PackingListId = packingListId;
    }
}