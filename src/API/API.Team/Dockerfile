FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 5100

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src/API/API.Team/
COPY "API.Team.csproj" ./
RUN dotnet restore "API.Team.csproj"

COPY . ./
RUN dotnet build "API.Team.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "API.Team.csproj" -c Release -o /app

FROM base AS final
COPY --from=publish /app .

EXPOSE 5100/tcp
ENV ASPNETCORE_URLS http://*:5100
ENTRYPOINT ["dotnet", "API.Team.dll"]