FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base

USER root
RUN apt-get update && apt-get install -y curl

USER $APP_UID
WORKDIR /app
EXPOSE 8080

HEALTHCHECK --interval=30s --timeout=10s --start-period=10s --retries=3 \
    CMD curl --fail http://localhost:8080/health || exit 1

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/FCG.Games.Api/FCG.Games.Api.csproj", "src/FCG.Games.Api/"]
RUN dotnet restore "./src/FCG.Games.Api/FCG.Games.Api.csproj"
COPY . .
WORKDIR "/src/src/FCG.Games.Api"
RUN dotnet build "./FCG.Games.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./FCG.Games.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

HEALTHCHECK --interval=30s --timeout=10s --retries=3 --start-period=20s \
  CMD curl --fail http://localhost:8080/health || exit 1

ENTRYPOINT ["dotnet", "FCG.Games.Api.dll"]