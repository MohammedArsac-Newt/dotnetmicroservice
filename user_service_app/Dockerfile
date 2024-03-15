# Use Ubuntu 22.04 as the base image
FROM ubuntu:22.04

# Set the working directory in the Docker image
WORKDIR /app

# Install necessary packages for the .NET SDK, PostgreSQL client, and other dependencies
RUN apt-get update \
    && apt-get install -y --no-install-recommends \
       ca-certificates \
       wget \
       apt-transport-https \
       software-properties-common \
    && wget -q https://packages.microsoft.com/config/ubuntu/22.04/packages-microsoft-prod.deb -O packages-microsoft-prod.deb \
    && dpkg -i packages-microsoft-prod.deb \
    && apt-get update \
    && apt-get install -y --no-install-recommends \
       dotnet-sdk-8.0 \
       postgresql-client \
    && apt-get clean \
    && rm -rf /var/lib/apt/lists/* /tmp/* /var/tmp/* packages-microsoft-prod.deb

# Copy the project files into the Docker image
COPY . .

# Restore the dependencies and tools for the project
RUN dotnet restore

# Publish the application to the 'out' folder
RUN dotnet publish -c Release -o out

# Expose the port the application runs on
EXPOSE 5050

# Set environment variables for ASP.NET Core
ENV ASPNETCORE_URLS=http://+:5050
ENV ASPNETCORE_ENVIRONMENT=Development

# Define the entry point for the application
ENTRYPOINT ["dotnet", "out/user_service_app.dll"]
