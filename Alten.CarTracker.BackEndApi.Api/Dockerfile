FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["Alten.CarTracker.BackEndApi.Api/Alten.CarTracker.BackEndApi.Api.csproj", "Alten.CarTracker.BackEndApi.Api/"]
COPY ["Alten.CarTracker.BackEndApi.Application/Alten.CarTracker.BackEndApi.Application.csproj", "Alten.CarTracker.BackEndApi.Application/"]
COPY ["Alten.CarTracker.BackEndApi.DataAccess/Alten.CarTracker.BackEndApi.DataAccess.csproj", "Alten.CarTracker.BackEndApi.DataAccess/"]
COPY ["Alten.CarTracker.BackEndApi.Domain/Alten.CarTracker.BackEndApi.Domain.csproj", "Alten.CarTracker.BackEndApi.Domain/"]
RUN dotnet restore "Alten.CarTracker.BackEndApi.Api/Alten.CarTracker.BackEndApi.Api.csproj"
COPY . .
WORKDIR "/src/Alten.CarTracker.BackEndApi.Api"
RUN dotnet build "Alten.CarTracker.BackEndApi.Api.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Alten.CarTracker.BackEndApi.Api.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Alten.CarTracker.BackEndApi.Api.dll"]