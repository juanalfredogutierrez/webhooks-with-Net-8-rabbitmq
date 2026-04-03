using AirlineWeb.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// 1. Obtener la cadena de conexión primero
var connectionString = builder.Configuration.GetConnectionString("AirlineConnection");
Console.WriteLine($"CONEXION: '{connectionString}'");

// 2. Registro de servicios (SIN DUPLICADOS)
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(); // Solo una vez

builder.Services.AddDbContext<AirlineDbContext>(options =>
       options.UseSqlServer(connectionString));

// 3. AutoMapper (Usa esta versión que es segura para EF)
builder.Services.AddAutoMapper(typeof(Program).Assembly);

var app = builder.Build();

// 4. Pipeline de HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();
app.Run();
