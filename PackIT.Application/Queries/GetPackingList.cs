using MediatR;
using PackIT.Application.Dto;
using PackIT.Shared.Abstractions.Queries;

namespace PackIT.Application.Queries;

public class GetPackingList : IRequest<PackingListDto>
{
    public Guid Id { get; set; }
}