using NSubstitute;
using PackIT.Application.Commands;
using PackIT.Application.Commands.Handlers;
using PackIT.Application.Dto.External;
using PackIT.Application.Exceptions;
using PackIT.Application.Services;
using PackIT.Domain.Entities;
using PackIT.Domain.Enums;
using PackIT.Domain.Factories;
using PackIT.Domain.Repositories;
using PackIT.Domain.ValueObjects;
using PackIT.Shared.Abstractions.Commands;
using Shouldly;
using Xunit;

namespace PackIT.UnitTests.Application;

public class CreatePackingListWithItemsHandlerTest
{

    Task Act(CreatePackingListWithItems command)
        => _commandHandler.HandleAsync(command);
    
    [Fact]
    public async Task HandleAsync_Throws_PackingListAlreadyExistsException_When_List_With_Name_Already_Exists()
    {
        var command = new CreatePackingListWithItems(Guid.NewGuid(), "MyList", 10, Gender.Male, default);
        _readService.ExistsByNameAsync(command.Name).Returns(true);

        var exception = await Record.ExceptionAsync(() => Act(command));

        // ASSERT
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<PackingListAlreadyExistsException>();
    }
    
    [Fact]
    public async Task HandleAsync_Throws_MissingLocalizationWeatherException_When_Weather_Data_Is_Not_Returned_From_Service()
    {
        var command = new CreatePackingListWithItems(Guid.NewGuid(), "MyList", 10, Gender.Male, new LocalizationWriteModel("Warsaw", "Poland"));
        _readService.ExistsByNameAsync(command.Name).Returns(false);
        _weatherService.GetWeatherAsync(Arg.Any<Localization>()).Returns(default(WeatherDto));

        var exception = await Record.ExceptionAsync(() => Act(command));

        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<MissingLocalizationWeatherException>();
    }

    [Fact]
    public async Task HandleAsync_Calls_Repository_On_Success()
    {
        var command = new CreatePackingListWithItems(Guid.NewGuid(), "MyList", 10, Gender.Male, new LocalizationWriteModel("Warsaw", "Poland"));
        _readService.ExistsByNameAsync(command.Name).Returns(false);
        _weatherService.GetWeatherAsync(Arg.Any<Localization>()).Returns(new WeatherDto(12));
        var packingList = _factory.CreateWithDefaultItems(command.Id, command.Name, command.Days, command.Gender,
                Arg.Any<Localization>(), Arg.Any<Temperature>())
            .Returns(default(PackingList));
    }
    
    #region ARRANGE

    // private readonly ICommandHandler<CreatePackingListWithItems> _commandHandler;
    private readonly IPackingListRepository _repository;
    private readonly IPackingListFactory _factory;
    private readonly IPackingListReadService _readService;
    private readonly IWeatherService _weatherService;

    public CreatePackingListWithItemsHandlerTest()
    {
        _repository = Substitute.For<IPackingListRepository>();
        _weatherService = Substitute.For<IWeatherService>();
        _factory = Substitute.For<IPackingListFactory>();
        _readService = Substitute.For<IPackingListReadService>();

        _commandHandler = new CreatePackingListWithItemsHandler(_factory, _readService, _repository, _weatherService);
    }

    #endregion
}