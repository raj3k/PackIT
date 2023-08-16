using MediatR;
using PackIT.Application.Exceptions;
using PackIT.Domain.Repositories;
using PackIT.Shared.Abstractions.Commands;

namespace PackIT.Application.Commands.Handlers;

public class RemovePackingItemHandler : IRequestHandler<RemovePackingItem>
{
    private readonly IPackingListRepository _repository;

    public RemovePackingItemHandler(IPackingListRepository repository)
    {
        _repository = repository;
    }

    // public async Task HandleAsync(RemovePackingItem command)
    // {
    //     var packingList = await _repository.GetAsync(command.PackingListId);
    //
    //     if (packingList is null)
    //     {
    //         throw new PackingListNotFoundException(command.PackingListId);
    //     }
    //     
    //     packingList.RemoveItem(command.Name);
    //
    //     await _repository.UpdateAsync(packingList);
    // }

    public async Task Handle(RemovePackingItem request, CancellationToken cancellationToken)
    {
        var packingList = await _repository.GetAsync(request.PackingListId);

        if (packingList is null)
        {
            throw new PackingListNotFoundException(request.PackingListId);
        }
        
        packingList.RemoveItem(request.Name);

        await _repository.UpdateAsync(packingList);
    }
}