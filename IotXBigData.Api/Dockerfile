FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["IotXBigData.Api/IotXBigData.Api.csproj", "IotXBigData.Api/"]
RUN dotnet restore "IotXBigData.Api/IotXBigData.Api.csproj"
COPY . .
WORKDIR "/src/IotXBigData.Api"
RUN dotnet build "./IotXBigData.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./IotXBigData.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "IotXBigData.Api.dll"]
