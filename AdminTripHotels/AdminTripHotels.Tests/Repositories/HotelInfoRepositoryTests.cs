using AdminTripHotels.Core.Domain;
using AdminTripHotels.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using AdminTripHotels.Core;

namespace AdminTripHotels.Tests.Repositories
{
    [Parallelizable]
    public class HotelInfoRepositoryTests
    {
        private AdminTripHotelsDbContext context;
        private HotelInfoRepository repository;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<AdminTripHotelsDbContext>()
                .UseInMemoryDatabase(databaseName: "Test_Database")
                .Options;

            context = new AdminTripHotelsDbContext(options);
            repository = new HotelInfoRepository(context);

            context.Database.EnsureDeleted();
        }

        [TearDown]
        public void TearDown()
        {
            context.Dispose();
        }

        [Test]
        public async Task AddAsync_ShouldAddHotelInfoToDatabase()
        {
            var hotelInfo = new HotelInfo
            {
                Code = "TEST123",
                Title = "Test Hotel",
                Description = "Test Description",
                Address = "Test Address"
            };

            await repository.AddAsync(hotelInfo);
            var result = await repository.GetAll().FirstOrDefaultAsync(h => h.Code == "TEST123");

            result.Should().NotBeNull();
            result.Code.Should().Be(hotelInfo.Code);
            result.Title.Should().Be(hotelInfo.Title);
            result.Description.Should().Be(hotelInfo.Description);
            result.Address.Should().Be(hotelInfo.Address);
        }

        [Test]
        public async Task DeleteAsync_ShouldRemoveHotelInfoFromDatabase()
        {
            var hotelInfo = new HotelInfo
            {
                Code = "TO_DELETE",
                Title = "To Delete Hotel",
                Description = "To Delete Description",
                Address = "To Delete Address"
            };
            
            await repository.AddAsync(hotelInfo);

            await repository.DeleteAsync(hotelInfo);
            var result = await repository.GetAll().FirstOrDefaultAsync(h => h.Code == "TO_DELETE");

            result.Should().BeNull();
        }

        [Test]
        public async Task UpdateAsync_ShouldUpdateExistingHotelInfo()
        {
            var originalHotel = new HotelInfo
            {
                Code = "UPDATE_TEST",
                Title = "Original Title",
                Description = "Original Description",
                Address = "Original Address"
            };
            
            await repository.AddAsync(originalHotel);
            
            context.ChangeTracker.Clear();
            
            var updatedHotel = new HotelInfo
            {
                Code = "UPDATE_TEST",
                Title = "Updated Title",
                Description = "Updated Description",
                Address = "Updated Address"
            };

            await repository.UpdateAsync(updatedHotel);
            var result = await repository.GetAll().FirstOrDefaultAsync(h => h.Code == "UPDATE_TEST");

            result.Should().NotBeNull();
            result.Title.Should().Be("Updated Title");
            result.Description.Should().Be("Updated Description");
            result.Address.Should().Be("Updated Address");
        }

        [Test]
        public async Task GetAll_ShouldReturnAllHotels()
        {
            var hotels = new List<HotelInfo>
            {
                new() { Code = "HOTEL1", Title = "Hotel 1", Description = "Desc 1", Address = "Addr 1" },
                new() { Code = "HOTEL2", Title = "Hotel 2", Description = "Desc 2", Address = "Addr 2" },
                new() { Code = "HOTEL3", Title = "Hotel 3", Description = "Desc 3", Address = "Addr 3" }
            };
            
            await repository.AddAllAsync(hotels);

            var result = await repository.GetAll().ToListAsync();

            result.Should().HaveCount(3);
            result.Select(h => h.Code).Should().Contain(new[] { "HOTEL1", "HOTEL2", "HOTEL3" });
        }

        [Test]
        public async Task DeleteAllAsync_WithPredicate_ShouldRemoveMatchingHotels()
        {
            var hotels = new List<HotelInfo>
            {
                new() { Code = "KEEP1", Title = "Keep Hotel 1", Description = "Desc", Address = "Addr" },
                new() { Code = "DELETE1", Title = "Delete Hotel 1", Description = "Desc", Address = "Addr" },
                new() { Code = "DELETE2", Title = "Delete Hotel 2", Description = "Desc", Address = "Addr" }
            };
            
            await repository.AddAllAsync(hotels);

            await repository.DeleteAllAsync(h => h.Title.Contains("Delete"));
            var remainingHotels = await repository.GetAll().ToListAsync();

            remainingHotels.Should().HaveCount(1);
            remainingHotels[0].Code.Should().Be("KEEP1");
        }
    }
}