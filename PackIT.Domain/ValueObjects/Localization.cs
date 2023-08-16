using Microsoft.VisualBasic;
using PackIT.Domain.Exceptions;

namespace PackIT.Domain.ValueObjects;

// Primary constructor
public record Localization(string City, string Country)
{
    // Factory method; Conversion from string to Value Object
    public static Localization Create(string value)
    {

        if (string.IsNullOrWhiteSpace(value))
        {
            throw new EmptyLocalizationValueException();
        }

        var splitLocalization = value.Split(",");
        var city = Strings.Trim(splitLocalization.First());
        var country = Strings.Trim(splitLocalization.Last());
        return new Localization(city, country);
    }

    // Conversion from Value Object to string
    public override string ToString()
    {
        return $"{City},{Country}";
    }
}