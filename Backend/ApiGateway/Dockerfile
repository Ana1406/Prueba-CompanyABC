# -------- Build Stage --------
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar solución y restaurar dependencias
COPY *.sln .
COPY Backend.Api/*.csproj Backend.Api/
RUN dotnet restore

# Copiar todo el código
COPY . .
WORKDIR /src/Backend.Api
RUN dotnet publish -c Release -o /app/publish

# -------- Runtime Stage --------
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

COPY --from=build /app/publish .

EXPOSE 8080

ENTRYPOINT ["dotnet", "Backend.Api.dll"]