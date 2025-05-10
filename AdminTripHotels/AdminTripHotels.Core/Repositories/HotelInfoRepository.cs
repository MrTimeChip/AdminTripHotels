using System.Linq.Expressions;
using AdminTripHotels.Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace AdminTripHotels.Core.Repositories;

public class HotelInfoRepository : IRepository<HotelInfo>
{
    private readonly AdminTripHotelsDbContext context;
    private readonly DbSet<HotelInfo> dbset;

    public HotelInfoRepository(AdminTripHotelsDbContext context)
    {
        this.context = context;
        dbset = context.SearchResultItems;
    }

    public IQueryable<HotelInfo> GetAll() => dbset.AsQueryable();

    public async Task AddAsync(HotelInfo item, CancellationToken cancellationToken = default)
    {
        await dbset.AddAsync(item, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task AddAllAsync(IEnumerable<HotelInfo> entities, CancellationToken cancellationToken = default)
    {
        await dbset.AddRangeAsync(entities, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(HotelInfo item, CancellationToken cancellationToken = default)
    {
        dbset.Update(item);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(HotelInfo item, CancellationToken cancellationToken = default)
    {
        dbset.Remove(item);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAllAsync(IEnumerable<HotelInfo> items, CancellationToken cancellationToken = default)
    {
        dbset.RemoveRange(items);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAllAsync(Expression<Func<HotelInfo, bool>> predicate,
        CancellationToken cancellationToken = default)
    {
        var itemsToDelete = await dbset.Where(predicate).ToListAsync(cancellationToken);
        dbset.RemoveRange(itemsToDelete);
        await context.SaveChangesAsync(cancellationToken);
    }
}