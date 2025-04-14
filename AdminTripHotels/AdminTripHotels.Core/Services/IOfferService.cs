using AdminTripHotels.Core.Domain;
using AdminTripHotels.WebApi.Utils;

namespace AdminTripHotels.Core.Services;

public interface IOfferService
{
	Task<PageList<HotelOffer>> GetOffersByHotelCode(string hotelCode, int pageNumber, int pageSize);
	HotelOffer? GetHotelOfferById(string hotelCode, Guid? id);
	Task<Guid> CreateAsync(HotelOffer offerEntity);
}