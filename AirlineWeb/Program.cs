using AirlineWeb.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AirlineDbContext>(options =>
       options.UseSqlServer(
           builder.Configuration.GetConnectionString("AirlineConnection")));
var connectionString = builder.Configuration.GetConnectionString("AirlineConnection");
Console.WriteLine($"CONEXION: '{connectionString}'"); // Esto imprimirá en la consola si es nulo o n

builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
