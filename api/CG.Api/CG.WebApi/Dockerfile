FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 7001

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["CG.WebApi/CG.WebApi.csproj", "CG.WebApi/"]
RUN dotnet restore "CG.WebApi/CG.WebApi.csproj"
COPY . .
WORKDIR "/src/CG.WebApi"
RUN dotnet build "CG.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CG.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
RUN mkdir files
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CG.WebApi.dll"]
