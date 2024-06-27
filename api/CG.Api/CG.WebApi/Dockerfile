#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["CF.WebApi/CF.WebApi.csproj", "CF.WebApi/"]
RUN dotnet restore "CF.WebApi/CF.WebApi.csproj"
COPY . .
WORKDIR "/src/CF.WebApi"
RUN dotnet build "CF.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CF.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
RUN mkdir files
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CF.WebApi.dll"]
