using MinimalApi.DTOs;
using Microsoft.AspNetCore.Mvc;
using MinimalApi.Dominio.Servicos;
using MinimalApi.Infraestrutura.DB;
using Microsoft.EntityFrameworkCore;
using MinimalApi.Infraestrutura.Interfaces;
using MinimalApi.Dominio.ModelViews;
using MinimalApi.Dominio.Entidades;
using MinimalApi.Dominio.Enuns;

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

ErrosValidacao ValidaUsersDto(AdministradorDTO administradorDto)
{
    var validacao = new ErrosValidacao
    {
        Mensagens = new List<string>()
    };

    if (string.IsNullOrEmpty(administradorDto.Email))
    {
        validacao.Mensagens.Add("Email é obrigatório");
    }

    if (string.IsNullOrEmpty(administradorDto.Senha))
    {
        validacao.Mensagens.Add("Senha é obrigatória");
    }

    if (administradorDto.Perfil == null)
    {
        validacao.Mensagens.Add("Selecione um perfil");
    }

    return validacao;
}

app.MapPost("/administradores/login", ([FromBody] LoginDTO loginDto, IAdministradorServico adminService) =>
{
    if (adminService.Login(loginDto) != null)
    {
        return Results.Ok("Login Success");
    }

    return Results.Unauthorized();

}).WithTags("Administradores");

app.MapPost("/administradores/adicionar", ([FromBody] AdministradorDTO adminDto, IAdministradorServico adminService) =>
{
    var validacao = ValidaUsersDto(adminDto);
    if (validacao.Mensagens.Count() > 0)
    {
        return Results.BadRequest(validacao);
    }

    var admin = new Administrador
    {
        Email = adminDto.Email,
        Senha = adminDto.Senha,
        Perfil = adminDto.Perfil.ToString() ?? Perfil.Editor.ToString()
    };

    adminService.Adicionar(admin);

    return Results.Created($"/administradores/{admin.Id}", admin);

}).WithTags("Administradores");

app.MapGet("/administradores", ([FromQuery] int? pagina, IAdministradorServico adminService) =>
{
    var admins = new List<AdministradorModelView>();
    var administradores = adminService.Listar(pagina);
    foreach (var admin in administradores)
    {
        admins.Add(new AdministradorModelView
        {
            Id = admin.Id,
            Email = admin.Email,
            Perfil = (Perfil)Enum.Parse(typeof(Perfil), admin.Perfil)
        });
    }

    return Results.Ok(administradores);
}).WithTags("Administradores");

app.MapGet("/administradores/{id}", ([FromRoute] int id, IAdministradorServico adminService) =>
{
    var admin = adminService.BuscaPorId(id);
    if (admin == null)
    {
        return Results.NotFound();
    }

    var adminModelView = new AdministradorModelView
    {
        Id = admin.Id,
        Email = admin.Email,
        Perfil = (Perfil)Enum.Parse(typeof(Perfil), admin.Perfil)
    };

    return Results.Ok(adminModelView);

}).WithTags("Administradores");

#endregion

#region Veiculos

ErrosValidacao ValidaVeiculoDto(VeiculoDTO veiculoDto)
{
    var validacao = new ErrosValidacao
    {
        Mensagens = new List<string>()
    };

    if (string.IsNullOrEmpty(veiculoDto.Nome))
    {
        validacao.Mensagens.Add("Nome é obrigatório");
    }

    if (string.IsNullOrEmpty(veiculoDto.Marca))
    {
        validacao.Mensagens.Add("Marca é obrigatória");
    }

    if (veiculoDto.Ano < 1950)
    {
        validacao.Mensagens.Add("Aceitamos veículos a partir de 1950");
    }

    return validacao;
}

app.MapPost("/veiculos/adicionar", ([FromBody] VeiculoDTO veiculoDto, IVeiculoServico veiculoServico) =>
{
    var validacao = ValidaVeiculoDto(veiculoDto);
    if (validacao.Mensagens.Count() > 0)
    {
        return Results.BadRequest(validacao);
    }

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

    var validacao = ValidaVeiculoDto(veiculoDto);
    if (validacao.Mensagens.Count() > 0)
    {
        return Results.BadRequest(validacao);
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
    if (veiculo == null)
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