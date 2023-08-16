using MediatR;
using PackIT.Domain.Enums;
using PackIT.Shared.Abstractions.Commands;

namespace PackIT.Application.Commands;

public record CreatePackingList(Guid Id, string Name, LocalizationWriteModel LocalizationWriteModel) : IRequest;
