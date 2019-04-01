FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 7000

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src/web/webapp/
COPY "WebApp.csproj" ./
RUN dotnet restore "WebApp.csproj"

COPY . ./
RUN dotnet build "WebApp.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "WebApp.csproj" -c Release -o /app

FROM base AS final
COPY --from=publish /app .

EXPOSE 7000/tcp
ENV ASPNETCORE_URLS http://*:7000
ENTRYPOINT ["dotnet", "WebApp.dll"]