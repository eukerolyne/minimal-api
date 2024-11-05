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
}