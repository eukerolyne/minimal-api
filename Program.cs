using Microsoft.EntityFrameworkCore;
using MinimalApi.DTOs;
using MinimalApi.Infraestrutura.DB;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEntityFrameworkSqlServer()
            .AddDbContext<DbContexto>(
            options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

//builder.Services.AddDbContext<DbContexto>(options =>
//    options.UseSqlServer(connectionString));

var app = builder.Build();

app.MapGet("/", () => "Hello World!");
//dotnet whatch run

app.MapPost("/login", (LoginDTO loginDto) =>
{
    if (loginDto.Email == "admin@teste.com" && loginDto.Password == "admin123")
    {
        return Results.Ok("Login Success");
    }

    return Results.Unauthorized();

});

app.Run();