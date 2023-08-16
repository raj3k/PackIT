using MediatR;
using Microsoft.EntityFrameworkCore;
using PackIT.Application.Dto;
using PackIT.Application.Queries;
using PackIT.Infrastructure.EF.Contexts;
using PackIT.Infrastructure.EF.Models;
using PackIT.Infrastructure.Queries;
using PackIT.Shared.Abstractions.Queries;

namespace PackIT.Infrastructure.EF.Queries.Handlers;

internal sealed class SearchPackingListsHandler : IRequestHandler<SearchPackingLists, IEnumerable<PackingListDto>>
{
    private readonly DbSet<PackingListReadModel> _packingList;
    private IRequestHandler<SearchPackingLists, IEnumerable<PackingListDto>> _requestHandlerImplementation;

    public SearchPackingListsHandler(ReadDbContext context)
    {
        _packingList = context.PackingLists;
    }

    // public async Task<IEnumerable<PackingListDto>> HandleAsync(SearchPackingLists query)
    // {
    //     var dbQuery = _packingList
    //         .Include(pl => pl.Items)
    //         .AsQueryable();
    //
    //     if (query.SearchPhrase is not null)
    //     {
    //         dbQuery = dbQuery.Where(pl =>
    //             Microsoft.EntityFrameworkCore.EF.Functions.ILike(pl.Name, $"%{query.SearchPhrase}%")
    //         );
    //     }
    //
    //     return await dbQuery.Select(pl => pl.AsDto()).AsNoTracking().ToListAsync();
    // }

    public async Task<IEnumerable<PackingListDto>> Handle(SearchPackingLists request, CancellationToken cancellationToken)
    {
        var dbQuery = _packingList
            .Include(pl => pl.Items)
            .AsQueryable();

        if (request.SearchPhrase is not null)
        {
            dbQuery = dbQuery.Where(pl =>
                Microsoft.EntityFrameworkCore.EF.Functions.ILike(pl.Name, $"%{request.SearchPhrase}%")
            );
        }

        return await dbQuery.Select(pl => pl.AsDto()).AsNoTracking().ToListAsync();
    }
}