FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

RUN mkdir /output

# Copy project and publish

COPY . /app

WORKDIR /app/Escalas.API
RUN dotnet publish --configuration Release --output /output

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0

# Definir a variável de ambiente TZ
ENV TZ=America/Sao_Paulo
ENV ASPNETCORE_URLS http://*:80 https://*:443

WORKDIR /app

ENV ASPNETCORE_ENVIRONMENT=Development
ENV PASSWORD_DB=$PASSWORD_DB

COPY --from=build-env /output .
EXPOSE 80
EXPOSE 443

ENTRYPOINT ["dotnet", "Escalas.API.dll"]