using AdminTripHotels.Core.Domain;
using AdminTripHotels.Core.Repositories;
using Microsoft.Extensions.Logging;

namespace AdminTripHotels.Core.Services;

public class OfferService : IOfferService
{
	private readonly IRepository<HotelOffer> hotelOfferRepository;
	private readonly IRepository<HotelInfo>  hotelInfoRepository;
	private readonly ILogger<OfferService> logger;

	public OfferService(IRepository<HotelOffer> hotelOfferRepository, IRepository<HotelInfo> hotelInfoRepository, ILogger<OfferService> logger)
	{
		this.hotelOfferRepository = hotelOfferRepository;
		this.hotelInfoRepository = hotelInfoRepository;
		this.logger = logger;
	}

	public async Task<IEnumerable<HotelOffer>> GetOffersByHotelCode(string hotelCode)
	{
		var hotelInfos = hotelInfoRepository.GetAll();
		return await Task.FromResult(hotelInfoRepository.GetAll()
			.FirstOrDefault(h => h.Code == hotelCode)?.Offers ?? Array.Empty<HotelOffer>());
	}

	public HotelOffer? GetHotelOfferById(string? hotelCode, Guid? id)
	{
		if(string.IsNullOrEmpty(hotelCode))
			throw new ArgumentNullException(nameof(hotelCode));

		var hotelInfo = hotelInfoRepository.GetAll()
			.FirstOrDefault(x => x.Code == hotelCode);

		if(hotelInfo == null)
			throw new ArgumentNullException(nameof(hotelInfo));

		if (id == null)
			throw new ArgumentNullException(nameof(id));

		return hotelOfferRepository.GetAll()
			.FirstOrDefault(x => x.OfferId == id);
	}

	public async Task<Guid> CreateAsync(HotelOffer offerEntity)
	{
		await hotelOfferRepository.AddAsync(offerEntity);

		return offerEntity.OfferId;
	}

	public Task DeleteAsync(string hotelCode, Guid offerId)
	{
		throw new NotImplementedException();
	}
}