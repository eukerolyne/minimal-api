using MinimalApi.Dominio.Enuns;

namespace MinimalApi.Dominio.ModelViews
{
    public record AdministradorModelView
    {
        public int Id { get; set; }
        public string Email { get; set; } 
        public Perfil Perfil { get; set; } 
    }
}