using PackIT.Shared.Abstractions.Exceptions;

namespace PackIT.Domain.Exceptions;

public class PackingItemQuantityZeroOrNegativeNumberException : PackItException
{
    public PackingItemQuantityZeroOrNegativeNumberException() : 
        base("Packing Item Quantity must be a positive number.")
    {
    }
}