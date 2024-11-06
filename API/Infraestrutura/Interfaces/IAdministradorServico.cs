using MinimalApi.Dominio.Entidades;
using MinimalApi.DTOs;

namespace MinimalApi.Infraestrutura.Interfaces
{
    public interface IAdministradorServico
    {
        Administrador? Login(LoginDTO loginDto);

        List<Administrador> Listar(int? pagina);

        Administrador? BuscaPorId(int id);

        void Adicionar(Administrador administrador);
        void Apagar(Administrador administrador);
    }
}