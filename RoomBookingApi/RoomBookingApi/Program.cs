using RoomBookingApi.Repositories;
using RoomBookingApi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddSingleton<IRoomRepository, RoomRepository>();
builder.Services.AddSingleton<IReservationRepository, ReservationRepository>();
builder.Services.AddTransient<IRoomService, RoomService>();
builder.Services.AddTransient<IReservationService, ReservationService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options => { options.SwaggerEndpoint("/openapi/v1.json", "RoomBookingApi v1"); });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();