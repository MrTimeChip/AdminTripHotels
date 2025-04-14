using AdminTripHotels.Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace AdminTripHotels.Core;

public class AdminTripHotelsDbContext : DbContext
{
    public AdminTripHotelsDbContext(DbContextOptions<AdminTripHotelsDbContext> options)
        : base(options)
    {
    }

    public DbSet<HotelInfo> SearchResultItems { get; set; }
    public DbSet<HotelOffer> SearchHotelOffers { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<HotelInfo>()
            .HasKey(x => x.Code);
        builder.Entity<HotelOffer>()
            .HasKey(x => x.OfferId);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseInMemoryDatabase("AdminDb");
    }
}