using PackIT.Domain.Exceptions;

namespace PackIT.Domain.ValueObjects;

public record Temperature
{
    public double Value { get; }

    public Temperature(double value)
    {
        if (value is < -100 or > 100)
        {
            throw new InvalidTemperatureException(value);
        }

        Value = value;
    }
    
    // conversion to string
    public static implicit operator double(Temperature temp) => temp.Value;

    
    // conversion from string
    public static implicit operator Temperature(double temp) => new(temp);
}