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
    }
}