using AdminTripHotels.Core;
using AdminTripHotels.Core.Domain;
using AdminTripHotels.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AdminTripHotels.Tests.Repositories;

[Parallelizable]
public class HotelOfferRepositoryTests
{
    private AdminTripHotelsDbContext context;
    private HotelOfferRepository repository;

    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<AdminTripHotelsDbContext>()
            .UseInMemoryDatabase(databaseName: "Test_Db")
            .Options;

        context = new AdminTripHotelsDbContext(options);
        repository = new HotelOfferRepository(context);

        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();
    }

    [TearDown]
    public void TearDown()
    {
        context.Dispose();
    }

    [Test]
    public async Task AddAsync_ShouldAddHotelOfferToDatabase()
    {
        var hotelOffer = new HotelOffer
        {
            OfferId = Guid.NewGuid(),
            Title = "Deluxe Room",
            Description = "Spacious room with sea view",
            RoomsRemained = 5,
            TotalPrice = 150.99m,
            ExtraBeds = 1
        };

        await repository.AddAsync(hotelOffer);
        await context.SaveChangesAsync();

        var result = await context.SearchHotelOffers.FindAsync(hotelOffer.OfferId);
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(hotelOffer);
    }

    [Test]
    public async Task GetAll_ShouldReturnAllHotelOffers()
    {
        var offers = new List<HotelOffer>
        {
            new() { OfferId = Guid.NewGuid(), Title = "Offer 1", TotalPrice = 100m },
            new() { OfferId = Guid.NewGuid(), Title = "Offer 2", TotalPrice = 200m },
            new() { OfferId = Guid.NewGuid(), Title = "Offer 3", TotalPrice = 300m }
        };

        await context.SearchHotelOffers.AddRangeAsync(offers);
        await context.SaveChangesAsync();

        var result = repository.GetAll().ToList();

        result.Should().HaveCount(3);
        result.Should().BeEquivalentTo(offers);
    }

    [Test]
    public async Task UpdateAsync_ShouldUpdateExistingHotelOffer()
    {
        var originalOffer = new HotelOffer
        {
            OfferId = Guid.NewGuid(),
            Title = "Original Title",
            TotalPrice = 100m
        };

        await repository.AddAsync(originalOffer);
        await context.SaveChangesAsync();
        context.Entry(originalOffer).State = EntityState.Detached;

        var updatedOffer = new HotelOffer
        {
            OfferId = originalOffer.OfferId,
            Title = "Updated Title",
            TotalPrice = 150m
        };

        await repository.UpdateAsync(updatedOffer);

        var result = await context.SearchHotelOffers.FindAsync(originalOffer.OfferId);
        result.Should().NotBeNull();
        result!.Title.Should().Be("Updated Title");
        result.TotalPrice.Should().Be(150m);
    }

    [Test]
    public async Task DeleteAsync_ShouldRemoveHotelOfferFromDatabase()
    {
        var offer = new HotelOffer
        {
            OfferId = Guid.NewGuid(),
            Title = "To be deleted",
            TotalPrice = 100m
        };

        await repository.AddAsync(offer);
        await context.SaveChangesAsync();

        await repository.DeleteAsync(offer);

        var result = await context.SearchHotelOffers.FindAsync(offer.OfferId);
        result.Should().BeNull();
    }

    [Test]
    public async Task DeleteAllAsync_WithPredicate_ShouldRemoveMatchingOffers()
    {
        var offers = new List<HotelOffer>
        {
            new() { OfferId = Guid.NewGuid(), Title = "Cheap Offer", TotalPrice = 50m },
            new() { OfferId = Guid.NewGuid(), Title = "Expensive Offer", TotalPrice = 500m },
            new() { OfferId = Guid.NewGuid(), Title = "Mid-range Offer", TotalPrice = 200m }
        };

        await context.SearchHotelOffers.AddRangeAsync(offers);
        await context.SaveChangesAsync();

        await repository.DeleteAllAsync(o => o.TotalPrice < 100m || o.TotalPrice > 400m);

        var remainingOffers = context.SearchHotelOffers.ToList();
        remainingOffers.Should().HaveCount(1);
        remainingOffers[0].Title.Should().Be("Mid-range Offer");
    }
}