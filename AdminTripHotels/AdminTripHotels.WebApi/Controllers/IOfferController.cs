using AdminTripHotels.WebApi.DTO;

namespace AdminTripHotels.WebApi.Controllers;

public interface IOfferController
{
	public IEnumerable<OfferDTO> GetOffers();
}