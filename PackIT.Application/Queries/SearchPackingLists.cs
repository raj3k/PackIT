using MediatR;
using PackIT.Application.Dto;
using PackIT.Shared.Abstractions.Queries;

namespace PackIT.Application.Queries;

public class SearchPackingLists : IRequest<IEnumerable<PackingListDto>>
{
    public string? SearchPhrase { get; set; }
}