using PackIT.Domain.Entities;
using PackIT.Domain.Enums;
using PackIT.Domain.ValueObjects;

namespace PackIT.Domain.Factories;

public interface IPackingListFactory
{
    PackingList Create(PackingListId id, PackingListName name, Localization localization);
    PackingList CreateWithDefaultItems(PackingListId id, PackingListName name, TravelDays days, Gender gender, 
        Localization localization, Temperature temperature);
}