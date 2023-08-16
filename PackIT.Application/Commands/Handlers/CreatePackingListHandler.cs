using MediatR;
using PackIT.Application.Exceptions;
using PackIT.Application.Services;
using PackIT.Domain.Factories;
using PackIT.Domain.Repositories;
using PackIT.Domain.ValueObjects;
using PackIT.Shared.Abstractions.Commands;

namespace PackIT.Application.Commands.Handlers;

public class CreatePackingListHandler : IRequestHandler<CreatePackingList>
{
    private readonly IPackingListRepository _repository;
    private readonly IPackingListFactory _factory;
    private readonly IPackingListReadService _readService;

    public CreatePackingListHandler(IPackingListRepository repository, IPackingListFactory factory, IPackingListReadService readService)
    {
        _repository = repository;
        _factory = factory;
        _readService = readService;
    }

    // public async Task HandleAsync(CreatePackingList command)
    // {
    //     var (id, name, localizationWriteModel) = command;
    //
    //     if (await _readService.ExistsByNameAsync(name))
    //     {
    //         throw new PackingListAlreadyExistsException(name);
    //     }
    //
    //     var localization = new Localization(localizationWriteModel.City, localizationWriteModel.Country);
    //
    //     var packingList = _factory.Create(id, name, localization);
    //
    //     await _repository.AddAsync(packingList);
    // }

    public async Task Handle(CreatePackingList request, CancellationToken cancellationToken)
    {
        var (id, name, localizationWriteModel) = request;

        if (await _readService.ExistsByNameAsync(name))
        {
            throw new PackingListAlreadyExistsException(name);
        }

        var localization = new Localization(localizationWriteModel.City, localizationWriteModel.Country);

        var packingList = _factory.Create(id, name, localization);

        await _repository.AddAsync(packingList);
    }
}