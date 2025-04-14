using AdminTripHotels.Core.Domain;
using AdminTripHotels.Core.Services;
using AdminTripHotels.WebApi.Controllers;
using AdminTripHotels.WebApi.DTO;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using Newtonsoft.Json;
using AdminTripHotels.Core.Utils;
using Microsoft.AspNetCore.Mvc;

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
            _controller = new OffersController(_mockOfferService.Object, _mockMapper.Object, _mockLogger.Object);
        }

        [Test]
        public async Task GetOffers_WithValidParameters_ReturnsOkResultWithPagination()
        {
            var hotelCode = "TEST123";
            var pageNumber = 1;
            var pageSize = 3;
            
            var pagedOffers = new PageList<HotelOffer>(
                new List<HotelOffer> { new HotelOffer
                {
                    Title = null
                }, new HotelOffer
                {
                    Title = null
                }, new HotelOffer
                    {
                        Title = null
                    }
                },
                10, pageNumber, pageSize);
            
            var expectedDtos = new List<OfferDTO> { new OfferDTO
            {
                Title = null
            }, new OfferDTO
            {
                Title = null
            }, new OfferDTO
                {
                    Title = null
                }
            };

            _mockOfferService.Setup(x => x.GetOffersByHotelCode(hotelCode, pageNumber, pageSize))
                .ReturnsAsync(pagedOffers);
            
            _mockMapper.Setup(x => x.Map<IEnumerable<OfferDTO>>(pagedOffers))
                .Returns(expectedDtos);

            var result = await _controller.GetOffers(hotelCode, pageNumber, pageSize);

            result.Should().BeOfType<OkObjectResult>();
            var okResult = result;
            okResult.Value.Should().BeEquivalentTo(expectedDtos);
            
            _controller.Response.Headers.Should().ContainKey("X-Pagination");
            var paginationHeader = JsonConvert.DeserializeAnonymousType(
                _controller.Response.Headers["X-Pagination"],
                new { totalCount = 0, pageSize = 0, currentPage = 0, totalPages = 0 });
            
            paginationHeader.totalCount.Should().Be(10);
            paginationHeader.pageSize.Should().Be(pageSize);
            paginationHeader.currentPage.Should().Be(pageNumber);
        }

        [Test]
        public async Task GetOffers_WithInvalidPageNumber_AdjustsToFirstPage()
        {
            var hotelCode = "TEST123";
            var invalidPageNumber = 0;
            var expectedPageNumber = 1;
            var pageSize = 3;
            
            var pagedOffers = new PageList<HotelOffer>(
                new List<HotelOffer>(), 0, expectedPageNumber, pageSize);

            _mockOfferService.Setup(x => x.GetOffersByHotelCode(hotelCode, expectedPageNumber, pageSize))
                .ReturnsAsync(pagedOffers);

            await _controller.GetOffers(hotelCode, invalidPageNumber, pageSize);

            _mockOfferService.Verify(x => 
                x.GetOffersByHotelCode(hotelCode, expectedPageNumber, pageSize), Times.Once);
        }

        [Test]
        public async Task GetOffers_WithInvalidPageSize_AdjustsWithinLimits()
        {
            var hotelCode = "TEST123";
            var pageNumber = 1;
            var invalidPageSize = 20;
            var expectedPageSize = 10;
            
            var pagedOffers = new PageList<HotelOffer>(
                new List<HotelOffer>(), 0, pageNumber, expectedPageSize);

            _mockOfferService.Setup(x => x.GetOffersByHotelCode(hotelCode, pageNumber, expectedPageSize))
                .ReturnsAsync(pagedOffers);

            await _controller.GetOffers(hotelCode, pageNumber, invalidPageSize);

            _mockOfferService.Verify(x => 
                x.GetOffersByHotelCode(hotelCode, pageNumber, expectedPageSize), Times.Once);
        }

        [Test]
        public async Task GetOffers_WithNoOffers_ReturnsNotFound()
        {
            var hotelCode = "TEST123";
            
            _mockOfferService.Setup(x => x.GetOffersByHotelCode(hotelCode, It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync((PageList<HotelOffer>)null);

            var result = await _controller.GetOffers(hotelCode);

            result.Should().BeOfType<NotFoundResult>();
        }

        [Test]
        public void GetOfferByHotelIdAndId_WithValidParameters_ReturnsOkResult()
        {
            var hotelCode = "TEST123";
            var offerId = Guid.NewGuid();
            var offer = new HotelOffer
            {
                OfferId = offerId,
                Title = null
            };
            var expectedDto = new OfferDTO
            {
                OfferId = offerId,
                Title = null
            };
            
            _mockOfferService.Setup(x => x.GetHotelOfferById(hotelCode, offerId))
                .Returns(offer);
            
            _mockMapper.Setup(x => x.Map<OfferDTO>(offer))
                .Returns(expectedDto);

            var result = _controller.GetOfferByHotelIdAndId(hotelCode, offerId);

            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult.Value.Should().BeEquivalentTo(expectedDto);
        }

        [Test]
        public void GetOfferByHotelIdAndId_WithNonExistingOffer_ReturnsNotFound()
        {
            var hotelCode = "TEST123";
            var offerId = Guid.NewGuid();
            
            _mockOfferService.Setup(x => x.GetHotelOfferById(hotelCode, offerId))
                .Returns((HotelOffer)null);

            var result = _controller.GetOfferByHotelIdAndId(hotelCode, offerId);

            result.Should().BeOfType<NotFoundResult>();
        }

        [Test]
        public void GetOfferByHotelIdAndId_WhenServiceThrowsException_ReturnsBadRequest()
        {
            var hotelCode = "TEST123";
            var offerId = Guid.NewGuid();
            var exceptionMessage = "Test exception";
            
            _mockOfferService.Setup(x => x.GetHotelOfferById(hotelCode, offerId))
                .Throws(new Exception(exceptionMessage));

            var result = _controller.GetOfferByHotelIdAndId(hotelCode, offerId);

            result.Should().BeOfType<BadRequestObjectResult>();
            var badRequestResult = result as BadRequestObjectResult;
            badRequestResult.Value.Should().Be(exceptionMessage);
            
            _mockLogger.Verify(x => x.LogError(
                It.IsAny<string>(),
                It.Is<Exception>(e => e.Message == exceptionMessage)), Times.Once);
        }

        [Test]
        public async Task CreateOffer_WithValidModel_ReturnsCreatedResult()
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
            var expectedId = offerEntity.OfferId;
            
            _mockMapper.Setup(x => x.Map<HotelOffer>(createOfferDto))
                .Returns(offerEntity);
            
            _mockOfferService.Setup(x => x.CreateAsync(offerEntity))
                .ReturnsAsync(expectedId);

            var result = await _controller.CreateOffer(createOfferDto);

            result.Should().BeOfType<CreatedAtActionResult>();
            var createdAtResult = result;
            createdAtResult.Value.Should().Be(expectedId);
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

            result.Should().BeOfType<UnprocessableEntityObjectResult>();
        }

        [Test]
        public async Task CreateOffer_WhenServiceThrowsException_ReturnsBadRequest()
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

            result.Should().BeOfType<BadRequestObjectResult>();
            var badRequestResult = result;
            badRequestResult.Value.Should().Be(exceptionMessage);
            
            _mockLogger.Verify(x => x.LogError(
                It.IsAny<string>(),
                It.Is<Exception>(e => e.Message == exceptionMessage)), Times.Once);
        }
    }
}