
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src


COPY ./TriviaBackend.sln ./
COPY ./TriviaAppBL/TriviaAppBL.csproj ./TriviaAppBL/
COPY ./TriviaAppInfrastructure/TriviaAppInfrastructure.csproj ./TriviaAppInfrastructure/
COPY ./TriviaBackend/TriviaBackend.csproj ./TriviaBackend/

RUN dotnet restore ./TriviaBackend.sln

COPY . .

WORKDIR /src/TriviaBackend
RUN dotnet publish -c Release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app .

CMD sh -c "dotnet TriviaBackend.dll --urls http://0.0.0.0:${PORT}"