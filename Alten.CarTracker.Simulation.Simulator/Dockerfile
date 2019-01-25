FROM microsoft/dotnet:2.2-runtime AS base
WORKDIR /app

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY Alten.CarTracker.Simulation.Simulator/Alten.CarTracker.Simulation.Simulator.csproj Alten.CarTracker.Simulation.Simulator/
COPY Alten.CarTracker.Infrastructure.Common/Alten.CarTracker.Infrastructure.Common.csproj Alten.CarTracker.Infrastructure.Common/
COPY Alten.CarTracker.Infrastructure.Messaging/Alten.CarTracker.Infrastructure.Messaging.csproj Alten.CarTracker.Infrastructure.Messaging/
RUN dotnet restore Alten.CarTracker.Simulation.Simulator/Alten.CarTracker.Simulation.Simulator.csproj
COPY . .
WORKDIR /src/Alten.CarTracker.Simulation.Simulator
RUN dotnet build Alten.CarTracker.Simulation.Simulator.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish Alten.CarTracker.Simulation.Simulator.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Alten.CarTracker.Simulation.Simulator.dll"]
