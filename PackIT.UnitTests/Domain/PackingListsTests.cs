using PackIT.Domain.Entities;
using PackIT.Domain.Events;
using PackIT.Domain.Exceptions;
using PackIT.Domain.Factories;
using PackIT.Domain.Policies;
using PackIT.Domain.ValueObjects;
using Shouldly;
using Xunit;

namespace PackIT.UnitTests.Domain;

public class PackingListsTests
{
    [Fact]
    public void AddItem_Throws_PackingItemAlreadyExistException()
    {
        // ARRANGE
        var packingList = GetPackingList();
        packingList.AddItem(new PackingItem("Item 1", 1));
        
        // ACT
        var exception = Record.Exception(() => packingList.AddItem(new PackingItem("Item 1", 1)));
        
        // ASSERT
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<PackingItemAlreadyExistException>();
    }

    [Fact]
    public void AddItem_ItemDoesNotExist_ItemAddedSuccessfully_Domain_Event_On_Success()
    {
        // ARRANGE
        var packingList = GetPackingList();
        var item = new PackingItem("Item 1", 1);

        // ACT
        var exception = Record.Exception(() => packingList.AddItem(item));

        // ASSERT
        exception.ShouldBeNull();
        packingList.Events.Count().ShouldBe(1);
        
        var @event = packingList.Events.FirstOrDefault() as PackingItemAddedEvent;
        @event.ShouldBeOfType<PackingItemAddedEvent>();
        
        @event.PackingItem.Name.ShouldBe("Item 1");
    }

    [Fact]
    public void PackItem_IsPackedValueIsFalse_ItemPackedSuccessfully_Domain_Event_On_Success()
    {
        // ARRANGE
        var packingList = GetPackingList();
        var item = new PackingItem("Item 1", 1);
        packingList.AddItem(item);
        
        // ACT
        var exception = Record.Exception(() => packingList.PackItem(item.Name));
        
        // ASSERT
        exception.ShouldBeNull();
        packingList.Events.Count().ShouldBe(2);

        var @event = packingList.Events.LastOrDefault() as PackingItemPackedEvent;
        @event.ShouldBeOfType<PackingItemPackedEvent>();
        
        @event.PackingItem.Name.ShouldBe(item.Name);
    }


    #region ARRANGE
    
    private PackingList GetPackingList()
    {
        var packingList = _factory.Create(Guid.NewGuid(), "My List", new Localization("Warsaw", "Poland"));
        packingList.ClearEvents();
        return packingList;
    }

    private readonly IPackingListFactory _factory;

    public PackingListsTests()
    {
        _factory = new PackingListFactory(Enumerable.Empty<IPackingItemPolicy>());
    }

    #endregion
    
    
}