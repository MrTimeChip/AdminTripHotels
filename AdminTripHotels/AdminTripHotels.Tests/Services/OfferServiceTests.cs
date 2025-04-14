using AdminTripHotels.Core.Domain;
using AdminTripHotels.Core.Repositories;
using AdminTripHotels.Core.Services;
using Microsoft.Extensions.Logging;
using Moq;

namespace AdminTripHotels.Tests.Services
{
    [Parallelizable]
    public class OfferServiceTests
    {
        private Mock<IRepository<HotelOffer>> _mockHotelOfferRepository;
        private Mock<IRepository<HotelInfo>> _mockHotelInfoRepository;
        private Mock<ILogger<OfferService>> _mockLogger;
        private OfferService _offerService;

        [SetUp]
        public void Setup()
        {
            _mockHotelOfferRepository = new Mock<IRepository<HotelOffer>>();
            _mockHotelInfoRepository = new Mock<IRepository<HotelInfo>>();
            _mockLogger = new Mock<ILogger<OfferService>>();
            
            _offerService = new OfferService(
                _mockHotelOfferRepository.Object,
                _mockHotelInfoRepository.Object,
                _mockLogger.Object);
        }

        [Test]
        public void GetAll_WhenCalled_ReturnsAllHotelOffers()
        {
            var expectedOffers = new List<HotelOffer>
            {
                new()
                {
                    OfferId = Guid.NewGuid(),
                    Title = "FirstTitle"
                },
                new()
                {
                    OfferId = Guid.NewGuid(),
                    Title = "SecondTitle"
                }
            }.AsQueryable();
            
            _mockHotelOfferRepository.Setup(x => x.GetAll())
                .Returns(expectedOffers);

            var result = _offerService.GetAll();

            result.Should().BeEquivalentTo(expectedOffers);
            _mockHotelOfferRepository.Verify(x => x.GetAll(), Times.Once);
        }

        [Test]
        public void GetByHotelIdAndId_WithValidParameters_ReturnsCorrectOffer()
        {
            var hotelCode = "TEST123";
            var offerId = Guid.NewGuid();
            var hotelInfo = new HotelInfo
            {
                Code = hotelCode,
                Description = "Test Description",
                Title = "Title1",
                Address = "BestAddress"
            };
            var expectedOffer = new HotelOffer
            {
                Title = "Title2"
            };
            expectedOffer.OfferId = offerId;

            _mockHotelInfoRepository.Setup(x => x.GetAll())
                .Returns(new List<HotelInfo> { hotelInfo }.AsQueryable());
                
            _mockHotelOfferRepository.Setup(x => x.GetAll())
                .Returns(new List<HotelOffer> { expectedOffer }.AsQueryable());

            var result = _offerService.GetByHotelIdAndId(hotelCode, offerId);

            result.Should().BeEquivalentTo(expectedOffer);
            _mockHotelInfoRepository.Verify(x => x.GetAll(), Times.Once);
            _mockHotelOfferRepository.Verify(x => x.GetAll(), Times.Once);
        }

        [Test]
        public void GetByHotelIdAndId_WithNullHotelCode_ThrowsArgumentNullException()
        {
            string hotelCode = null;
            var offerId = Guid.NewGuid();

            Action act = () => _offerService.GetByHotelIdAndId(hotelCode, offerId);

            act.Should().Throw<ArgumentNullException>()
                .WithParameterName("hotelCode");
        }

        [Test]
        public void GetByHotelIdAndId_WithEmptyHotelCode_ThrowsArgumentNullException()
        {
            var hotelCode = string.Empty;
            var offerId = Guid.NewGuid();

            Action act = () => _offerService.GetByHotelIdAndId(hotelCode, offerId);

            act.Should().Throw<ArgumentNullException>()
                .WithParameterName("hotelCode");
        }

        [Test]
        public void GetByHotelIdAndId_WithNonExistingHotelCode_ThrowsArgumentNullException()
        {
            var hotelCode = "NON_EXISTING";
            var offerId = Guid.NewGuid();
            
            _mockHotelInfoRepository.Setup(x => x.GetAll())
                .Returns(Enumerable.Empty<HotelInfo>().AsQueryable());

            Action act = () => _offerService.GetByHotelIdAndId(hotelCode, offerId);

            act.Should().Throw<ArgumentNullException>()
                .WithParameterName("hotelInfo");
        }

        [Test]
        public void GetByHotelIdAndId_WithNullOfferId_ThrowsArgumentNullException()
        {
            var hotelCode = "TEST123";
            Guid? offerId = null;
            
            var hotelInfo = new HotelInfo
            {
                Code = hotelCode,
                Description = "Description",
                Title = "TestTitle",
                Address = "Address11"
            };
            _mockHotelInfoRepository.Setup(x => x.GetAll())
                .Returns(new List<HotelInfo> { hotelInfo }.AsQueryable());

            Action act = () => _offerService.GetByHotelIdAndId(hotelCode, offerId);

            act.Should().Throw<ArgumentNullException>()
                .WithParameterName("id");
        }

        [Test]
        public void GetByHotelIdAndId_WithNonExistingOfferId_ReturnsNull()
        {
            var hotelCode = "TEST123";
            var offerId = Guid.NewGuid();
            var hotelInfo = new HotelInfo
            {
                Code = hotelCode,
                Description = "Description",
                Title = "TitleTest",
                Address = "Maloprudnaya"
            };
            
            _mockHotelInfoRepository.Setup(x => x.GetAll())
                .Returns(new List<HotelInfo> { hotelInfo }.AsQueryable());
                
            _mockHotelOfferRepository.Setup(x => x.GetAll())
                .Returns(Enumerable.Empty<HotelOffer>().AsQueryable());

            var result = _offerService.GetByHotelIdAndId(hotelCode, offerId);

            result.Should().BeNull();
        }

        [Test]
        public void CreateAsync_WhenCalled_()
        {
    
        }
    }
}