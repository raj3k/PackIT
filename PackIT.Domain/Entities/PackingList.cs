using PackIT.Domain.Events;
using PackIT.Domain.Exceptions;
using PackIT.Domain.ValueObjects;
using PackIT.Shared.Abstractions.Domain;

namespace PackIT.Domain.Entities;

public class PackingList : AggregateRoot<PackingListId>
{
    public new PackingListId Id { get; private set; }
    private PackingListName _name;
    private Localization _localization;

    private readonly LinkedList<PackingItem> _items = new();
    public LinkedList<PackingItem> Items => _items;

    private PackingList(PackingListId id, PackingListName name, Localization localization, LinkedList<PackingItem> items) : this(id, name, localization)
    {
        _items = items;
    }
    
    internal PackingList(PackingListId id, PackingListName name, Localization localization)
    {
        Id = id;
        _name = name;
        _localization = localization;
    }

    private PackingList()
    {
    }
    
    public void AddItem(PackingItem item)
    {
        var alreadyExists = _items.Any(i => i.Name == item.Name);

        if (alreadyExists)
        {
            throw new PackingItemAlreadyExistException(_name, item.Name);
        }

        _items.AddFirst(item);
        AddEvent(new PackingItemAddedEvent(this, item));
    }
    
    public void AddItems(IEnumerable<PackingItem> items)
    {
        foreach (var item in items)
        {
            AddItem(item);
        }
    }

    public void PackItem(string itemName)
    {
        var item = GetItem(itemName);
        // below creates copy with changed property
        var packedItem = item with { IsPacked = true };

        _items.Find(item)!.Value = packedItem;
        AddEvent(new PackingItemPackedEvent(this, item));
    }

    public void RemoveItem(string itemName)
    {
        var item = GetItem(itemName);
        _items.Remove(item);
        AddEvent(new PackingItemRemovedEvent(this, item));
    }

    private PackingItem GetItem(string itemName)
    {
        var item = _items.SingleOrDefault(i => i.Name == itemName);

        if (item is null)
        {
            throw new PackingItemNotFoundException(itemName);
        }
        
        return item;
    }
}