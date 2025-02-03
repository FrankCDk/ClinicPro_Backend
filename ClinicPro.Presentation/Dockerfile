# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Set environment variables for database connection
ENV ConnectionStrings__DefaultConnection="Server=localhost;Database=ClinicProDB;User Id=root;Password=Dike9930;"

# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["ClinicPro.Presentation/ClinicPro.Presentation.csproj", "ClinicPro.Presentation/"]
COPY ["ClinicPro.Application/ClinicPro.Application.csproj", "ClinicPro.Application/"]
COPY ["ClinicPro.Core/ClinicPro.Core.csproj", "ClinicPro.Core/"]
COPY ["ClinicPro.Utils/ClinicPro.Utils.csproj", "ClinicPro.Utils/"]
COPY ["ClinicPro.Infrastructure/ClinicPro.Infrastructure.csproj", "ClinicPro.Infrastructure/"]
RUN dotnet restore "./ClinicPro.Presentation/ClinicPro.Presentation.csproj"
COPY . .
WORKDIR "/src/ClinicPro.Presentation"
RUN dotnet build "./ClinicPro.Presentation.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./ClinicPro.Presentation.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ClinicPro.Presentation.dll"]