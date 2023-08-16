using PackIT.Application.Dto;
using PackIT.Domain.ValueObjects;

namespace PackIT.Infrastructure.EF.Models;

internal class PackingListReadModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int Version { get; set; }
    public LocalizationReadModel Localization { get; set; }
    public ICollection<PackingItemReadModel> Items { get; set; }
}