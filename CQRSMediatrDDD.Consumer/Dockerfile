#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["CQRSMediatrDDD.Consumer/CQRSMediatrDDD.Consumer.csproj", "CQRSMediatrDDD.Consumer/"]
COPY ["CQRSMediatrDDD.Domain/CQRSMediatrDDD.Domain.csproj", "CQRSMediatrDDD.Domain/"]
COPY ["CQRSMediatrDDD.Infra.Repository/CQRSMediatrDDD.Infra.Repository.csproj", "CQRSMediatrDDD.Infra.Repository/"]
RUN dotnet restore "CQRSMediatrDDD.Consumer/CQRSMediatrDDD.Consumer.csproj"
COPY . .
WORKDIR "/src/CQRSMediatrDDD.Consumer"
RUN dotnet build "CQRSMediatrDDD.Consumer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CQRSMediatrDDD.Consumer.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CQRSMediatrDDD.Consumer.dll"]