using AdminTripHotels.Core.Domain;
using AdminTripHotels.Core.Repositories;
using AdminTripHotels.WebApi.DTO.HotelInfo;
using AdminTripHotels.WebApi.Utils;
using Microsoft.AspNetCore.Mvc;

namespace AdminTripHotels.WebApi.Controllers;

[ApiController]
[Route("api/hotels/[action]")]
public class HotelInfoController(IRepository<HotelInfo> hotelInfoRepository) : Controller
{
    private IRepository<HotelInfo> hotelInfoRepository = hotelInfoRepository;


    [HttpGet]
    [Produces("application/json")]
    public ActionResult<PageList<HotelInfo>> GetAllHotelInfos([FromQuery] int skip = 0, [FromQuery] int take = 10)
    {
        var hotels = hotelInfoRepository.GetAll();
        var totalCount = hotels.Count();
        var pageHotels = hotels.Skip(skip).Take(take).ToList();
        var pageList = new PageList<HotelInfo>(pageHotels, totalCount, skip, take);
        return pageList;
    }
}