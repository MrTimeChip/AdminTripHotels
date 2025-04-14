using AdminTripHotels.Core.Domain;
using AdminTripHotels.Core.Services;
using AdminTripHotels.WebApi.DTO.HotelInfo;
using AdminTripHotels.WebApi.Utils;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AdminTripHotels.WebApi.Controllers;

[ApiController]
[Route("api/hotels")]
public class HotelInfoController(IHotelInfoService hotelInfoService, IMapper mapper) : Controller
{
    [HttpGet]
    [Produces("application/json")]
    public ActionResult<PageList<HotelInfoGetAllDTO>> GetAllHotelInfos([FromQuery] int skip = 0,
        [FromQuery] int take = 10)
    {
        var hotels = hotelInfoService.GetByPage(skip, take);
        var pageHotelsDto = mapper.Map<List<HotelInfoGetAllDTO>>(hotels);
        var pageList =
            new PageList<HotelInfoGetAllDTO>(pageHotelsDto, hotels.TotalCount, hotels.CurrentPage, hotels.PageSize);
        return Ok(pageList);
    }

    [HttpGet]
    [Route("{hotelCode}", Name = "GetHotelInfoById")]
    [Produces("application/json")]
    public ActionResult<HotelInfoDefaultDTO> GetHotelInfoById([FromRoute] string hotelCode)
    {
        var hotelInfo = hotelInfoService.GetByCode(hotelCode);
        var hotel = mapper.Map<HotelInfoDefaultDTO>(hotelInfo);
        return Ok(hotel);
    }

    [HttpPost]
    [Produces("application/json")]
    public ActionResult<HotelInfoDefaultDTO> CreateHotel([FromBody] HotelInfoDefaultDTO hotel)
    {
        var mappedHotel = mapper.Map<HotelInfo>(hotel);
        hotelInfoService.Add(mappedHotel);
        return CreatedAtRoute(nameof(GetHotelInfoById), new { hotelCode = mappedHotel.Code }, hotel);
    }

    [HttpPut]
    [Route("{hotelCode}")]
    [Produces("application/json")]
    public ActionResult<HotelInfoDefaultDTO> UpdateHotel([FromBody] HotelInfoDefaultDTO hotel,
        [FromRoute] string hotelCode)
    {
        var mappedHotel = mapper.Map<HotelInfo>(hotel);
        hotelInfoService.Update(mappedHotel);
        return Ok(mappedHotel);
    }

    [HttpDelete]
    [Route("{hotelCode}")]
    [Produces("application/json")]
    public ActionResult<HotelInfoDefaultDTO> DeleteHotel([FromRoute] string hotelCode)
    {
        hotelInfoService.Delete(hotelCode);
        return NoContent();
    }
}