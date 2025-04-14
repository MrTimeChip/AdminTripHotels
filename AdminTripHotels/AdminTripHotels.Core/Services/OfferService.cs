using AdminTripHotels.Core.Domain;
using AdminTripHotels.Core.Repositories;

namespace AdminTripHotels.Core.Services;

public class OfferService : IOfferService
{
	private readonly IRepository<HotelOffer> hotelOfferRepository;
	private readonly IRepository<HotelInfo> hotelInfoRepository;

	public OfferService(IRepository<HotelOffer> hotelOfferRepository, IRepository<HotelInfo> hotelInfoRepository)
	{
		this.hotelOfferRepository = hotelOfferRepository;
		this.hotelInfoRepository = hotelInfoRepository;
	}

	public IEnumerable<HotelOffer> GetAll()
	{
		return hotelOfferRepository.GetAll();
	}

	public async Task<IEnumerable<HotelOffer?>> GetFromHotelCode(string code)
	{
		return await Task.FromResult(hotelInfoRepository.GetAll().FirstOrDefault(h => h.Code == code)?.Offers);
	}
}