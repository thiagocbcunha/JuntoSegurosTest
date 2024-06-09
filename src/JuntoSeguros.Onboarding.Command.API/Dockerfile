#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Docker
WORKDIR /src
COPY ["src/JuntoSeguros.Onboarding.Command.API/JuntoSeguros.Onboarding.Command.Api.csproj", "src/JuntoSeguros.Onboarding.Command.API/"]
RUN dotnet restore "./src/JuntoSeguros.Onboarding.Command.API/JuntoSeguros.Onboarding.Command.Api.csproj"
COPY . .
WORKDIR "/src/src/JuntoSeguros.Onboarding.Command.API"
RUN dotnet build "./JuntoSeguros.Onboarding.Command.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Docker
RUN dotnet publish "./JuntoSeguros.Onboarding.Command.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false


FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "JuntoSeguros.Onboarding.Command.Api.dll"]