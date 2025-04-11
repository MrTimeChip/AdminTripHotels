namespace AdminTripHotels.Core.Domain;

/// <summary>
///     Информация об отеле
/// </summary>
public class HotelInfo
{
    /// <summary>
    /// Код гостиницы
    /// </summary>
    public required string Code { get; set; }

    /// <summary>
    /// Большая по размеру картинка
    /// </summary>
    public HotelImage? BigImageUrl { get; set; }

    /// <summary>
    /// Миниатюра
    /// </summary>
    public HotelImage? ThumbnailUrl { get; set; }

    /// <summary>
    /// Дата заселения
    /// </summary>
    public DateTime CheckInDateTime { get; set; }

    /// <summary>
    /// Дата выселения
    /// </summary>
    public DateTime CheckOutDateTime { get; set; }

    /// <summary>
    /// Время заселения по умолчанию (Check-In)
    /// </summary>
    public TimeOnly DefaultArrivalTime { get; set; }

    /// <summary>
    /// Время отбытия по умолчанию (Check-Out)
    /// </summary>
    public TimeOnly DefaultDepartureTime { get; set; }

    /// <summary>
    /// Расстояния до ближайших жд станции
    /// </summary>
    public string? RailwayDistances { get; set; }

    /// <summary>
    /// Раcстояния до центра города, текстом
    /// </summary>
    public string? CityCentreDistance { get; set; }

    /// <summary>
    /// Расстояния до ближайших станции метро, текстом
    /// </summary>
    public string? UndergroundDistances { get; set; }

    /// <summary>
    /// Расстояния до аэропортов
    /// </summary>
    public string? AirportsDistance { get; set; }

    /// <summary>
    /// Описание гостиницы
    /// </summary>
    public required string Description { get; set; }

    /// <summary>
    /// Название
    /// </summary>
    public required string Title { get; set; }

    /// <summary>
    /// Адрес изображения
    /// </summary>
    public HotelImage? ImageUrl { get; set; }

    /// <summary>
    /// Кол-во звезд у отеля
    /// </summary>
    public HotelStars Stars { get; set; }

    /// <summary>
    /// Позиция отеля на карте
    /// </summary>
    public GeoPoint? Position { get; set; }

    /// <summary>
    /// Адрес гостиницы
    /// </summary>
    public required string Address { get; set; }

    /// <summary>
    /// Предложения
    /// </summary>
    public IEnumerable<HotelOffer>? Offers { get; set; }
}