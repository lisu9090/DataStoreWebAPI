FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["Microservices/Shop/Shop.API/Shop.API.csproj", "Microservices/Shop/Shop.API/"]
RUN dotnet restore "Microservices/Shop/Shop.API/Shop.API.csproj"
COPY . .
WORKDIR "/src/Microservices/Shop/Shop.API"
RUN dotnet build "Shop.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Shop.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Shop.API.dll"]