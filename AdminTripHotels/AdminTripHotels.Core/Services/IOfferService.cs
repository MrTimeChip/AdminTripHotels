using AdminTripHotels.Core.Domain;

namespace AdminTripHotels.Core.Services;

public interface IOfferService
{
	public IEnumerable<HotelOffer> GetAll();
}