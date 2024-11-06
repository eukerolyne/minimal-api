using Microsoft.EntityFrameworkCore;
using MinimalApi.Dominio.Entidades;

namespace MinimalApi.Infraestrutura.DB;

public class DbContexto : DbContext
{
    public DbContexto(DbContextOptions<DbContexto> options)
            : base(options)
    {
    }

    public DbSet<Administrador> Administradores { get; set; } = default!;
     public DbSet<Veiculo> Veiculos { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Administrador>().HasData(
            new Administrador { 
                Id = 1, 
                Email = "admin@teste.com", 
                Senha = "admin123", 
                Perfil = "Admin" });
    }
}