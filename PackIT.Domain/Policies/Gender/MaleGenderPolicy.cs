using PackIT.Domain.ValueObjects;

namespace PackIT.Domain.Policies.Gender;

internal class MaleGenderPolicy : IPackingItemPolicy
{
    public bool IsApplicable(PolicyData data) => data.Gender is Enums.Gender.Male;

    public IEnumerable<PackingItem> GenerateItems(PolicyData data) => new List<PackingItem>
    {
        new ("Laptop", 1),
        new ("Beer", 10),
        new ("Book", 1)
    };
}