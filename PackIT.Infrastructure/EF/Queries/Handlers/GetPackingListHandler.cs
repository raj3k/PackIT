using MediatR;
using Microsoft.EntityFrameworkCore;
using PackIT.Application.Dto;
using PackIT.Application.Queries;
using PackIT.Infrastructure.EF.Contexts;
using PackIT.Infrastructure.EF.Models;
using PackIT.Infrastructure.Queries;
using PackIT.Shared.Abstractions.Queries;

namespace PackIT.Infrastructure.EF.Queries.Handlers;
// IQueryHandler<GetPackingList, PackingListDto>

internal sealed class GetPackingListHandler : IRequestHandler<GetPackingList, PackingListDto>
{
    private readonly DbSet<PackingListReadModel> _packingLists;

    public GetPackingListHandler(ReadDbContext context)
        => _packingLists = context.PackingLists;

    // public Task<PackingListDto> HandleAsync(GetPackingList query)
    //     => _packingLists
    //         .Include(pl => pl.Items)
    //         .Where(pl => pl.Id == query.Id)
    //         .Select(pl => pl.AsDto())
    //         .AsNoTracking()
    //         .SingleOrDefaultAsync();

    public Task<PackingListDto> Handle(GetPackingList request, CancellationToken cancellationToken)
     => _packingLists
         .Include(pl => pl.Items)
         .Where(pl => pl.Id == request.Id)
         .Select(pl => pl.AsDto())
         .AsNoTracking()
         .SingleOrDefaultAsync();
}