using AdminTripHotels.Core.Domain;
using AdminTripHotels.Core.Repositories;

namespace AdminTripHotels.Core.Services;

public class OfferService : IOfferService
{
	private readonly IRepository<HotelOffer> hotelOfferRepository;

	public OfferService(IRepository<HotelOffer> hotelOfferRepository)
	{
		this.hotelOfferRepository = hotelOfferRepository;
	}

	public IEnumerable<HotelOffer> GetAll()
	{
		return hotelOfferRepository.GetAll();
	}
}