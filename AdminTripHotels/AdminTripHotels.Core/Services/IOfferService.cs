using AdminTripHotels.Core.Domain;

namespace AdminTripHotels.Core.Services;

public interface IOfferService
{
	public Task<IEnumerable<HotelOffer?>> GetFromHotelCode(string hotelCode);
}