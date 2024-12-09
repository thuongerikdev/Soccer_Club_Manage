# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["SM.WebAPI/SM.WebAPI.csproj", "SM.WebAPI/"]
COPY ["Services/Auth/SM.Auth.ApplicationService/SM.Auth.ApplicationService.csproj", "Services/Auth/SM.Auth.ApplicationService/"]
COPY ["Services/Shared/SM.Constant/SM.Constant.csproj", "Services/Shared/SM.Constant/"]
COPY ["Services/Shared/SM.Shared.ApplicationService/SM.Shared.ApplicationService.csproj", "Services/Shared/SM.Shared.ApplicationService/"]
COPY ["Services/Auth/SM.Auth.Dtos/SM.Auth.Dtos.csproj", "Services/Auth/SM.Auth.Dtos/"]
COPY ["Services/Auth/SM.Auth.Infrastructure/SM.Auth.Infrastructure.csproj", "Services/Auth/SM.Auth.Infrastructure/"]
COPY ["Services/Auth/SM.Auth.Domain/SM.Auth.Domain.csproj", "Services/Auth/SM.Auth.Domain/"]
COPY ["Services/Tournament/SM.Tournament.ApplicationService/SM.Tournament.ApplicationService.csproj", "Services/Tournament/SM.Tournament.ApplicationService/"]
COPY ["Services/Tournament/SM.Tournament.Domain/SM.Tournament.Domain.csproj", "Services/Tournament/SM.Tournament.Domain/"]
COPY ["Services/Tournament/SM.Tournament.Dtos/SM.Tournament.Dtos.csproj", "Services/Tournament/SM.Tournament.Dtos/"]
COPY ["Services/Tournament/SM.Tournament.Infrastructure/SM.Tournament.Infrastructure.csproj", "Services/Tournament/SM.Tournament.Infrastructure/"]
RUN dotnet restore "./SM.WebAPI/SM.WebAPI.csproj"
COPY . .
WORKDIR "/src/SM.WebAPI"
RUN dotnet build "./SM.WebAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./SM.WebAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SM.WebAPI.dll"]