
# Minimal API

Este projeto é uma implementação de uma Minimal API em .NET, desenvolvido como parte de um bootcamp da Digital Innovation One (DIO). O foco é fornecer um exemplo simples e eficiente para criar APIs rápidas e com baixo overhead.

- Referencia -> [digitalinnovationone/minimal-api](https://github.com/digitalinnovationone/minimal-api)


## 📋 Funcionalidades

- Endpoints de CRUD para operações básicas (criação, leitura, atualização e exclusão).
- Configuração de rotas e middlewares minimalistas.
- Integração com banco de dados para persistência de dados (caso aplicável).
- Uso de boas práticas para APIs RESTful.

## 🚀 Tecnologias Utilizadas

- C#
- .NET 8 (Minimal API)
- Entity Framework Core (caso haja persistência)
- Swagger (para documentação da API)

## ⚙️ Pré-requisitos

Para rodar este projeto, você precisará ter instalado:

- .NET SDK 8
- Banco de Dados SQL Server

## Instalação

1.  Clone o repositório:
```bash
  git clone https://github.com/eukerolyne/minimal-api.git
```

2. Navegue até o diretório do projeto:
```bash
  cd minimal-api
```

3. Restaure as dependências do projeto:
```bash
  dotnet restore
```

4. Atualize o banco de dados (caso esteja configurado com Entity Framework):
```bash
  dotnet ef database update
```

## 🏃‍♀️ Executando o Projeto
Para iniciar a API em modo de desenvolvimento, execute:

```bash
 dotnet run
```

A API estará disponível em http://localhost:5010 ou conforme a configuração do launchSettings.json.

## 📖 Documentação
Acesse o Swagger para visualizar a documentação dos endpoints e realizar testes diretamente pela interface web:
```bash
 http://localhost:5010/swagger
```
## Autores

- [@eukerolyne](https://www.github.com/eukerolyne)

