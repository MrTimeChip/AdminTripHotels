using AdminTripHotels.Core;
using AdminTripHotels.Core.Domain;
using AdminTripHotels.Core.Services;
using AdminTripHotels.WebApi.DTO;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddAdminTripHotelsCore();
builder.Services.AddScoped<IOfferService, OfferService>();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(config =>
{
	config.CreateMap<HotelOffer, OfferDTO>();
});

var app = builder.Build();
app.UseHttpsRedirection();
app.UseRouting();

app.UseSwagger();
app.UseSwaggerUI();

app.UseEndpoints(x => x.MapControllers());

app.Run();