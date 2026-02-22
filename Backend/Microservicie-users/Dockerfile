# --- Stage 1: Build ---
# Usamos el SDK completo para compilar
FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build
WORKDIR /src

# Copiar archivos de proyecto y restaurar dependencias
# Se hace por separado para aprovechar la caché de capas de Docker
COPY ["Backend.Api/Backend.Api.csproj", "Backend.Api/"]
RUN dotnet restore "Backend.Api/Backend.Api.csproj"

# Copiar el resto del código y compilar
COPY . .
WORKDIR "/src/Backend.Api"
RUN dotnet build "Backend.Api.csproj" -c Release -o /app/build

# --- Stage 2: Publish ---
FROM build AS publish
RUN dotnet publish "Backend.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

# --- Stage 3: Final Runtime ---
FROM mcr.microsoft.com/dotnet/aspnet:8.0-jammy-chiseled AS final
WORKDIR /app

# Copiar los binarios publicados
COPY --from=publish /app/publish .

# Exponer el puerto estándar de .NET 8/9
EXPOSE 8080
EXPOSE 8081

ENTRYPOINT ["dotnet", "Backend.Api.dll"]