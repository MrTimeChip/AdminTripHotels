using AdminTripHotels.WebApi.DTO;
using Microsoft.AspNetCore.Mvc;

namespace AdminTripHotels.WebApi.Controllers;

public interface IOfferController
{
	public Task<IActionResult> GetOffers(string hotelCode);
}