FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
ENV COMPlus_EnableDiagnostics=0
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
RUN echo $Database__lunchUp__Password
ENV COMPlus_EnableDiagnostics=0
WORKDIR /src
COPY ["src/LunchUp.Backend/", "LunchUp.Backend/"]
RUN dotnet restore "LunchUp.Backend/LunchUp.Backend.sln"
COPY . .
WORKDIR "/src/LunchUp.Backend"
RUN dotnet build "LunchUp.WebHost/LunchUp.WebHost.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "LunchUp.WebHost/LunchUp.WebHost.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LunchUp.WebHost.dll"]
