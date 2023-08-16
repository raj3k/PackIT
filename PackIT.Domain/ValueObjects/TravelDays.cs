using PackIT.Domain.Exceptions;

namespace PackIT.Domain.ValueObjects;

public record TravelDays
{
    public ushort Value { get; }

    public TravelDays(ushort value)
    {
        if (value is 0 or > 100)
        {
            throw new InvalidTravelDaysException(value);
        }

        Value = value;
    }
    
    // conversion to string
    public static implicit operator ushort(TravelDays days) => days.Value;

    
    // conversion from string
    public static implicit operator TravelDays(ushort days) => new(days);
}