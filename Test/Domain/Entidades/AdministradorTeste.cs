using MinimalApi.Dominio.Entidades;

namespace Test.Domain.Entidades;

[TestClass]
public class AdministradorTeste
{
    [TestMethod]
    public void TestarGetSetPropriedades()
    {
        //Arrange
        var admin = new Administrador();

        //Act
        admin.Id = 1;
        admin.Email = "teste@email.com";
        admin.Senha = "123456";
        admin.Perfil = "Admin";

        //Assert
        Assert.AreEqual(1, admin.Id);
        Assert.AreEqual("teste@email.com", admin.Email);
        Assert.AreEqual("123456", admin.Senha);
        Assert.AreEqual("Admin", admin.Perfil);
    }
}