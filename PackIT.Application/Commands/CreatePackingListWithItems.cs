using MediatR;
using PackIT.Domain.Enums;
using PackIT.Shared.Abstractions.Commands;

namespace PackIT.Application.Commands;


public record CreatePackingListWithItems(Guid Id, string Name, ushort Days, Gender Gender,
    LocalizationWriteModel Localization) : IRequest;

public record LocalizationWriteModel(string City, string Country);

