using AdminTripHotels.Core.Domain;
using AdminTripHotels.Core.Services;
using AdminTripHotels.WebApi.DTO;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

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
	[Route("hotels/{hotelCode}/offers")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<ActionResult<IEnumerable<OfferDTO>>> GetOffers([FromRoute]string hotelCode,
		[FromQuery]int pageNumber = 1, [FromQuery]int pageSize = 3)
	{
		logger.LogInformation($"Получение всех предложений по отелю {hotelCode}");
		if (pageNumber < 1)
			pageNumber = 1;

		if (pageSize < 1)
			pageSize = 1;

		if (pageSize > 10)
			pageSize = 10;
		
		var offers = await offerService.GetOffersByHotelCode(hotelCode, pageNumber, pageSize);
		if (offers == null)
		{
			logger.LogError($"Не найден отель {hotelCode} или у отеля нет предложений");
			return NotFound();
		}
			
		
		var paginationHeader = new
		{
			totalCount = offers.TotalCount,
			pageSize,
			currentPage = offers.CurrentPage,
			totalPages = offers.TotalPages,
		};
		Response.Headers["X-Pagination"] = JsonConvert.SerializeObject(paginationHeader);
		
		return Ok(mapper.Map<IEnumerable<OfferDTO>>(offers));
	}

	[HttpGet("hotels/{hotelCode}/offers/{offerId}")]
	[Produces("application/json")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	public IActionResult GetOfferByHotelIdAndId([FromRoute]string hotelCode, [FromRoute]Guid offerId)
	{
		logger.LogInformation($"Получение предложения по ID:{offerId} для отеля:{hotelCode}");
		try
		{
			var offer = offerService.GetHotelOfferById(hotelCode, offerId);
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

	[HttpPost("hotels/{hotelCode}/offers")]
	[Produces("application/json")]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status201Created)]
	public async Task<ActionResult<Guid>> CreateOffer([FromBody] CreateOfferDTO createOfferDto)
	{
		logger.LogInformation("Получение ");
		try
		{
			if (!ModelState.IsValid)
				return UnprocessableEntity(ModelState);
			var offerEntity = mapper.Map<HotelOffer>(createOfferDto);
			var offerId = await offerService.CreateAsync(offerEntity);

			return CreatedAtRoute("name", new {offer = offerId},  offerId);
		}
		catch (Exception e)
		{
			logger.LogError(e.Message);
			return BadRequest(e.Message);
		}
	}

	[HttpDelete("hotels/{hotelCode}/offers/{offerId}")]
	[Produces("application/json")]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status201Created)]
	public async Task<IActionResult> DeleteOffer([FromRoute] string hotelCode, [FromRoute] Guid offerId)
	{
		logger.LogInformation("Удаление предложения.");
		try
		{
			if (!ModelState.IsValid)
				return UnprocessableEntity(ModelState);
			await offerService.DeleteAsync(hotelCode, offerId);

			return Ok();
		}
		catch (Exception e)
		{
			logger.LogError(e.Message);
			return BadRequest(e.Message);
		}
	}

	[HttpPut("hotels/{hotelCode}/offers/{offerId}")]
	[Produces("application/json")]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status200OK)]
	public async Task<IActionResult> UpdateOffer([FromRoute] string hotelCode, [FromRoute] Guid offerId)
	{
		logger.LogInformation("Удаление предложения.");
		try
		{
			if (!ModelState.IsValid)
				return UnprocessableEntity(ModelState);
			await offerService.DeleteAsync(hotelCode, offerId);

			return Ok();
		}
		catch (Exception e)
		{
			logger.LogError(e.Message);
			return BadRequest(e.Message);
		}
	}
}