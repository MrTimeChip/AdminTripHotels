using AdminTripHotels.Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace AdminTripHotels.Core;

public static class AdminTripHotelsDbContextConfig
{
    public static void ConfigureHotelInfo(ModelBuilder builder)
    {
        builder.Entity<HotelInfo>(entity =>
        {
            entity.HasKey(x => x.Code);
        });
    }

    public static void ConfigureHotelOffer(ModelBuilder builder)
    {
        builder.Entity<HotelOffer>(entity =>
        {
            entity.HasKey(x => x.OfferId);
        });
    }
}