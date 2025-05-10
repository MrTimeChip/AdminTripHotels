using Microsoft.AspNetCore.Mvc;

namespace AdminTripHotels.WebApi.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class DefaultController : IDefaultController
{
	public DefaultController()
	{

	}

	[HttpGet]
	public async Task<string> Ping()
	{
		return await Task.FromResult("Pong");
	}
}