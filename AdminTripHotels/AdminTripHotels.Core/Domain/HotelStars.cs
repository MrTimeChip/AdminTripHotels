namespace AdminTripHotels.Core.Domain;

[Flags]
public enum HotelStars
{
    /// <summary>
    ///     Одна звезда
    /// </summary>
    OneStar = 1,

    /// <summary>
    ///     Две звезды
    /// </summary>
    TwoStars = 2,

    /// <summary>
    ///     Три звезды
    /// </summary>
    ThreeStars = 4,

    /// <summary>
    ///     Четыре звезды
    /// </summary>
    FourStars = 8,

    /// <summary>
    ///     Пять звезд
    /// </summary>
    FiveStars = 16
}