using System.ComponentModel.DataAnnotations;

namespace AdminTripHotels.WebApi.DTO;

public class CreateOfferDTO
{
	/// <summary>
	/// Название предложения
	/// </summary>
	[Required]
	public required string Title { get; set; }

	// Описание предложения
	public string? Description { get; set; }

	// Питание
	public MealDTO? Meal { get; set; }

	// Количество оставшихся предложений
	public int? RoomsRemained { get; set; }

	// Стоимость предложения
	[Required]
	public decimal TotalPrice { get; set; }

	// Число дополнительных кроватей
	public int ExtraBeds { get; set; }

	// Изображение номера
	public ImageDTO? ImageUrl { get; set; }

	// Большое изображение номера
	public ImageDTO? BigImageUrl { get; set; }

	// Миниатюра
	public ImageDTO? ThumbnailUrl { get; set; }
}