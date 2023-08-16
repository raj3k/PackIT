using MediatR;
using PackIT.Shared.Abstractions.Commands;

namespace PackIT.Application.Commands;

public record PackItem(Guid PackingListId, string PackingItemName) : IRequest;