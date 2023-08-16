using MediatR;
using PackIT.Application.Exceptions;
using PackIT.Domain.Repositories;
using PackIT.Shared.Abstractions.Commands;

namespace PackIT.Application.Commands.Handlers;

public class DeletePackingListHandler : IRequestHandler<DeletePackingList>
{
    private readonly IPackingListRepository _repository;

    public DeletePackingListHandler(IPackingListRepository repository)
    {
        _repository = repository;
    }

    // public async Task HandleAsync(DeletePackingList command)
    // {
    //     var packingList = await _repository.GetAsync(command.PackingListId);
    //
    //     if (packingList is null)
    //     {
    //         throw new PackingListNotFoundException(command.PackingListId);
    //     }
    //
    //     await _repository.DeleteAsync(packingList);
    // }

    public async Task Handle(DeletePackingList request, CancellationToken cancellationToken)
    {
        var packingList = await _repository.GetAsync(request.PackingListId);

        if (packingList is null)
        {
            throw new PackingListNotFoundException(request.PackingListId);
        }

        await _repository.DeleteAsync(packingList);
    }
}