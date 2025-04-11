namespace AdminTripHotels.Core.Domain;

/// <summary>
/// Тариф отеля
/// </summary>
public class HotelOffer
{
    public Guid OfferId { get; set; }

    /// <summary>
    ///     Название предложения
    /// </summary>
    public required string Title { get; set; }

    /// <summary>
    ///     Описание предложения
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    ///     Питание
    /// </summary>
    public HotelMeal Meal { get; set; }

    /// <summary>
    ///     Количество оставшихся предложений
    /// </summary>
    public int? RoomsRemained { get; set; }

    /// <summary>
    ///     Стоимость предложения
    /// </summary>
    public decimal TotalPrice { get; set; }

    /// <summary>
    /// Число дополнительных кроватей
    /// </summary>
    public int ExtraBeds { get; set; }

    /// <summary>
    /// Изображение номера
    /// </summary>
    public HotelImage? ImageUrl { get; set; }

    /// <summary>
    /// Большое изображение номера
    /// </summary>
    public HotelImage? BigImageUrl { get; set; }

    /// <summary>
    /// Миниатюра
    /// </summary>
    public HotelImage? ThumbnailUrl { get; set; }
}