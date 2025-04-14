using AdminTripHotels.Core.Domain;
using AdminTripHotels.Core.Repositories;
using AdminTripHotels.WebApi.Utils;
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

	public async Task<PageList<HotelOffer>> GetOffersByHotelCode(string hotelCode, int pageNumber, int pageSize)
	{
		var hotel = hotelInfoRepository.GetAll().FirstOrDefault(h => h.Code == hotelCode);

		if (hotel == null || hotel.Offers == null)
			return null;

		var count = hotel.Offers.Count();

		var offers = hotel.Offers
			.OrderBy(o => o.OfferId)
			.Skip((pageNumber - 1) * pageSize)
			.Take(pageSize)
			.Select(o => Clone(o.OfferId, o))
			.ToList();

		return await Task.FromResult(
			new PageList<HotelOffer>(offers, count, pageNumber, pageSize)
		);
	}

	public HotelOffer Clone(Guid id, HotelOffer offer)
	{
		return new HotelOffer
		{
			OfferId = id, Title = offer.Title, Description = offer.Description,
			Meal = offer.Meal, ExtraBeds = offer.ExtraBeds, ImageUrl = offer.ImageUrl,
			ThumbnailUrl = offer.ThumbnailUrl, BigImageUrl = offer.BigImageUrl, RoomsRemained = offer.RoomsRemained,
			TotalPrice = offer.TotalPrice
		};
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