using AdminTripHotels.Core.Domain;
using AdminTripHotels.WebApi.Utils;

namespace AdminTripHotels.Core.Services;

public interface IHotelInfoService
{
    public HotelInfo GetByCode(string code);
    public IEnumerable<HotelInfo> GetAllHotels();
    public PageList<HotelInfo> GetByPage(int skip, int take);
    public void Add(HotelInfo hotelInfo);
    public void Update(HotelInfo hotelInfo);
    public void Delete(string hotelCode);
}