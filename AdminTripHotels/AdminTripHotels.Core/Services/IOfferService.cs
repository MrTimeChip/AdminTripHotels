using AdminTripHotels.Core.Domain;

namespace AdminTripHotels.Core.Services;

public interface IOfferService
{
	public IEnumerable<HotelOffer> GetAll();

	HotelOffer? GetByHotelIdAndId(string hotelCode, Guid? id);
	Task<Guid> CreateAsync(HotelOffer offerEntity);
}