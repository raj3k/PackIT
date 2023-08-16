using MediatR;
using PackIT.Application.Exceptions;
using PackIT.Domain.Events;
using PackIT.Domain.Repositories;
using PackIT.Domain.ValueObjects;
using PackIT.Shared.Abstractions.Commands;

namespace PackIT.Application.Commands.Handlers;

public class AddPackingItemHandler : IRequestHandler<AddPackingItem>
{
    private readonly IPackingListRepository _repository;
    private readonly IMediator _mediator;

    public AddPackingItemHandler(IPackingListRepository repository, IMediator mediator)
    {
        _repository = repository;
        _mediator = mediator;
    }

    // public async Task HandleAsync(AddPackingItem command)
    // {
    //     var packingList = await _repository.GetAsync(command.PackingListId);
    //
    //     if (packingList is null)
    //     {
    //         throw new PackingListNotFoundException(command.PackingListId);
    //     }
    //
    //     var packingItem = new PackingItem(command.Name, command.Quantity);
    //     packingList.AddItem(packingItem);
    //
    //     await _repository.UpdateAsync(packingList);
    // }

    public async Task Handle(AddPackingItem request, CancellationToken cancellationToken)
    {
        var packingList = await _repository.GetAsync(request.PackingListId);

        if (packingList is null)
        {
            throw new PackingListNotFoundException(request.PackingListId);
        }

        var packingItem = new PackingItem(request.Name, request.Quantity);

        packingList.AddItem(packingItem);

        // await _mediator.Publish(new PackingItemAddedEvent(packingList, packingItem), cancellationToken); 

        await _repository.UpdateAsync(packingList);
    }
}