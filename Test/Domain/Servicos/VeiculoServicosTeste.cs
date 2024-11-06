using Microsoft.EntityFrameworkCore;
using MinimalApi.Dominio.Entidades;
using MinimalApi.Dominio.Servicos;
using MinimalApi.Infraestrutura.DB;

namespace Test.Domain.Entidades;

[TestClass]
public class VeiculoServicosTeste
{
    private DbContexto CriarContextoTeste()
    {
        var options = new DbContextOptionsBuilder<DbContexto>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        return new DbContexto(options);
    }

    [TestMethod]
    public void TestarAdicionar()
    {
        //Arrange
        var context = CriarContextoTeste();

        var veiculo = new Veiculo();
        veiculo.Nome = "Teste";
        veiculo.Marca = "Teste marca";
        veiculo.Ano = 2024;

        var servicos = new VeiculoServico(context);

        //Act
        servicos.Adicionar(veiculo);

        //Assert
        Assert.AreEqual(1, servicos.Listar(1).Count());
    }

    [TestMethod]
    public void TestarAtualizar()
    {
        //Arrange
        var context = CriarContextoTeste();

        var veiculo = new Veiculo();
        veiculo.Nome = "Teste 2";
        veiculo.Marca = "Teste-marca";
        veiculo.Ano = 2024;

        var servicos = new VeiculoServico(context);
        servicos.Adicionar(veiculo);

        var veiculoAtualizado = servicos.BuscaPorId(veiculo.Id);
        veiculoAtualizado.Nome = "atualizado";
        veiculoAtualizado.Marca = "Marca-atualizada";
        veiculoAtualizado.Ano = 2023;

        //Act
        servicos.Atualizar(veiculoAtualizado);

        //Assert
        var veiculoDB = servicos.Listar(1).FirstOrDefault(a => a.Id == veiculo.Id);
        Assert.IsNotNull(veiculoDB);
        Assert.AreEqual("atualizado", veiculoDB.Nome);
        Assert.AreEqual("Marca-atualizada", veiculoDB.Marca);
        Assert.AreEqual(2023, veiculoDB.Ano);
    }

    [TestMethod]
    public void TestarBuscaPorId()
    {
        //Arrange
        var context = CriarContextoTeste();

        var veiculo = new Veiculo();
        veiculo.Nome = "Teste";
        veiculo.Marca = "Teste-marca";
        veiculo.Ano = 2024;

        var servicos = new VeiculoServico(context);
        servicos.Adicionar(veiculo);

        //Act
        var veiculoDb = servicos.BuscaPorId(veiculo.Id);

        //Assert
        Assert.IsNotNull(veiculoDb);
        Assert.AreEqual(veiculo.Id, veiculoDb.Id);
    }

    [TestMethod]
    public void TestarApagar()
    {
        //Arrange
        var context = CriarContextoTeste();

        var veiculo = new Veiculo();
        veiculo.Nome = "Teste";
        veiculo.Marca = "Teste-marca";
        veiculo.Ano = 2024;

        var servicos = new VeiculoServico(context);
        servicos.Adicionar(veiculo);

        //Act
        servicos.Apagar(veiculo);

        //Assert
        var veiculoDb = servicos.BuscaPorId(veiculo.Id);
        Assert.IsNull(veiculoDb);
    }
}