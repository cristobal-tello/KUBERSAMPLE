FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 5200

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src/API/API.Member/
COPY "API.Member.csproj" ./
RUN dotnet restore "API.Member.csproj"

COPY . ./
RUN dotnet build "API.Member.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "API.Member.csproj" -c Release -o /app

FROM base AS final
COPY --from=publish /app .

EXPOSE 5200/tcp
ENV ASPNETCORE_URLS http://*:5200
ENTRYPOINT ["dotnet", "API.Member.dll"]