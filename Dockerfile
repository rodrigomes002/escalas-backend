FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

RUN mkdir /output

COPY . /app

WORKDIR /app/Escalas.API
RUN dotnet publish --configuration Release --output /output

FROM mcr.microsoft.com/dotnet/aspnet:8.0

ENV TZ=America/Sao_Paulo
ENV ASPNETCORE_URLS http://*:5001

WORKDIR /app

ENV ASPNETCORE_ENVIRONMENT=Production

COPY --from=build-env /output .
EXPOSE 5001

ENTRYPOINT ["dotnet", "Escalas.API.dll"]