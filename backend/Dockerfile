FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["Dgii.sln", "./"]
COPY ["Dgii.Api/Dgii.Api.csproj", "Dgii.Api/"]
COPY ["Dgii.Application/Dgii.Application.csproj", "Dgii.Application/"]
COPY ["Dgii.Domain/Dgii.Domain.csproj", "Dgii.Domain/"]
COPY ["Dgii.Infrastructure/Dgii.Infrastructure.csproj", "Dgii.Infrastructure/"]
COPY ["Dgii.Tests/Dgii.Tests.csproj", "Dgii.Tests/"]

RUN dotnet restore "Dgii.sln"

COPY . .
WORKDIR "/src/Dgii.Api"
RUN dotnet build "Dgii.Api.csproj" -c Release -o /app/build
RUN dotnet publish "Dgii.Api.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

# Configure the app to listen on all interfaces
ENV ASPNETCORE_URLS=http://+:8080

COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Dgii.Api.dll"]
