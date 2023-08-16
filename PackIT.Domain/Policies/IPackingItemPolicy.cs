using PackIT.Domain.ValueObjects;

namespace PackIT.Domain.Policies;

public interface IPackingItemPolicy
{
    bool IsApplicable(PolicyData data);
    IEnumerable<PackingItem> GenerateItems(PolicyData data);
}