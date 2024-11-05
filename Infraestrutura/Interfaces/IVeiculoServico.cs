using MinimalApi.Dominio.Entidades;

namespace MinimalApi.Infraestrutura.Interfaces
{
    public interface IVeiculoServico
    {
        List<Veiculo> ListarVeiculos(int pagina = 1, string? nome = null, string? marca = null);
        Veiculo? BuscaPorId(int id);

        void Adicionar(Veiculo veiculo);

        void Atualizar(Veiculo veiculo);

        void Apagar(Veiculo veiculo);

    }
}