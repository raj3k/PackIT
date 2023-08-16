using PackIT.Shared.Abstractions.Exceptions;

namespace PackIT.Domain.Exceptions;

public class InvalidTemperatureException : PackItException
{
    public double Temp { get; }

    public InvalidTemperatureException(double temp) : base($"Value '{temp} is invalid for temperature.")
    {
        Temp = temp;
    }
}