# Etapa de construção
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

RUN mkdir /output

COPY . /app

WORKDIR /app/Escalas.API
RUN dotnet publish --configuration Release --output /output

# Etapa de runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0

# Definir timezone
ENV TZ=America/Sao_Paulo

# Variáveis para rodar a aplicação em HTTPS
ENV ASPNETCORE_URLS=http://*:5001
ENV ASPNETCORE_ENVIRONMENT=Production
ENV PASSWORD_DB="$PASSWORD_DB"

# Copiar o certificado SSL .pfx para o contêiner (ajuste o caminho conforme necessário)
#COPY ./certificate.pfx /https/yourcertificate.pfx

# Definir a senha do certificado
#ENV ASPNETCORE_Kestrel__Certificates__Default__Password="$PASSWORD_CERT"

# Configurar o local do certificado
#ENV ASPNETCORE_Kestrel__Certificates__Default__Path="/https/yourcertificate.pfx"

WORKDIR /app

# Copiar a aplicação publicada da etapa anterior
COPY --from=build-env /output .

# Expor as portas 5000 (HTTPS) e 5001 (HTTP)
EXPOSE 5001

# Comando para iniciar a aplicação
ENTRYPOINT ["dotnet", "Escalas.API.dll"]
