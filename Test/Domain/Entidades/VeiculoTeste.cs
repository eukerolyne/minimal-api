using MinimalApi.Dominio.Entidades;

namespace Test.Domain.Entidades;

[TestClass]
public class VeiculoTeste
{
    [TestMethod]
    public void TestarGetSetPropriedades()
    {
        //Arrange
        var veiculo = new Veiculo();

        //Act
        veiculo.Id = 1;
        veiculo.Nome = "Teste";
        veiculo.Marca = "Teste marca";
        veiculo.Ano = 2024;

        //Assert
        Assert.AreEqual(1, veiculo.Id);
        Assert.AreEqual("Teste", veiculo.Nome);
        Assert.AreEqual("Teste marca", veiculo.Marca);
        Assert.AreEqual(2024, veiculo.Ano);
    }
}