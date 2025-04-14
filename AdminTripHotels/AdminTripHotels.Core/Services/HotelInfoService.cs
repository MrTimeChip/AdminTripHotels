using AdminTripHotels.Core.Domain;
using AdminTripHotels.Core.Repositories;
using AdminTripHotels.WebApi.Utils;

namespace AdminTripHotels.Core.Services;

public class HotelInfoService(IRepository<HotelInfo> hotelInfoRepository) : IHotelInfoService
{
    public HotelInfo GetByCode(string code)
    {
        var hotel = hotelInfoRepository
            .GetAll()
            .First(h => h.Code == code);
        return hotel;
    }

    public IEnumerable<HotelInfo> GetAllHotels()
    {
        var hotels = hotelInfoRepository.GetAll();
        return hotels.ToList();
    }

    public PageList<HotelInfo> GetByPage(int skip, int take)
    {
        var allHotels = hotelInfoRepository.GetAll();
        var totalCount = allHotels.Count();
        var hotels = allHotels
            .Skip(skip)
            .Take(take)
            .ToList();
        var hotelsPage = new PageList<HotelInfo>(hotels, totalCount, skip, take);
        return hotelsPage;
    }

    public void Add(HotelInfo hotelInfo)
    {
        hotelInfoRepository.AddAsync(hotelInfo);
    }

    public void Update(HotelInfo hotelInfo)
    {
        hotelInfoRepository.UpdateAsync(hotelInfo);
    }

    public void Delete(string hotelCode)
    {
        var hotel = GetByCode(hotelCode);
        hotelInfoRepository.DeleteAsync(hotel);
    }
}