using PackIT.Domain.Entities;
using PackIT.Domain.Enums;
using PackIT.Domain.Policies;
using PackIT.Domain.ValueObjects;

namespace PackIT.Domain.Factories;

public sealed class PackingListFactory : IPackingListFactory
{
    private readonly IEnumerable<IPackingItemPolicy> _policies;

    public PackingListFactory(IEnumerable<IPackingItemPolicy> policies)
    {
        _policies = policies;
    }

    public PackingList Create(PackingListId id, PackingListName name, Localization localization) =>
        new(id, name, localization);
    

    public PackingList CreateWithDefaultItems(PackingListId id, PackingListName name, TravelDays days, Gender gender,
        Localization localization, Temperature temperature)
    {
        var data = new PolicyData(days, gender, temperature, localization);

        var applicablePolices = _policies.Where(p => p.IsApplicable(data));

        var items = applicablePolices.SelectMany(p => p.GenerateItems(data));

        var packingList = new PackingList(id, name, localization);
        
        packingList.AddItems(items);

        return packingList;
    }
}