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
        AdminTripHotelsDbContextConfig.ConfigureHotelInfo(builder);
        AdminTripHotelsDbContextConfig.ConfigureHotelOffer(builder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseInMemoryDatabase("AdminDb");
    }
}