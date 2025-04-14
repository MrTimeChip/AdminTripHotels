using System.Linq.Expressions;
using AdminTripHotels.Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace AdminTripHotels.Core.Repositories;

public class HotelOfferRepository(AdminTripHotelsDbContext context) : IRepository<HotelOffer>
{
    private readonly DbSet<HotelOffer> dbSet = context.SearchHotelOffers;

    public IQueryable<HotelOffer> GetAll()
    {
        return dbSet;
    }

    public async Task AddAsync(HotelOffer item, CancellationToken cancellationToken = default)
    {
        await dbSet.AddAsync(item, cancellationToken);
    }

    public async Task AddAllAsync(IEnumerable<HotelOffer> entities, CancellationToken cancellationToken = default)
    {
        await dbSet.AddRangeAsync(entities, cancellationToken);
    }

    public Task UpdateAsync(HotelOffer item, CancellationToken cancellationToken = default)
    {
        dbSet.Update(item);
        return context.SaveChangesAsync(cancellationToken);
    }

    public Task DeleteAsync(HotelOffer item, CancellationToken cancellationToken = default)
    {
        dbSet.Remove(item);
        return context.SaveChangesAsync(cancellationToken);
    }

    public Task DeleteAllAsync(IEnumerable<HotelOffer> items, CancellationToken cancellationToken = default)
    {
        dbSet.RemoveRange(items);
        return context.SaveChangesAsync(cancellationToken);
    }

    public Task DeleteAllAsync(Expression<Func<HotelOffer, bool>> predicate,
        CancellationToken cancellationToken = default)
    {
        dbSet.RemoveRange(dbSet.Where(predicate));
        return context.SaveChangesAsync(cancellationToken);
    }
}