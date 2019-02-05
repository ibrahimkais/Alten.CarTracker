FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 12002/tcp
ENV ASPNETCORE_URLS http://*:12002
HEALTHCHECK --interval=30s --timeout=10s --retries=1 CMD curl --silent --fail http://localhost:12002/hc || exit 1
ENV WEBSITE_SITE_NAME NotificationService

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY Alten.CarTracker.Services.NotificationService/Alten.CarTracker.Services.NotificationService.csproj Alten.CarTracker.Services.NotificationService/
COPY Alten.CarTracker.Infrastructure.Common/Alten.CarTracker.Infrastructure.Common.csproj Alten.CarTracker.Infrastructure.Common/
COPY Alten.CarTracker.Infrastructure.Messaging/Alten.CarTracker.Infrastructure.Messaging.csproj Alten.CarTracker.Infrastructure.Messaging/
COPY Alten.CarTracker.Infrastructure.ServiceDiscovery/Alten.CarTracker.Infrastructure.ServiceDiscovery.csproj Alten.CarTracker.Infrastructure.ServiceDiscovery/
RUN dotnet restore Alten.CarTracker.Services.NotificationService/Alten.CarTracker.Services.NotificationService.csproj
COPY . .
WORKDIR /src/Alten.CarTracker.Services.NotificationService
RUN dotnet build Alten.CarTracker.Services.NotificationService.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish Alten.CarTracker.Services.NotificationService.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Alten.CarTracker.Services.NotificationService.dll"]
