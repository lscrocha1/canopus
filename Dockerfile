FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Canopus.API/Canopus.API.csproj", "Canopus.API/"]
RUN dotnet restore "Canopus.API/Canopus.API.csproj"
COPY . .
WORKDIR "/src/Canopus.API"
RUN dotnet build "Canopus.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Canopus.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Canopus.API.dll"]
