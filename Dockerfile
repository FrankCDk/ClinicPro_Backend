# Usar una imagen base de .NET SDK
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src

# Copiar los archivos de la solución
COPY ["ClinicPro_Backend.sln", "."]
COPY ["ClinicPro.Presentation/ClinicPro.Presentation.csproj", "ClinicPro.Presentation/"]
COPY ["ClinicPro.Application/ClinicPro.Application.csproj", "ClinicPro.Application/"]
COPY ["ClinicPro.Core/ClinicPro.Core.csproj", "ClinicPro.Core/"]
COPY ["ClinicPro.Infrastructure/ClinicPro.Infrastructure.csproj", "ClinicPro.Infrastructure/"]
COPY ["ClinicPro.Utils/ClinicPro.Utils.csproj", "ClinicPro.Utils/"]

RUN dotnet restore

# Copiar el código fuente y construir el proyecto
COPY . .
WORKDIR "/src/ClinicPro.Presentation"
RUN dotnet build -c Release -o /app/build

# Publicar el proyecto
RUN dotnet publish -c Release -o /app/publish

# Establecer el contenedor base de la aplicación
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

# Copiar los archivos publicados
COPY --from=build /app/publish .

# Comando para ejecutar la API
ENTRYPOINT ["dotnet", "ClinicPro.Presentation.dll"]
