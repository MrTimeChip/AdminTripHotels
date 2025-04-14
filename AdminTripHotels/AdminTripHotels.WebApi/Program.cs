using AdminTripHotels.Core;
using AdminTripHotels.Core.Domain;
using AdminTripHotels.Core.Services;
using AdminTripHotels.WebApi.DTO;
using AdminTripHotels.Core.Repositories;
using AdminTripHotels.WebApi.DTO.HotelInfo;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Services.AddAdminTripHotelsCore();
builder.Services.AddScoped<IOfferService, OfferService>();
builder.Services.AddScoped<IHotelInfoService, HotelInfoService>();

builder.Services.AddScoped<IRepository<HotelInfo>, HotelInfoRepository>();
builder.Services.AddScoped<IRepository<HotelOffer>, HotelOfferRepository>();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(config =>
{
    config.CreateMap<HotelOffer, OfferDTO>()
        .ForMember(dest => dest.Meal, opt => opt.MapFrom(src => new MealDTO
        {
            Id = src.Meal.Id,
            Name = src.Meal.Name,
        }))
        .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => new ImageDTO
        {
            Url = src.ImageUrl.Value.Url,
        }))
        .ForMember(dest => dest.BigImageUrl, opt => opt.MapFrom(src => new ImageDTO
        {
            Url = src.BigImageUrl.Value.Url,
        }))
        .ForMember(dest => dest.ThumbnailUrl, opt => opt.MapFrom(src => new ImageDTO
        {
            Url = src.ThumbnailUrl.Value.Url,
        }));
    ;
    config.CreateMap<HotelOffer, CreateOfferDTO>();
    config.CreateMap<CreateOfferDTO, HotelOffer>()
        .ForMember(dest => dest.OfferId, opt => opt.MapFrom(_ => Guid.NewGuid()))
        .ForMember(dest => dest.Meal, opt => opt.MapFrom(src => new HotelMeal
        {
            Id = src.Meal.Id,
            Name = src.Meal.Name,
        }))
        .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => new HotelImage
        {
            Url = src.ImageUrl.Url,
        }))
        .ForMember(dest => dest.BigImageUrl, opt => opt.MapFrom(src => new HotelImage
        {
            Url = src.BigImageUrl.Url,
        }))
        .ForMember(dest => dest.ThumbnailUrl, opt => opt.MapFrom(src => new HotelImage
        {
            Url = src.ThumbnailUrl.Url,
        }));
});
builder.Services.AddAutoMapper(config => { config.CreateMap<MealDTO, HotelMeal>(); });
builder.Services.AddAutoMapper(config => { config.CreateMap<HotelMeal, MealDTO>(); });
builder.Services.AddAutoMapper(config => { config.CreateMap<ImageDTO, HotelImage>(); });
builder.Services.AddAutoMapper(config => { config.CreateMap<HotelImage, ImageDTO>(); });
builder.Services.AddAutoMapper(config => { config.CreateMap<HotelOffer, OfferDTO>(); });
builder.Services.AddAutoMapper(config => { config.CreateMap<CreateOfferDTO, HotelOffer>(); });
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