# Use the official .NET 8 runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

# Use the SDK image for building
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["PortfolioAPI.csproj", "."]
RUN dotnet restore "PortfolioAPI.csproj"
COPY . .
WORKDIR "/src"
RUN dotnet build "PortfolioAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PortfolioAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PortfolioAPI.dll"]
