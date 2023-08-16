using MediatR;
using PackIT.Application.Exceptions;
using PackIT.Domain.Repositories;
using PackIT.Shared.Abstractions.Commands;

namespace PackIT.Application.Commands.Handlers;

public class PackItemHandler : IRequestHandler<PackItem>
{
    private readonly IPackingListRepository _repository;

    public PackItemHandler(IPackingListRepository repository)
    {
        _repository = repository;
    }

    // public async Task HandleAsync(PackItem command)
    // {
    //     var packingList = await _repository.GetAsync(command.PackingListId);
    //
    //     if (packingList is null)
    //     {
    //         throw new PackingListNotFoundException(command.PackingListId);
    //     }
    //     
    //     // TODO: throw exception when item not found, probably in entity
    //     packingList.PackItem(command.PackingItemName);
    //
    //     await _repository.UpdateAsync(packingList);
    // }

    public async Task Handle(PackItem request, CancellationToken cancellationToken)
    {
        var packingList = await _repository.GetAsync(request.PackingListId);

        if (packingList is null)
        {
            throw new PackingListNotFoundException(request.PackingListId);
        }
        
        // TODO: throw exception when item not found, probably in entity
        packingList.PackItem(request.PackingItemName);

        await _repository.UpdateAsync(packingList);
    }
}