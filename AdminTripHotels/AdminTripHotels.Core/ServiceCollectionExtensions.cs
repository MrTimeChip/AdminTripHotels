using AdminTripHotels.Core.Domain;
using AdminTripHotels.Core.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace AdminTripHotels.Core;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddAdminTripHotelsCore(this IServiceCollection services)
	{
		//Сюда добавлять сервисы
		//services.AddSingleton<>();
		services.AddDbContext<AdminTripHotelsDbContext>();
		services.AddScoped<IRepository<HotelOffer>, HotelOfferRepository>();
		services.AddScoped<IRepository<HotelInfo>, HotelInfoRepository>();
		return services;
	}
}