FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 7001

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["TS.WebApi/TS.WebApi.csproj", "TS.WebApi/"]
RUN dotnet restore "TS.WebApi/CG.WebApi.csproj"
COPY . .
WORKDIR "/src/TS.WebApi"
RUN dotnet build "TS.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TS.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
RUN mkdir files
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TS.WebApi.dll"]
