using Microsoft.EntityFrameworkCore;
using MinimalApi.Dominio.Entidades;
using MinimalApi.Dominio.Servicos;
using MinimalApi.DTOs;
using MinimalApi.Infraestrutura.DB;

namespace Test.Domain.Entidades;

[TestClass]
public class AdministradorServicosTeste
{
    private DbContexto CriarContextoTeste()
    {
        var options = new DbContextOptionsBuilder<DbContexto>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        return new DbContexto(options);
    }

    [TestMethod]
    public void TestarLogin()
    {
        //Arrange
        var context = CriarContextoTeste();

        var admin = new Administrador();
        admin.Email = "teste@email.com";
        admin.Senha = "123456";
        admin.Perfil = "Admin";

        var login = new LoginDTO
        {
            Email = admin.Email,
            Password = admin.Senha
        };

        var servicos = new AdministradorServico(context);

        //Act
        servicos.Adicionar(admin);
        var adm = servicos.Login(login);

        //Assert
        Assert.AreEqual(admin, adm);
    }

    [TestMethod]
    public void TestarAdicionar()
    {
        //Arrange
        var context = CriarContextoTeste();

        var admin = new Administrador();
        admin.Email = "teste@email.com";
        admin.Senha = "123456";
        admin.Perfil = "Admin";

        var servicos = new AdministradorServico(context);

        //Act
        servicos.Adicionar(admin);

        //Assert
        Assert.AreEqual(1, servicos.Listar(1).Count());
    }

    [TestMethod]
    public void TestarBuscaPorId()
    {
        //Arrange
        var context = CriarContextoTeste();

        var admin = new Administrador();
        admin.Email = "teste@email.com";
        admin.Senha = "123456";
        admin.Perfil = "Admin";

        var servicos = new AdministradorServico(context);
        servicos.Adicionar(admin);

        //Act
        var adm = servicos.BuscaPorId(admin.Id);

        //Assert
        Assert.IsNotNull(adm);
        Assert.AreEqual(admin.Id, adm.Id);
    }

    [TestMethod]
    public void TestarApagar()
    {
        //Arrange
        var context = CriarContextoTeste();

        var admin = new Administrador();
        admin.Email = "teste@email.com";
        admin.Senha = "123456";
        admin.Perfil = "Admin";

        var servicos = new AdministradorServico(context);
        servicos.Adicionar(admin);

        //Act
        servicos.Apagar(admin);

        //Assert
        var adm = servicos.BuscaPorId(admin.Id);
        Assert.IsNull(adm);
    }
}