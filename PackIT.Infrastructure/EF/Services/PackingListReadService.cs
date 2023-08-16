using Microsoft.EntityFrameworkCore;
using PackIT.Application.Services;
using PackIT.Domain.Entities;
using PackIT.Infrastructure.EF.Contexts;
using PackIT.Infrastructure.EF.Models;

namespace PackIT.Infrastructure.EF.Services;

internal sealed class PackingListReadService : IPackingListReadService
{
    private readonly DbSet<PackingListReadModel> _packingLists;

    public PackingListReadService(ReadDbContext context)
    {
        _packingLists = context.PackingLists;
    }

    public Task<bool> ExistsByNameAsync(string name)
    {
        return _packingLists.AnyAsync(pl => pl.Name == name);
    }
}