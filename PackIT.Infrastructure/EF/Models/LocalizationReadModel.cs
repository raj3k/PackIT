using Microsoft.VisualBasic;

namespace PackIT.Infrastructure.EF.Models;

internal class LocalizationReadModel
{
    public string City { get; set; }
    public string Country { get; set; }

    public static LocalizationReadModel Create(string value)
    {
        var splitValue = value.Split(",");
        var city = Strings.Trim(splitValue.First());
        var country = Strings.Trim(splitValue.Last());
        return new LocalizationReadModel()
        {
            City = city,
            Country = country
        };
    }

    public override string ToString()
        => $"{City},{Country}";
}