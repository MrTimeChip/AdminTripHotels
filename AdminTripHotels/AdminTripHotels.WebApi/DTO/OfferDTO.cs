namespace AdminTripHotels.WebApi.DTO;

public class OfferDTO
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
	public MealDTO Meal { get; set; }

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
	public ImageDTO? ImageUrl { get; set; }

	/// <summary>
	/// Большое изображение номера
	/// </summary>
	public ImageDTO? BigImageUrl { get; set; }

	/// <summary>
	/// Миниатюра
	/// </summary>
	public ImageDTO? ThumbnailUrl { get; set; }
}