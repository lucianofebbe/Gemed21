# Use the .NET SDK 7.0 as the base image
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env

# Define the working directory
WORKDIR /app

# Define o hostname usando a instrução LABEL
LABEL HOSTNAME=gatewayService

# Copy the project files into the container
COPY . .

# Build each project in the respective directories
RUN dotnet build IP.Gateway/Domain/IP.Domain/IP.Domain.csproj -c Release
RUN dotnet build IP.Gateway/Application/IP.Application.Comands.Requests/IP.Application.Comands.Requests.csproj -c Release
RUN dotnet build IP.Gateway/Application/IP.Application.Comands.Responses/IP.Application.Comands.Responses.csproj -c Release
RUN dotnet build IP.Gateway/Infrastructure/IP.DependencyInjection.Infrastructure/IP.DependencyInjection.Infrastructure.csproj -c Release
RUN dotnet build IP.Gateway/Infrastructure/IP.Mapper.Infrastructure/IP.Mapper.Infrastructure.csproj -c Release
RUN dotnet build IP.Gateway/Services/IP.Services/IP.Services.csproj -c Release
RUN dotnet build IP.Gateway/Application/IP.Application.Handlers/IP.Application.Handlers.csproj -c Release
RUN dotnet build IP.Gateway/IP.Gateway/IP.Gateway.csproj -c Release

# Create the publish directory for each project
RUN dotnet publish IP.Gateway/Application/IP.Application.Comands.Requests/IP.Application.Comands.Requests.csproj -c Release -o /app/out/IP.Gateway/Application/IP.Application.Comands.Requests/
RUN dotnet publish IP.Gateway/Application/IP.Application.Comands.Responses/IP.Application.Comands.Responses.csproj -c Release -o /app/out/IP.Gateway/Application/IP.Application.Comands.Responses/
RUN dotnet publish IP.Gateway/Application/IP.Application.Handlers/IP.Application.Handlers.csproj -c Release -o /app/out/IP.Gateway/Application/IP.Application.Handlers/
RUN dotnet publish IP.Gateway/Domain/IP.Domain/IP.Domain.csproj -c Release -o /app/out/IP.Gateway/Domain/IP.Domain/
RUN dotnet publish IP.Gateway/Infrastructure/IP.DependencyInjection.Infrastructure/IP.DependencyInjection.Infrastructure.csproj -c Release -o /app/out/IP.Gateway/Infrastructure/IP.DependencyInjection.Infrastructure/
RUN dotnet publish IP.Gateway/Infrastructure/IP.Mapper.Infrastructure/IP.Mapper.Infrastructure.csproj -c Release -o /app/out/IP.Gateway/Infrastructure/IP.Mapper.Infrastructure/
RUN dotnet publish IP.Gateway/Services/IP.Services/IP.Services.csproj -c Release -o /app/out/IP.Gateway/Services/IP.Services/
RUN dotnet publish IP.Gateway/IP.Gateway/IP.Gateway.csproj -c Release -o /app/out/IP.Gateway/IP.Gateway/

# Build the runtime image
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app

# Copy the published output from each project into the container
COPY --from=build-env /app/out/IP.Gateway/IP.Gateway /app/

# Set the entry point
ENTRYPOINT ["dotnet", "IP.Gateway.dll"]
