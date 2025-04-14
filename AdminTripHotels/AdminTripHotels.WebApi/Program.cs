using AdminTripHotels.Core;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAdminTripHotelsCore();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseHttpsRedirection();
app.UseRouting();

app.UseSwagger();
app.UseSwaggerUI();

app.UseEndpoints(x => x.MapControllers());

app.Run();