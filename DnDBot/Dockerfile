#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.
ARG wd=pwd
FROM mcr.microsoft.com/dotnet/core/runtime:3.1-buster-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build

WORKDIR /src
COPY [wd+"/DnDBot.csproj", "DnDBot/"]
RUN dotnet restore "DnDBot/DnDBot.csproj"
COPY . .
WORKDIR "/src/DnDBot"
RUN dotnet build "DnDBot.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DnDBot.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DnDBot.dll"]