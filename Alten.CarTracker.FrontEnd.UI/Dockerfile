FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["Alten.CarTracker.FrontEnd.UI/Alten.CarTracker.FrontEnd.UI.csproj", "Alten.CarTracker.FrontEnd.UI/"]
RUN dotnet restore "Alten.CarTracker.FrontEnd.UI/Alten.CarTracker.FrontEnd.UI.csproj"
COPY . .
WORKDIR "/src/Alten.CarTracker.FrontEnd.UI"
RUN dotnet build "Alten.CarTracker.FrontEnd.UI.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Alten.CarTracker.FrontEnd.UI.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Alten.CarTracker.FrontEnd.UI.dll"]