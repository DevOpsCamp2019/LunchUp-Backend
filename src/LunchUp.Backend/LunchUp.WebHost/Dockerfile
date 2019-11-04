FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["LunchUp.Backend/LunchUp.Backend.csproj", "LunchUp.Backend/"]
RUN dotnet restore "LunchUp.Backend/LunchUp.Backend.csproj"
COPY . .
WORKDIR "/src/LunchUp.Backend"
RUN dotnet build "LunchUp.Backend.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "LunchUp.Backend.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LunchUp.Backend.dll"]