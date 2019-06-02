FROM mcr.microsoft.com/dotnet/core/sdk:2.2-alpine3.9 AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY TechCalendar.Api/*.csproj .
RUN dotnet restore

# Copy everything else and build
COPY TechCalendar.Api/ .
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-alpine3.9
WORKDIR /app
COPY --from=build-env /app/out .

RUN mkdir /Data
VOLUME /Data

ENTRYPOINT ["dotnet", "TechCalendar.Api.dll"]
