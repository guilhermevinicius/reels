FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /
COPY ["../src/Reels.Backoffice.CrossCutting/Reels.Backoffice.CrossCutting.csproj", "src/Reels.Backoffice.CrossCutting/"]
COPY ["../src/Reels.Backoffice.Domain/Reels.Backoffice.Domain.csproj", "src/Reels.Backoffice.Domain/"]
COPY ["../src/Reels.Backoffice.Application/Reels.Backoffice.Application.csproj", "src/Reels.Backoffice.Application/"]
COPY ["../src/Reels.Backoffice.Persistence/Reels.Backoffice.Persistence.csproj", "src/Reels.Backoffice.Persistence/"]
COPY ["../src/Reels.Backoffice.Infrastructure/Reels.Backoffice.Infrastructure.csproj", "src/Reels.Backoffice.Infrastructure/"]
COPY ["../src/Reels.Backoffice.Api/Reels.Backoffice.Api.csproj", "src/Reels.Backoffice.Api/"]
COPY ["../src/Reels.Backoffice.UnitTests/Reels.Backoffice.UnitTests.csproj", "src/Reels.Backoffice.UnitTests/"]
COPY ["../src/Reels.Backoffice.IntegrationTests/Reels.Backoffice.IntegrationTests.csproj", "src/Reels.Backoffice.IntegrationTests/"]
RUN dotnet restore src/Reels.Backoffice.Api/Reels.Backoffice.Api.csproj

COPY . .
WORKDIR "src/Reels.Backoffice.Api"
RUN dotnet build "Reels.Backoffice.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Reels.Backoffice.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_HTTP_PORTS=80
ENTRYPOINT ["dotnet", "Reels.Backoffice.Api.dll"]