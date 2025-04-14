using AdminTripHotels.Core.Domain;

namespace AdminTripHotels.WebApi.DTO.HotelInfo;

public class HotelInfoDefaultDTO
{
    public required string Code { get; set; }
    public HotelImage? BigImageUrl { get; set; }
    public HotelImage? ThumbnailUrl { get; set; }
    public DateTime CheckInDateTime { get; set; }
    public DateTime CheckOutDateTime { get; set; }
    public TimeOnly DefaultArrivalTime { get; set; }
    public TimeOnly DefaultDepartureTime { get; set; }
    public string? RailwayDistances { get; set; }
    public string? CityCentreDistance { get; set; }
    public string? UndergroundDistances { get; set; }
    public string? AirportsDistance { get; set; }
    public required string Description { get; set; }
    public required string Title { get; set; }
    public HotelImage? ImageUrl { get; set; }
    public HotelStars Stars { get; set; }
    public GeoPoint? Position { get; set; }
    public required string Address { get; set; }
    public IEnumerable<HotelOffer>? Offers { get; set; }
}