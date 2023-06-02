#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["CQRSMediatrDDD.Job/CQRSMediatrDDD.Job.csproj", "CQRSMediatrDDD.Job/"]
COPY ["CQRSMediatrDDD.Domain/CQRSMediatrDDD.Domain.csproj", "CQRSMediatrDDD.Domain/"]
COPY ["CQRSMediatrDDD.Infra.Cache/CQRSMediatrDDD.Infra.Cache.csproj", "CQRSMediatrDDD.Infra.Cache/"]
COPY ["CQRSMediatrDDD.Infra.Repository/CQRSMediatrDDD.Infra.Repository.csproj", "CQRSMediatrDDD.Infra.Repository/"]
RUN dotnet restore "CQRSMediatrDDD.Job/CQRSMediatrDDD.Job.csproj"
COPY . .
WORKDIR "/src/CQRSMediatrDDD.Job"
RUN dotnet build "CQRSMediatrDDD.Job.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CQRSMediatrDDD.Job.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CQRSMediatrDDD.Job.dll"]