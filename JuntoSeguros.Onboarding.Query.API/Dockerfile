#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["JuntoSeguros.Onboarding.Query.Api/JuntoSeguros.Onboarding.Query.Api.csproj", "JuntoSeguros.Onboarding.Query.Api/"]
RUN dotnet restore "./JuntoSeguros.Onboarding.Query.Api/./JuntoSeguros.Onboarding.Query.Api.csproj"
COPY . .
WORKDIR "/src/JuntoSeguros.Onboarding.Query.Api"
RUN dotnet build "./JuntoSeguros.Onboarding.Query.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./JuntoSeguros.Onboarding.Query.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false


FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "JuntoSeguros.Onboarding.Query.Api.dll"]