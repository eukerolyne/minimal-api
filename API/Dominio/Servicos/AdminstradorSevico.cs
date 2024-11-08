using MinimalApi.Dominio.Entidades;
using MinimalApi.DTOs;
using MinimalApi.Infraestrutura.DB;
using MinimalApi.Infraestrutura.Interfaces;

namespace MinimalApi.Dominio.Servicos
{
    public class AdministradorServico : IAdministradorServico
    {
        private readonly DbContexto _dbContexto;

        public AdministradorServico(DbContexto dbContexto)
        {
            _dbContexto = dbContexto;
        }

        public Administrador? Login(LoginDTO loginDto)
        {
            var administrador = _dbContexto.Administradores.Where(admin => admin.Email == loginDto.Email && admin.Senha == loginDto.Password).FirstOrDefault();
            if (administrador == null)
            {
                return null;
            }

            return administrador;
        }

        public void Adicionar(Administrador administrador)
        {
            _dbContexto.Administradores.Add(administrador);
            _dbContexto.SaveChanges();
        }

        public List<Administrador> Listar(int? pagina = 1)
        {
            var query = _dbContexto.Administradores.AsQueryable();

            if (pagina != null)
            {
                query = query.Skip(((int)pagina - 1) * 10).Take(10);
            }

           return query.ToList();
        }

        public Administrador? BuscaPorId(int id)
        {
            var administrador = _dbContexto.Administradores.Find(id);
            if (administrador == null)
            {
                return null;
            }

            return administrador;
        }
    }
}