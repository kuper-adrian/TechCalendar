FROM mcr.microsoft.com/dotnet/core/sdk:2.2-alpine3.9 AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY TechCalendar.Client/*.csproj ./TechCalendar.Client/
COPY TechCalendar.Web/*.csproj ./TechCalendar.Web/

RUN dotnet restore ./TechCalendar.Web/TechCalendar.Web.csproj

# Copy everything else and build
COPY TechCalendar.Client/ ./TechCalendar.Client/
COPY TechCalendar.Web/ ./TechCalendar.Web/
RUN dotnet publish -c Release -o out ./TechCalendar.Web/TechCalendar.Web.csproj
RUN ls

# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-alpine3.9
WORKDIR /app
COPY --from=build-env /app/TechCalendar.Web/out .
ENTRYPOINT ["dotnet", "TechCalendar.Web.dll"]
