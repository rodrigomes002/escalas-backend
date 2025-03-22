# escalas-backend

# build

docker build -t escalasapi . <br />
docker run -d --name escalasapi -p 5001:5001 escalasapi


Backend C# - API Restful
Este é um projeto de API Backend em C#. A API oferece endpoints para realizar operações CRUD (Create, Read, Update, Delete) em dados armazenados. O objetivo é fornecer uma estrutura escalável e de fácil manutenção para aplicações modernas.

Índice
Tecnologias Utilizadas

Instalação

Uso

Estrutura do Projeto

Endpoints

Contribuição

Licença

Tecnologias Utilizadas
Este projeto foi desenvolvido utilizando as seguintes tecnologias:

C#

ASP.NET Core 6/7 (ou versão utilizada)

Entity Framework Core

SQL Server / MySQL / PostgreSQL (dependendo da escolha de banco de dados)

JWT Authentication (para autenticação de usuários)

Swagger (para documentação de API)

Instalação
Pré-requisitos
Antes de rodar o projeto, tenha as seguintes ferramentas instaladas:

.NET SDK (versão 6.0 ou superior)

Banco de Dados (SQL Server, MySQL, PostgreSQL ou outro de sua escolha)

Visual Studio ou Visual Studio Code

Passos para Instalação
Clone o repositório

No terminal, execute o seguinte comando:

bash
Copiar código
git clone https://github.com/SEU-USUARIO/NOME-DO-REPOSITORIO.git
Restaure as dependências

No diretório do projeto, execute o seguinte comando para restaurar as dependências:

bash
Copiar código
dotnet restore
Configure o banco de dados

Se você estiver usando o SQL Server, edite o arquivo appsettings.json para configurar a string de conexão com o banco de dados:

json
Copiar código
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=nome_do_banco;User Id=usuario;Password=senha;"
}
Se necessário, crie o banco de dados e as tabelas utilizando migrations do Entity Framework:

bash
Copiar código
dotnet ef migrations add InitialCreate
dotnet ef database update
Executando a API

Para rodar o projeto, use o seguinte comando:

bash
Copiar código
dotnet run
A API estará disponível em http://localhost:5000.

Uso
Swagger UI
Após iniciar o servidor, você pode acessar a documentação interativa da API através do Swagger. Basta abrir o navegador e ir até:

bash
Copiar código
http://localhost:5000/swagger
Aqui você poderá ver todos os endpoints e testá-los diretamente da interface.

Exemplos de requisições
Criar um novo usuário
POST /api/usuarios

Exemplo de corpo da requisição:

json
Copiar código
{
  "nome": "João Silva",
  "email": "joao.silva@example.com",
  "senha": "senha123"
}
Listar todos os usuários
GET /api/usuarios

Estrutura do Projeto
Abaixo está uma visão geral da estrutura de pastas do projeto:

bash
Copiar código
/Backend
├── /Controllers           # Contém os controladores da API
├── /Models                # Contém as classes de modelo de dados
├── /Data                  # Contém o contexto do Entity Framework e conexões ao banco
├── /Services              # Contém a lógica de negócio e serviços auxiliares
├── /Migrations            # Contém os arquivos de migração do banco de dados
├── /DTOs                  # Contém objetos de transferência de dados
├── /wwwroot               # Arquivos estáticos e recursos públicos (se necessário)
└── Program.cs             # Configuração e inicialização do projeto
Endpoints
Usuários
GET /api/usuarios
Retorna todos os usuários cadastrados.

POST /api/usuarios
Cria um novo usuário.

Exemplo de corpo:

json
Copiar código
{
  "nome": "João Silva",
  "email": "joao.silva@example.com",
  "senha": "senha123"
}
GET /api/usuarios/{id}
Retorna um usuário específico pelo ID.

PUT /api/usuarios/{id}
Atualiza as informações de um usuário existente.

DELETE /api/usuarios/{id}
Exclui um usuário pelo ID.

Contribuição
Contribuições são bem-vindas! Para contribuir com este projeto, siga os passos:

Faça um fork deste repositório.

Crie uma branch com suas mudanças: git checkout -b minha-feature.

Faça commit das suas mudanças: git commit -am 'Adicionando nova feature'.

Envie para o seu fork: git push origin minha-feature.

Abra uma pull request no repositório original.

Licença
Este projeto está licenciado sob a MIT License - veja o arquivo LICENSE para mais detalhes.

Esse modelo pode ser ajustado conforme a estrutura e as necessidades do seu projeto. Se precisar de mais informações ou ajustes, só avisar!


Você disse:
crie para esse repositório : https://github.com/rodrigomes002/escalas-backend
O ChatGPT disse:
Aqui está uma sugestão de README para o repositório escalas-backend baseado nas informações disponíveis:

Escalas Backend
Este repositório contém o backend da aplicação Escalas, desenvolvida utilizando C# e .NET Core. A API expõe funcionalidades relacionadas ao gerenciamento de escalas, como criação e consulta de escalas de trabalho.

Índice
Tecnologias Utilizadas

Instalação

Uso

Estrutura do Projeto

Contribuição

Licença

Tecnologias Utilizadas
C#

ASP.NET Core

Entity Framework Core

Docker

JWT Authentication (se aplicável)

Instalação
Pré-requisitos
Antes de rodar o projeto, certifique-se de ter as seguintes ferramentas instaladas:

.NET SDK (versão 6.0 ou superior)

Docker (para ambiente de containerização)

Banco de dados (SQL Server, MySQL ou PostgreSQL, conforme necessário)

Passos para Instalação
Clone o repositório

Execute o comando abaixo para clonar o repositório:

bash
Copiar código
git clone https://github.com/rodrigomes002/escalas-backend.git
Restaure as dependências

Navegue até o diretório do projeto e execute:

bash
Copiar código
dotnet restore
Configuração do Banco de Dados

Edite o arquivo appsettings.json para configurar a string de conexão ao banco de dados. Exemplo:

json
Copiar código
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=escalas_db;User Id=usuario;Password=senha;"
}
Executando o Docker

Se preferir rodar em um container Docker, construa a imagem e execute o container com os seguintes comandos:

bash
Copiar código
docker build -t escalasapi .
docker run -d --name escalasapi -p 5001:5001 escalasapi
Executando a API

Caso não esteja utilizando Docker, você pode executar diretamente com:

bash
Copiar código
dotnet run
A API estará disponível em http://localhost:5001.

Uso
Swagger UI
Após iniciar a API, você pode acessar a documentação interativa da API através do Swagger. Acesse:

bash
Copiar código
http://localhost:5001/swagger
Exemplos de Endpoints
Criar uma nova escala
POST /api/escalas

Exemplo de corpo da requisição:

json
Copiar código
{
  "nome": "Escala 1",
  "dataInicio": "2025-03-21T08:00:00",
  "dataFim": "2025-03-21T16:00:00",
  "usuarioId": 1
}
Consultar escalas
GET /api/escalas

Estrutura do Projeto
O projeto está dividido da seguinte forma:

bash
Copiar código
/Escalas.API            # Controladores da API
/Escalas.Application     # Lógica de aplicação
/Escalas.CrossCutting    # Funcionalidades auxiliares e configuração
/Escalas.Domain          # Modelos de domínio e entidades
/Escalas.Infrastructure   # Implementação de acesso a dados e persistência
/Escalas.Tests           # Testes unitários e integração
Contribuição
Contribuições são bem-vindas! Para contribuir:

Faça um fork deste repositório.

Crie uma branch para suas mudanças: git checkout -b minha-feature.

Faça commit das suas mudanças: git commit -am 'Adicionando nova funcionalidade'.

Envie para o seu fork: git push origin minha-feature.

Abra uma pull request.

Licença
Este projeto está licenciado sob a MIT License - veja o arquivo LICENSE para mais detalhes.
