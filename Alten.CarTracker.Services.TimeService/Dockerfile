FROM microsoft/dotnet:2.2-runtime AS base
WORKDIR /app

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY Alten.CarTracker.Services.TimeService/Alten.CarTracker.Services.TimeService.csproj Alten.CarTracker.Services.TimeService/
COPY Alten.CarTracker.Infrastructure.Common/Alten.CarTracker.Infrastructure.Common.csproj Alten.CarTracker.Infrastructure.Common/
COPY Alten.CarTracker.Infrastructure.Messaging/Alten.CarTracker.Infrastructure.Messaging.csproj Alten.CarTracker.Infrastructure.Messaging/
COPY Alten.CarTracker.Infrastructure.ServiceDiscovery/Alten.CarTracker.Infrastructure.ServiceDiscovery.csproj Alten.CarTracker.Infrastructure.ServiceDiscovery/
RUN dotnet restore Alten.CarTracker.Services.TimeService/Alten.CarTracker.Services.TimeService.csproj
COPY . .
WORKDIR /src/Alten.CarTracker.Services.TimeService
RUN dotnet build Alten.CarTracker.Services.TimeService.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish Alten.CarTracker.Services.TimeService.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Alten.CarTracker.Services.TimeService.dll"]
