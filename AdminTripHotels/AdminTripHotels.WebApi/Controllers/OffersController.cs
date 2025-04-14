using AdminTripHotels.Core.Services;
using AdminTripHotels.WebApi.DTO;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AdminTripHotels.WebApi.Controllers;

[ApiController]
[Route("api")]
public class OffersController : ControllerBase
{
	private readonly IOfferService offerService;
	private readonly IMapper mapper;
	private readonly ILogger<OffersController> logger;

	public OffersController(IOfferService offerService, IMapper mapper, ILogger<OffersController> logger)
	{
		this.offerService = offerService;
		this.mapper = mapper;
		this.logger = logger;
	}

	[HttpGet]
	public IEnumerable<OfferDTO> GetOffers()
	{
		var offers = offerService.GetAll().ToList();
		return mapper.Map<IEnumerable<OfferDTO>>(offers);
	}

	[HttpGet("hotels/{hotelCode}/offers/{offerId}")]
	[Produces("application/json")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public IActionResult GetOfferByHotelIdAndId([FromRoute]string hotelCode, [FromRoute]Guid offerId)
	{
		logger.LogInformation($"Получение предложения по ID:{offerId} для отеля:{hotelCode}");
		try
		{
			var offer = offerService.GetByHotelIdAndId(hotelCode, offerId);
			if (offer == null)
				return NotFound();

			return Ok(mapper.Map<OfferDTO>(offer));
		}
		catch (Exception e)
		{
			logger.LogError($"Ошибка при Получение предложения по ID:{offerId} для отеля:{hotelCode}", e.Message);
			return BadRequest(e.Message);
		}
	}

	public IActionResult CreateOffer([FromBody] CreateOfferDTO offer)
	{
		logger.LogInformation("");
		try
		{

		}
		catch (Exception e)
		{
			logger.LogError(e.Message);
			return BadRequest(e.Message);
		}
	}
}