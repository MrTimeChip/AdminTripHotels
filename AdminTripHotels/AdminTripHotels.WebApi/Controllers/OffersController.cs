using AdminTripHotels.Core.Services;
using AdminTripHotels.WebApi.DTO;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AdminTripHotels.WebApi.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class OffersController : ControllerBase
{
	private readonly IOfferService offerService;
	private readonly IMapper mapper;

	public OffersController(IOfferService offerService, IMapper mapper)
	{
		this.offerService = offerService;
		this.mapper = mapper;
	}

	[HttpGet]
	[Route("hotels/{hotelCode}/offers")]
	public async Task<IActionResult> GetOffers([FromRoute] string hotelCode)
	{
		var offers = await offerService.GetFromHotelCode(hotelCode);
		return offers is null ? NotFound() : Ok(mapper.Map<IEnumerable<OfferDTO>>(offers));
	}
}