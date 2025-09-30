
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY TriviaAppWeb/TriviaBackend.csproj ./TriviaAppWeb/
COPY TriviaBackend.sln .

COPY TriviaAppBL/ ./TriviaAppBL/
COPY TriviaAppInfrastructure/ ./TriviaAppInfrastructure/

RUN dotnet restore

COPY TriviaAppWeb/ ./TriviaAppWeb/

RUN dotnet publish TriviaAppWeb/TriviaBackend.csproj -c Release -o /app --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

COPY --from=build /app .

ENV ASPNETCORE_URLS=http://0.0.0.0:${PORT}

ENTRYPOINT ["dotnet", "TriviaBackend.dll"]