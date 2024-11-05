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

app.MapGet("/", () => Results.Json(new Home())).WithTags("Home");

#endregion

#region Administradores
app.MapPost("/administradores/login", ([FromBody] LoginDTO loginDto, IAdministradorServico adminService) =>
{
    if (adminService.Login(loginDto) != null)
    {
        return Results.Ok("Login Success");
    }

    return Results.Unauthorized();

}).WithTags("Administradores");
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
}).WithTags("Veiculos");

app.MapPut("/veiculos/{id}", ([FromQuery] int id, [FromBody] VeiculoDTO veiculoDto, IVeiculoServico veiculoServico) =>
{
    var veiculo = veiculoServico.BuscaPorId(id);
    if (veiculo == null)
    {
        return Results.NotFound();
    }

    veiculo.Nome = veiculoDto.Nome;
    veiculo.Marca = veiculoDto.Marca;
    veiculo.Ano = veiculoDto.Ano;

    veiculoServico.Atualizar(veiculo);

    return Results.Ok(veiculo);

}).WithTags("Veiculos");

app.MapDelete("/veiculos/{id}", ([FromRoute] int id, IVeiculoServico veiculoServico) =>
{
    var veiculo = veiculoServico.BuscaPorId(id);
    if (veiculo == null)
    {
        return Results.NotFound();
    }

    veiculoServico.Apagar(veiculo);

    return Results.NoContent();

}).WithTags("Veiculos");

app.MapGet("/veiculos", ([FromQuery] int? pagina, string? nome, string? marca, IVeiculoServico veiculoServico) =>
{
    var veiculos = veiculoServico.Listar(pagina, nome, marca);

    return Results.Ok(veiculos);
}).WithTags("Veiculos");

app.MapGet("/veiculo/{id}", ([FromRoute] int id, IVeiculoServico veiculoServico) =>
{
    var veiculo = veiculoServico.BuscaPorId(id);
    if(veiculo == null)
    {
        return Results.NotFound();
    }

    return Results.Ok(veiculo);

}).WithTags("Veiculos");

#endregion

#region App

app.UseSwagger();
app.UseSwaggerUI();

app.Run();

#endregion