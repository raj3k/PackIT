using PackIT.Shared.Abstractions.Exceptions;

namespace PackIT.Domain.Exceptions;

public class PackingItemAlreadyExistException : PackItException
{
    public string ListName { get; }
    public string ItemName { get; }

    public PackingItemAlreadyExistException(string listName, string itemName) 
        : base($"Packing list: {listName} already defined item '{itemName}'")
    {
        ListName = listName;
        ItemName = itemName;
    }
}