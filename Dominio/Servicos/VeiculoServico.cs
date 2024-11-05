using Microsoft.EntityFrameworkCore;
using MinimalApi.Dominio.Entidades;
using MinimalApi.Infraestrutura.DB;
using MinimalApi.Infraestrutura.Interfaces;

namespace MinimalApi.Dominio.Servicos
{
    public class VeiculoServico : IVeiculoServico
    {
        private readonly DbContexto _dbContexto;

        public VeiculoServico(DbContexto dbContexto)
        {
            _dbContexto = dbContexto;
        }

        public void Adicionar(Veiculo veiculo)
        {
            _dbContexto.Add(veiculo);
            _dbContexto.SaveChanges();
        }

        public void Atualizar(Veiculo veiculo)
        {
            _dbContexto.Update(veiculo);
            _dbContexto.SaveChanges();
        }

        public void Apagar(Veiculo veiculo)
        {
            _dbContexto.Veiculos.Remove(veiculo);
            _dbContexto.SaveChanges();
        }

        public Veiculo? BuscaPorId(int id)
        {
            return _dbContexto.Veiculos.Find(id);
        }

        public List<Veiculo> ListarVeiculos(int pagina = 1, string? nome = null, string? marca = null)
        {
            //    var query = _dbContexto.Veiculos
            //         .Where(v => (nome == null || v.Nome.Contains(nome)) && (marca == null || v.Marca.Contains(marca)))
            //         .Skip((pagina - 1) * 10)
            //         .Take(10)
            //         .ToList();

            var query = _dbContexto.Veiculos.AsQueryable();

            if (!string.IsNullOrEmpty(nome))
            {
                query = query.Where(v => EF.Functions.Like(v.Nome.ToLower(), $"%{nome.ToLower()}%"));
            }

            if (!string.IsNullOrEmpty(marca))
            {
                query = query.Where(v => EF.Functions.Like(v.Marca.ToLower(), $"%{marca.ToLower()}%"));
            }

            query = query.Skip((pagina - 1) * 10).Take(10);

            return query.ToList();
        }
    }
}