FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /app

RUN mkdir /output

# Copy project and publish

COPY . /app

WORKDIR /app/Escalas.API
RUN dotnet publish --configuration Debug --output /output

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0

ENV ASPNETCORE_URLS http://*:5001

WORKDIR /app

ENV ASPNETCORE_ENVIRONMENT=Development

COPY --from=build-env /output .
EXPOSE 5001

ENTRYPOINT ["dotnet", "Escalas.API.dll"]