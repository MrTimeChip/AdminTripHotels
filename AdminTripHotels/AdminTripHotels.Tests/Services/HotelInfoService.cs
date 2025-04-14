using AdminTripHotels.Core.Domain;
using AdminTripHotels.Core.Repositories;
using AdminTripHotels.Core.Services;
using Moq;

namespace AdminTripHotels.Tests.Services
{
    [TestFixture]
    public class HotelInfoServiceTests
    {
        private Mock<IRepository<HotelInfo>> mockHotelInfoRepository;
        private HotelInfoService hotelInfoService;
        private CancellationToken cancellationToken;

        [SetUp]
        public void Setup()
        {
            mockHotelInfoRepository = new Mock<IRepository<HotelInfo>>();
            hotelInfoService = new HotelInfoService(mockHotelInfoRepository.Object);
            cancellationToken = new CancellationToken();
        }

        [Test]
        public void GetByCode_WithExistingCode_ReturnsHotel()
        {
            var code = "TEST123";
            var expectedHotel = new HotelInfo
            {
                Code = code,
                Description = "Test hotel description",
                Title = "Test Hotel",
                Address = "123 Test Street, Test City"
            };

            mockHotelInfoRepository.Setup(x => x.GetAll())
                .Returns(new List<HotelInfo> { expectedHotel }.AsQueryable());

            var result = hotelInfoService.GetByCode(code);

            result.Should().BeEquivalentTo(expectedHotel);
            mockHotelInfoRepository.Verify(x => x.GetAll(), Times.Once);
        }

        [Test]
        public void GetByCode_WithNonExistingCode_ThrowsInvalidOperationException()
        {
            var code = "NON_EXISTING";

            mockHotelInfoRepository.Setup(x => x.GetAll())
                .Returns(Enumerable.Empty<HotelInfo>().AsQueryable());

            Action act = () => hotelInfoService.GetByCode(code);

            act.Should().Throw<InvalidOperationException>()
                .WithMessage("Sequence contains no matching element");
        }

        [Test]
        public void GetAllHotels_WhenCalled_ReturnsAllHotels()
        {
            var expectedHotels = new List<HotelInfo>
            {
                new()
                {
                    Code = "HOTEL1",
                    Description = "First hotel description",
                    Title = "First Hotel",
                    Address = "1 First Avenue, City One"
                },
                new()
                {
                    Code = "HOTEL2",
                    Description = "Second hotel description",
                    Title = "Second Hotel",
                    Address = "2 Second Street, City Two"
                }
            }.AsQueryable();

            mockHotelInfoRepository.Setup(x => x.GetAll())
                .Returns(expectedHotels);

            var result = hotelInfoService.GetAllHotels();

            result.Should().BeEquivalentTo(expectedHotels.ToList());
            mockHotelInfoRepository.Verify(x => x.GetAll(), Times.Once);
        }

        [Test]
        public void GetByPage_WithValidParameters_ReturnsCorrectPage()
        {
            var allHotels = Enumerable.Range(1, 10)
                .Select(i => new HotelInfo
                {
                    Code = $"HOTEL{i}",
                    Description = $"Description for Hotel {i}",
                    Title = $"Hotel {i}",
                    Address = $"{i} Main Street, City {i}"
                })
                .AsQueryable();

            mockHotelInfoRepository.Setup(x => x.GetAll())
                .Returns(allHotels);

            var skip = 2;
            var take = 3;

            var result = hotelInfoService.GetByPage(skip, take);

            result.Should().NotBeNull();
            result.Should().HaveCount(take);
            result.First().Code.Should().Be("HOTEL3");
            result.Last().Code.Should().Be("HOTEL5");
            result.TotalCount.Should().Be(10);
            mockHotelInfoRepository.Verify(x => x.GetAll(), Times.Once);
        }

        [Test]
        public void GetByPage_WithSkipGreaterThanTotal_ReturnsEmptyPage()
        {
            var allHotels = Enumerable.Range(1, 5)
                .Select(i => new HotelInfo
                {
                    Code = $"HOTEL{i}",
                    Description = $"Description for Hotel {i}",
                    Title = $"Hotel {i}",
                    Address = $"{i} Park Avenue, City {i}"
                })
                .AsQueryable();

            mockHotelInfoRepository.Setup(x => x.GetAll())
                .Returns(allHotels);

            var skip = 10;
            var take = 3;

            var result = hotelInfoService.GetByPage(skip, take);

            result.Should().NotBeNull();
            result.Should().BeEmpty();
            result.TotalCount.Should().Be(5);
        }

        [Test]
        public void Add_WithValidHotel_CallsRepositoryAddAsync()
        {
            var hotel = new HotelInfo
            {
                Code = "NEW_HOTEL",
                Description = "New hotel description",
                Title = "New Hotel",
                Address = "100 New Street, New City"
            };

            hotelInfoService.Add(hotel);

            mockHotelInfoRepository.Verify(x => x.AddAsync(hotel, cancellationToken), Times.Once);
        }

        [Test]
        public void Update_WithValidHotel_CallsRepositoryUpdateAsync()
        {
            var hotel = new HotelInfo
            {
                Code = "EXISTING_HOTEL",
                Description = "Updated hotel description",
                Title = "Updated Hotel",
                Address = "500 Update Boulevard, Update City"
            };

            hotelInfoService.Update(hotel);

            mockHotelInfoRepository.Verify(x => x.UpdateAsync(hotel, cancellationToken), Times.Once);
        }

        [Test]
        public void Delete_WithExistingCode_CallsRepositoryDeleteAsync()
        {
            var code = "TO_DELETE";
            var hotel = new HotelInfo
            {
                Code = code,
                Description = "Hotel to be deleted",
                Title = "Delete Me Hotel",
                Address = "999 End Road, Final City"
            };

            mockHotelInfoRepository.Setup(x => x.GetAll())
                .Returns(new List<HotelInfo> { hotel }.AsQueryable());

            hotelInfoService.Delete(code);

            mockHotelInfoRepository.Verify(x => x.DeleteAsync(hotel, cancellationToken), Times.Once);
            mockHotelInfoRepository.Verify(x => x.GetAll(), Times.Once);
        }

        [Test]
        public void Delete_WithNonExistingCode_ThrowsInvalidOperationException()
        {
            var code = "NON_EXISTING";

            mockHotelInfoRepository.Setup(x => x.GetAll())
                .Returns(Enumerable.Empty<HotelInfo>().AsQueryable());

            Action act = () => hotelInfoService.Delete(code);

            act.Should().Throw<InvalidOperationException>()
                .WithMessage("Sequence contains no matching element");
            mockHotelInfoRepository.Verify(x => x.GetAll(), Times.Once);
            mockHotelInfoRepository.Verify(x => x.DeleteAsync(It.IsAny<HotelInfo>(), cancellationToken), Times.Never);
        }
    }
}