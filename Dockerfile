FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

RUN mkdir /output

# Copy project and publish

COPY . /app

WORKDIR /app/Escalas.API
RUN dotnet publish --configuration Release --output /output

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0

# Configurar o fuso horário
RUN apt-get update && \
    apt-get install -y tzdata && \
    ln -fs /usr/share/zoneinfo/America/Sao_Paulo /etc/localtime && \
    dpkg-reconfigure -f noninteractive tzdata

# Definir a variável de ambiente TZ
ENV TZ=America/Sao_Paulo

ENV ASPNETCORE_URLS http://*:5001

WORKDIR /app

ENV ASPNETCORE_ENVIRONMENT=Development

COPY --from=build-env /output .
EXPOSE 5001

ENTRYPOINT ["dotnet", "Escalas.API.dll"]