using PackIT.Domain.ValueObjects;

namespace PackIT.Domain.Policies.Gender;

internal class FemaleGenderPolicy : IPackingItemPolicy
{
    public bool IsApplicable(PolicyData data) => data.Gender is Enums.Gender.Female;

    public IEnumerable<PackingItem> GenerateItems(PolicyData data) => new[]
    {
        new PackingItem("Lipstick", 1),
        new PackingItem("Powder", 1),
        new PackingItem("Book", 1)
    };
}