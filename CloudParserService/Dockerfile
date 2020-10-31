#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/runtime:3.1-buster-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["CloudParserService/CloudParserService.csproj", "CloudParserService/"]
RUN dotnet restore "CloudParserService/CloudParserService.csproj"
COPY . .
WORKDIR "/src/CloudParserService"
RUN dotnet build "CloudParserService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CloudParserService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CloudParserService.dll"]