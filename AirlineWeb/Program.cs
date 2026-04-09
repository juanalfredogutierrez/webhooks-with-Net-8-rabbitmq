using AirlineWeb.Data;
using AirlineWeb.MessageBus;

using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("AirlineConnection");
Console.WriteLine($"CONEXION: '{connectionString}'");

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AirlineDbContext>(options =>
       options.UseSqlServer(connectionString));

builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddSingleton<IMessageBusClient,MessageBusClient>();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseStaticFiles();
app.UseAuthorization();
app.UseStaticFiles();
app.MapControllers();
app.Run();
