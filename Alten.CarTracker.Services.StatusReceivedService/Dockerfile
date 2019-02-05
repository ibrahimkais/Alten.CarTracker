FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 12001/tcp
ENV ASPNETCORE_URLS http://*:12001
HEALTHCHECK --interval=30s --timeout=10s --retries=1 CMD curl --silent --fail http://localhost:12001/hc || exit 1
ENV WEBSITE_SITE_NAME StatusReceivedService

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY Alten.CarTracker.Services.StatusReceivedService/Alten.CarTracker.Services.StatusReceivedService.csproj Alten.CarTracker.Services.StatusReceivedService/
COPY Alten.CarTracker.Infrastructure.Common/Alten.CarTracker.Infrastructure.Common.csproj Alten.CarTracker.Infrastructure.Common/
COPY Alten.CarTracker.Infrastructure.Messaging/Alten.CarTracker.Infrastructure.Messaging.csproj Alten.CarTracker.Infrastructure.Messaging/
COPY Alten.CarTracker.Infrastructure.ServiceDiscovery/Alten.CarTracker.Infrastructure.ServiceDiscovery.csproj Alten.CarTracker.Infrastructure.ServiceDiscovery/
RUN dotnet restore Alten.CarTracker.Services.StatusReceivedService/Alten.CarTracker.Services.StatusReceivedService.csproj
COPY . .
WORKDIR /src/Alten.CarTracker.Services.StatusReceivedService
RUN dotnet build Alten.CarTracker.Services.StatusReceivedService.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish Alten.CarTracker.Services.StatusReceivedService.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Alten.CarTracker.Services.StatusReceivedService.dll"]
