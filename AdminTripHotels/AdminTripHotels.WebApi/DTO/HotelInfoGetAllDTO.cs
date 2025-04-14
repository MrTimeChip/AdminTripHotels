using AdminTripHotels.Core.Domain;

namespace AdminTripHotels.WebApi.DTO.HotelInfo;

public class HotelInfoGetAllDTO
{
    public required string Code;
    public required string Title;
    public required string Description { get; set; }
    public HotelImage ImageUrl { get; set; }
    public HotelStars Stars { get; set; }
    public required string Address { get; set; }
    public DateTime CheckInDateTime { get; set; }
    public DateTime CheckOutDateTime { get; set; }
}    