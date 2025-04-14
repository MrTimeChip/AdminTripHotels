using AdminTripHotels.Core.Services;
using AdminTripHotels.WebApi.DTO;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AdminTripHotels.WebApi.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class OffersController : IOfferController
{
	private readonly IOfferService offerService;
	private readonly IMapper mapper;

	public OffersController(IOfferService offerService, IMapper mapper)
	{
		this.offerService = offerService;
		this.mapper = mapper;
	}

	[HttpGet]
	public IEnumerable<OfferDTO> GetOffers()
	{
		var offers = offerService.GetAll().ToList();
		return mapper.Map<IEnumerable<OfferDTO>>(offers);
	}
}