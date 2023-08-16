using PackIT.Shared.Abstractions.Exceptions;

namespace PackIT.Domain.Exceptions;

public class EmptyLocalizationValueException : PackItException
{
    public EmptyLocalizationValueException() : base("Localization cannot be empty.")
    {
    }
}