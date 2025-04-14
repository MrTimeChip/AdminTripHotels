using AdminTripHotels.Core.Domain;
using AdminTripHotels.Core.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace AdminTripHotels.WebApi.Controllers;

[ApiController]
[Route("api/hotels/[action]")]
public class HotelInfoController(IRepository<HotelInfo> hotelInfoRepository) : Controller
{
    private IRepository<HotelInfo> hotelInfoRepository = hotelInfoRepository;
}