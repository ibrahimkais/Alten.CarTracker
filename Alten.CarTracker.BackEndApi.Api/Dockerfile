FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 12000/tcp
ENV ASPNETCORE_URLS http://*:12000
HEALTHCHECK --interval=30s --timeout=3s --retries=1 CMD curl --silent --fail http://localhost:12000/hc || exit 1
ENV WEBSITE_SITE_NAME BackEndApi

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY Alten.CarTracker.BackEndApi.Api/Alten.CarTracker.BackEndApi.Api.csproj Alten.CarTracker.BackEndApi.Api/
COPY Alten.CarTracker.BackEndApi.Application/Alten.CarTracker.BackEndApi.Application.csproj Alten.CarTracker.BackEndApi.Application/
COPY Alten.CarTracker.BackEndApi.DataAccess/Alten.CarTracker.BackEndApi.DataAccess.csproj Alten.CarTracker.BackEndApi.DataAccess/
COPY Alten.CarTracker.BackEndApi.Domain/Alten.CarTracker.BackEndApi.Domain.csproj Alten.CarTracker.BackEndApi.Domain/
COPY Alten.CarTracker.Infrastructure.Common/Alten.CarTracker.Infrastructure.Common.csproj Alten.CarTracker.Infrastructure.Common/
RUN dotnet restore Alten.CarTracker.BackEndApi.Api/Alten.CarTracker.BackEndApi.Api.csproj
COPY . .
WORKDIR /src/Alten.CarTracker.BackEndApi.Api
RUN dotnet build Alten.CarTracker.BackEndApi.Api.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish Alten.CarTracker.BackEndApi.Api.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Alten.CarTracker.BackEndApi.Api.dll"]
