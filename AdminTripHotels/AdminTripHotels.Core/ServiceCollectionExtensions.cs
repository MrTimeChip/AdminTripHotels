using AdminTripHotels.Core.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace AdminTripHotels.Core;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddAdminTripHotelsCore(this IServiceCollection services)
	{
		//Сюда добавлять сервисы
		//services.AddSingleton<>();
		return services;
	}
}