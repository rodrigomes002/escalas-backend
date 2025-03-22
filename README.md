# Backend C# - API Restful

Este é um projeto de API Backend em C# para gerenciar a agenda de músicos do ministério de louvor da Igreja Batista em Jardim Sumaré.

## Tecnologias Utilizadas

 - C#
 - ASP.NET Core 8
 - Dapper
 - PostgreSQL
 - JWT Authentication
 - Swagger (para documentação de API)
 - Docker
## Autores

- [@rodrigomes002](https://github.com/rodrigomes002)
- [@CalebeSMartins](https://github.com/CalebeSMartins)

## Build

docker build -t escalasapi . <br />
docker run -d --name escalasapi -p 5001:5001 escalasapi
