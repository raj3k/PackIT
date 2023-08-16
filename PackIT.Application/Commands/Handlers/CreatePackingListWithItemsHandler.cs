using MediatR;
using PackIT.Application.Exceptions;
using PackIT.Application.Services;
using PackIT.Domain.Factories;
using PackIT.Domain.Repositories;
using PackIT.Domain.ValueObjects;
using PackIT.Shared.Abstractions.Commands;

namespace PackIT.Application.Commands.Handlers;

public class CreatePackingListWithItemsHandler : IRequestHandler<CreatePackingListWithItems>
{
    private readonly IPackingListRepository _repository;
    private readonly IPackingListFactory _factory;
    private readonly IPackingListReadService _readService;
    private readonly IWeatherService _weatherService;

    public CreatePackingListWithItemsHandler(IPackingListFactory factory, IPackingListReadService service, IPackingListRepository repository, IWeatherService weatherService)
    {
        _factory = factory;
        _readService = service;
        _repository = repository;
        _weatherService = weatherService;
    }

    // public async Task HandleAsync(CreatePackingListWithItems command)
    // {
    //     var (id, name, days, gender, localizationWriteModel) = command;
    //
    //     if (await _readService.ExistsByNameAsync(name))
    //     {
    //         throw new PackingListAlreadyExistsException(name);
    //     }
    //
    //     var localization = new Localization(localizationWriteModel.City, localizationWriteModel.Country);
    //     var weather = await _weatherService.GetWeatherAsync(localization);
    //
    //     if (weather is null)
    //     {
    //         throw new MissingLocalizationWeatherException(localization);
    //     }
    //
    //     var packingList = _factory.CreateWithDefaultItems(id, name, days, gender, localization, weather.Temperature);
    //
    //     await _repository.AddAsync(packingList);
    // }

    public async Task Handle(CreatePackingListWithItems request, CancellationToken cancellationToken)
    {
        var (id, name, days, gender, localizationWriteModel) = request;

        if (await _readService.ExistsByNameAsync(name))
        {
            throw new PackingListAlreadyExistsException(name);
        }

        var localization = new Localization(localizationWriteModel.City, localizationWriteModel.Country);
        var weather = await _weatherService.GetWeatherAsync(localization);

        if (weather is null)
        {
            throw new MissingLocalizationWeatherException(localization);
        }

        var packingList = _factory.CreateWithDefaultItems(id, name, days, gender, localization, weather.Temperature);

        await _repository.AddAsync(packingList);
    }
}