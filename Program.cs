using MinimalApi.DTOs;
using Microsoft.AspNetCore.Mvc;
using MinimalApi.Dominio.Servicos;
using MinimalApi.Infraestrutura.DB;
using Microsoft.EntityFrameworkCore;
using MinimalApi.Infraestrutura.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddEntityFrameworkSqlServer()
            .AddDbContext<DbContexto>(
            options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddScoped<IAdministradorServico, AdministradorServico>();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapPost("/login", ([FromBody] LoginDTO loginDto, IAdministradorServico adminService) =>
{
    if (adminService.Login(loginDto) != null)
    {
        return Results.Ok("Login Success");
    }

    return Results.Unauthorized();

});

app.UseSwagger();
app.UseSwaggerUI();

app.Run();