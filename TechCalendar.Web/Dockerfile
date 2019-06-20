FROM mcr.microsoft.com/dotnet/core/sdk:2.2-alpine3.9 AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY TechCalendar.Web/*.csproj ./TechCalendar.Web/
RUN dotnet restore ./TechCalendar.Web/TechCalendar.Web.csproj

RUN apk add nodejs
RUN apk add npm

COPY TechCalendar.Web/package*.json ./TechCalendar.Web/
RUN cd ./TechCalendar.Web/ && npm install

# Copy everything else and build
COPY TechCalendar.Web/ ./TechCalendar.Web/
RUN cd ./TechCalendar.Web/ && npm run prod
RUN dotnet publish -c Release -o out ./TechCalendar.Web/TechCalendar.Web.csproj


# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-alpine3.9
WORKDIR /app
COPY --from=build-env /app/TechCalendar.Web/out .
ENTRYPOINT ["dotnet", "TechCalendar.Web.dll"]
