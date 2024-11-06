namespace MinimalApi.Dominio.ModelViews;

public record AdminLogado{

    public string Email { get; set; }
    public string Token { get; set; }
    public string Perfil { get; set; }
}