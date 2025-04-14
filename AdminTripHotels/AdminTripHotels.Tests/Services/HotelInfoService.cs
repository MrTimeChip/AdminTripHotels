using AdminTripHotels.Core;
using AdminTripHotels.Core.Domain;
using AdminTripHotels.Core.Repositories;
using AdminTripHotels.Core.Services;
using Microsoft.Extensions.Logging;

namespace AdminTripHotels.Tests.Services;

public class HotelInfoService
{
    private OfferService service;
    private IRepository<HotelInfo> hotelInfoRepository;
    private IRepository<HotelOffer> hotelOfferRepository;
    private ILogger<OfferService> logger;
    private AdminTripHotelsDbContext dbContext;
    private LoggerFactory factory;

    [SetUp]
    public void Setup()
    {
        factory = new LoggerFactory();
        hotelInfoRepository = new HotelInfoRepository(dbContext);
        hotelOfferRepository = new HotelOfferRepository(dbContext);
        logger = new Logger<OfferService>(factory);
        service = new OfferService(hotelOfferRepository, hotelInfoRepository, logger);
    }

    [TearDown]
    public void TearDown()
    {
        dbContext.Dispose();
        factory.Dispose();
    }

}