using MinimalApi.DTOs;

var builder = WebApplication.CreateBuilder(args);
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