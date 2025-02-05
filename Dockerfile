# Use the official .NET 8 SDK image as the build environment
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

# Copy the project files and restore dependencies
COPY *.sln ./
COPY ["ClinicPro.Presentation/ClinicPro.Presentation.csproj", "ClinicPro.Presentation/"]
COPY ["ClinicPro.Application/ClinicPro.Application.csproj", "ClinicPro.Application/"]
COPY ["ClinicPro.Core/ClinicPro.Core.csproj", "ClinicPro.Core/"]
COPY ["ClinicPro.Infrastructure/ClinicPro.Infrastructure.csproj", "ClinicPro.Infrastructure/"]
COPY ["ClinicPro.Utils/ClinicPro.Utils.csproj", "ClinicPro.Utils/"]
RUN dotnet restore

# Copy the remaining files and build the project
COPY . ./
RUN dotnet publish -c Release -o out

# Use the official .NET 8 runtime image as the runtime environment
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build-env /app/out .

# Expose the port the app runs on
EXPOSE 80

# Set the entry point for the container
ENTRYPOINT ["dotnet", "ClinicPro.Presentation.dll"]