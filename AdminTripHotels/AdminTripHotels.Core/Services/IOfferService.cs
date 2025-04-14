using AdminTripHotels.Core.Domain;

namespace AdminTripHotels.Core.Services;

public interface IOfferService
{
	Task<IEnumerable<HotelOffer>> GetOffersByHotelCode(string hotelCode);
	HotelOffer? GetHotelOfferById(string hotelCode, Guid? id);
	Task<Guid> CreateAsync(HotelOffer offerEntity);
}