using AdminTripHotels.Core.Domain;
using AdminTripHotels.Core.Services;
using AdminTripHotels.WebApi.Controllers;
using AdminTripHotels.WebApi.DTO;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace AdminTripHotels.Tests.Controllers
{
    [TestFixture]
    public class OffersControllerTests
    {
        private Mock<IOfferService> _mockOfferService;
        private Mock<IMapper> _mockMapper;
        private Mock<ILogger<OffersController>> _mockLogger;
        private OffersController _controller;

        [SetUp]
        public void Setup()
        {
            _mockOfferService = new Mock<IOfferService>();
            _mockMapper = new Mock<IMapper>();
            _mockLogger = new Mock<ILogger<OffersController>>();
            
            _controller = new OffersController(
                _mockOfferService.Object,
                _mockMapper.Object,
                _mockLogger.Object);
        }

        [Test]
        public void GetOffers_WhenCalled_ReturnsAllOffersMappedToDTO()
        {
            var offers = new List<HotelOffer>
            {
                new()
                {
                    OfferId = Guid.NewGuid(),
                    Title = null
                },
                new()
                {
                    OfferId = Guid.NewGuid(),
                    Title = null
                }
            };
            
            var offerDTOs = new List<OfferDTO>
            {
                new()
                {
                    Title = null
                },
                new()
                {
                    Title = null
                }
            };

            _mockOfferService.Setup(x => x.GetAll())
                .Returns(offers);
                
            _mockMapper.Setup(x => x.Map<IEnumerable<OfferDTO>>(offers))
                .Returns(offerDTOs);

            var result = _controller.GetOffers();

            result.Should().BeEquivalentTo(offerDTOs);
            _mockOfferService.Verify(x => x.GetAll(), Times.Once);
            _mockMapper.Verify(x => x.Map<IEnumerable<OfferDTO>>(offers));
        }

        [Test]
        public void GetOfferByHotelIdAndId_WithValidParameters_ReturnsOkWithMappedDTO()
        {
            var hotelCode = "TEST123";
            var offerId = Guid.NewGuid();
            var offer = new HotelOffer
            {
                OfferId = offerId,
                Title = null
            };
            var offerDTO = new OfferDTO
            {
                Title = null
            };

            _mockOfferService.Setup(x => x.GetByHotelIdAndId(hotelCode, offerId))
                .Returns(offer);
                
            _mockMapper.Setup(x => x.Map<OfferDTO>(offer))
                .Returns(offerDTO);

            var result = _controller.GetOfferByHotelIdAndId(hotelCode, offerId);

            result.Should().BeOfType<OkObjectResult>()
                .Which.Value.Should().BeEquivalentTo(offerDTO);
                
            _mockLogger.Verify(
                x => x.Log(
                    LogLevel.Information,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString().Contains($"Получение предложения по ID:{offerId} для отеля:{hotelCode}")),
                    null,
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()),
                Times.Once);
        }

        [Test]
        public void GetOfferByHotelIdAndId_WhenOfferNotFound_ReturnsNotFound()
        {
            var hotelCode = "TEST123";
            var offerId = Guid.NewGuid();

            _mockOfferService.Setup(x => x.GetByHotelIdAndId(hotelCode, offerId))
                .Returns((HotelOffer)null);

            var result = _controller.GetOfferByHotelIdAndId(hotelCode, offerId);

            result.Should().BeOfType<NotFoundResult>();
        }

        [Test]
        public void GetOfferByHotelIdAndId_WhenExceptionThrown_ReturnsBadRequestWithMessage()
        {
            var hotelCode = "TEST123";
            var offerId = Guid.NewGuid();
            var exceptionMessage = "Test exception";

            _mockOfferService.Setup(x => x.GetByHotelIdAndId(hotelCode, offerId))
                .Throws(new Exception(exceptionMessage));

            var result = _controller.GetOfferByHotelIdAndId(hotelCode, offerId);

            result.Should().BeOfType<BadRequestObjectResult>()
                .Which.Value.Should().Be(exceptionMessage);
                
            _mockLogger.Verify(
                x => x.Log(
                    LogLevel.Error,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString().Contains($"Ошибка при Получение предложения по ID:{offerId} для отеля:{hotelCode}")),
                    It.IsAny<Exception>(),
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()),
                Times.Once);
        }

        [Test]
        public async Task CreateOffer_WithValidModel_ReturnsCreatedWithOfferId()
        {
            var createOfferDto = new CreateOfferDTO
            {
                Title = null
            };
            var offerEntity = new HotelOffer
            {
                OfferId = Guid.NewGuid(),
                Title = null
            };
            var expectedId = Guid.NewGuid();

            _mockMapper.Setup(x => x.Map<HotelOffer>(createOfferDto))
                .Returns(offerEntity);
                
            _mockOfferService.Setup(x => x.CreateAsync(offerEntity))
                .ReturnsAsync(expectedId);

            var result = await _controller.CreateOffer(createOfferDto);

            result.Should().BeOfType<CreatedAtRouteResult>()
                .Which.Value.Should().Be(expectedId);
                
            _mockMapper.Verify(x => x.Map<HotelOffer>(createOfferDto), Times.Once);
            _mockOfferService.Verify(x => x.CreateAsync(offerEntity), Times.Once);
        }

        [Test]
        public async Task CreateOffer_WithInvalidModel_ReturnsUnprocessableEntity()
        {
            var createOfferDto = new CreateOfferDTO
            {
                Title = null
            };
            _controller.ModelState.AddModelError("Test", "Test error");

            var result = await _controller.CreateOffer(createOfferDto);

            result.Should().BeOfType<UnprocessableEntityObjectResult>()
                .Which.Value.Should().BeEquivalentTo(_controller.ModelState);
        }

        [Test]
        public async Task CreateOffer_WhenExceptionThrown_ReturnsBadRequestWithMessage()
        {
            var createOfferDto = new CreateOfferDTO
            {
                Title = null
            };
            var exceptionMessage = "Test exception";

            _mockMapper.Setup(x => x.Map<HotelOffer>(createOfferDto))
                .Returns(new HotelOffer
                {
                    Title = null
                });
                
            _mockOfferService.Setup(x => x.CreateAsync(It.IsAny<HotelOffer>()))
                .ThrowsAsync(new Exception(exceptionMessage));

            var result = await _controller.CreateOffer(createOfferDto);

            result.Should().BeOfType<BadRequestObjectResult>()
                .Which.Value.Should().Be(exceptionMessage);
                
            _mockLogger.Verify(
                x => x.Log(
                    LogLevel.Error,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString().Contains(exceptionMessage)),
                    It.IsAny<Exception>(),
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()),
                Times.Once);
        }
    }
}