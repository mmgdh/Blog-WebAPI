#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["BlogInfoService.WebAPI/BlogInfoService.WebAPI.csproj", "BlogInfoService.WebAPI/"]
COPY ["BlogInfoService.Domain/BlogInfoService.Domain.csproj", "BlogInfoService.Domain/"]
COPY ["CommomInterface/CommomInterface.csproj", "CommomInterface/"]
COPY ["CommonDomain/DomainCommon.csproj", "CommonDomain/"]
COPY ["Commons/CommonHelpers.csproj", "Commons/"]
COPY ["BlogInfoService.Infrastructure/BlogInfoService.Infrastructure.csproj", "BlogInfoService.Infrastructure/"]
COPY ["CommonInitializer/CommonInitializer.csproj", "CommonInitializer/"]
COPY ["CommomFilters/CommomFilters.csproj", "CommomFilters/"]
COPY ["CommomJWT/CommomJWT.csproj", "CommomJWT/"]
COPY ["CommonInfrastructure/CommonInfrastructure.csproj", "CommonInfrastructure/"]
COPY ["CommonMiddleWare/CommonMiddleWare.csproj", "CommonMiddleWare/"]
COPY ["EventBus/EventBus.csproj", "EventBus/"]
COPY ["CommomConst/CommomConst.csproj", "CommomConst/"]
RUN dotnet restore "BlogInfoService.WebAPI/BlogInfoService.WebAPI.csproj"
COPY . .
WORKDIR "/src/BlogInfoService.WebAPI"
RUN dotnet build "BlogInfoService.WebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BlogInfoService.WebAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BlogInfoService.WebAPI.dll"]