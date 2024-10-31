FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

RUN mkdir /output

# Copy project and publish

COPY ./Escalas.API /app

WORKDIR /app/Escalas.API
RUN dotnet publish --configuration Release --output /output

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0

# Definir a variável de ambiente TZ
ENV TZ=America/Sao_Paulo

ENV ASPNETCORE_URLS http://*:80

WORKDIR /app

ENV ASPNETCORE_ENVIRONMENT=Development

COPY --from=build-env /output .
EXPOSE 80

ENTRYPOINT ["dotnet", "Escalas.API.dll"]