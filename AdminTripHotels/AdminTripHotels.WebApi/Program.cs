using AdminTripHotels.Core;
using AdminTripHotels.Core.Domain;
using AdminTripHotels.Core.Services;
using AdminTripHotels.WebApi.DTO;
using AdminTripHotels.Core.Repositories;
using AdminTripHotels.WebApi.DTO.HotelInfo;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddAdminTripHotelsCore();
builder.Services.AddScoped<IOfferService, OfferService>();
builder.Services.AddScoped<IHotelInfoService, HotelInfoService>();

builder.Services.AddScoped<IRepository<HotelInfo>, HotelInfoRepository>();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(config => { config.CreateMap<HotelOffer, OfferDTO>(); });
builder.Services.AddAutoMapper(config => { config.CreateMap<HotelInfo, HotelInfoDefaultDTO>(); });
builder.Services.AddAutoMapper(config => { config.CreateMap<HotelInfo, HotelInfoGetAllDTO>(); });
builder.Services.AddAutoMapper(config => { config.CreateMap<HotelInfoDefaultDTO, HotelInfo>(); });

var app = builder.Build();
app.UseHttpsRedirection();
app.UseRouting();

app.UseSwagger();
app.UseSwaggerUI();

app.UseEndpoints(x => x.MapControllers());

app.Run();