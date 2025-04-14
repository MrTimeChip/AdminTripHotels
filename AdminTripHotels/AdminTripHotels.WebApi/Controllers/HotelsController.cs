using AdminTripHotels.Core.Domain;
using Microsoft.AspNetCore.Mvc;

namespace AdminTripHotels.WebApi.Controllers;

[Route("hotels")]
public class HotelsController : ControllerBase
{
    [HttpPost]
    [Route("create")]
    public async Task<IActionResult> CreateHotel([FromBody] HotelInfo hotelInfo)
    {
        return Ok(hotelInfo);
    }
    
    [HttpPost]
    [Route("create-many")]
    public async Task<IActionResult> CreateHotels([FromBody] HotelInfo hotelInfo)
    {
        return Ok(hotelInfo);
    }    
}