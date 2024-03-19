# Use the Microsoft .NET SDK image to build the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src
# Copy the CSPROJ file and restore any dependencies (via NUGET)
COPY ["user_service_app.csproj", "./"]
RUN dotnet restore "user_service_app.csproj"

# Copy the rest of the source code
COPY . .

# Build the application
RUN dotnet publish -c Release -o /app/out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/out .

# Expose port 5050 for the application
EXPOSE 5050

# Environment Variables from appsettings and launchSettings for ASPNETCORE
ENV ASPNETCORE_URLS=http://+:5050
ENV ASPNETCORE_ENVIRONMENT=Development

ENTRYPOINT ["dotnet", "user_service_app.dll"]

