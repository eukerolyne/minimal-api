using MinimalApi.DTOs;
using Microsoft.AspNetCore.Mvc;
using MinimalApi.Dominio.Servicos;
using MinimalApi.Infraestrutura.DB;
using Microsoft.EntityFrameworkCore;
using MinimalApi.Infraestrutura.Interfaces;
using MinimalApi.Dominio.ModelViews;
using MinimalApi.Dominio.Entidades;

#region Builder

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddEntityFrameworkSqlServer()
            .AddDbContext<DbContexto>(
            options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IAdministradorServico, AdministradorServico>();
builder.Services.AddScoped<IVeiculoServico, VeiculoServico>();

#endregion

#region Home

var app = builder.Build();

app.MapGet("/", () => Results.Json(new Home()));

#endregion

#region Administradores
app.MapPost("/administradores/login", ([FromBody] LoginDTO loginDto, IAdministradorServico adminService) =>
{
    if (adminService.Login(loginDto) != null)
    {
        return Results.Ok("Login Success");
    }

    return Results.Unauthorized();

});
#endregion

#region Veiculos

app.MapPost("/veiculos/adicionar", ([FromBody] VeiculoDTO veiculoDto, IVeiculoServico veiculoServico) =>
{
    var veiculo = new Veiculo
    {
        Nome = veiculoDto.Nome,
        Marca = veiculoDto.Marca,
        Ano = veiculoDto.Ano
    };

    veiculoServico.Adicionar(veiculo);

    return Results.Created($"/veiculos/{veiculo.Id}", veiculo);
});

app.MapGet("/veiculos", ([FromQuery] int? pagina, string? nome, string? marca, IVeiculoServico veiculoServico) =>
{
    var veiculos = veiculoServico.Listar(pagina, nome, marca);

    return Results.Ok(veiculos);
});

#endregion

#region App

app.UseSwagger();
app.UseSwaggerUI();

app.Run();

#endregion