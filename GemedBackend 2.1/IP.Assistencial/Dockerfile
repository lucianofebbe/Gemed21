# Use the .NET SDK 7.0 as the base image
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env

# Define the working directory
WORKDIR /app

# Define o hostname usando a instrução LABEL
LABEL HOSTNAME=assistencialService

# Copy the project files into the container
COPY . .

# Build each project in the respective directories
RUN dotnet build IP.Usuario/Services/IP.Services/IP.Services.csproj -c Release

RUN dotnet build IP.Usuario/IP.Usuario/IP.Usuario.csproj -c Release

RUN dotnet build IP.Usuario/Infrastructure/IP.DependencyInjection.Infrastructure/IP.DependencyInjection.Infrastructure.csproj -c Release
RUN dotnet build IP.Usuario/Infrastructure/IP.Mapper.Infrastructure/IP.Mapper.Infrastructure.csproj -c Release
RUN dotnet build IP.Usuario/Infrastructure/IP.Repository.Infrastructure/IP.Repository.Infrastructure.csproj -c Release

RUN dotnet build IP.Usuario/Domain/IP.Domain/IP.Domain.csproj -c Release
RUN dotnet build IP.Usuario/Domain/IP.DomainMongo/IP.DomainMongo.csproj -c Release

RUN dotnet build IP.Usuario/Application/IP.Application.Comands.Requests/IP.Application.Comands.Requests.csproj -c Release
RUN dotnet build IP.Usuario/Application/IP.Application.Comands.Responses/IP.Application.Comands.Responses.csproj -c Release
RUN dotnet build IP.Usuario/Application/IP.Application.Handlers/IP.Application.Handlers.csproj -c Release

# Create the publish directory for each project
RUN dotnet publish IP.Usuario/Services/IP.Services/IP.Services.csproj -c Release -o /app/out/IP.Usuario/Services/IP.Services/IP.Services

RUN dotnet publish IP.Usuario/IP.Usuario/IP.Usuario.csproj -c Release -o /app/out/IP.Usuario/IP.Usuario

RUN dotnet publish IP.Usuario/Infrastructure/IP.DependencyInjection.Infrastructure/IP.DependencyInjection.Infrastructure.csproj -c Release -o /app/out/IP.Usuario/Infrastructure/IP.DependencyInjection.Infrastructure/IP.DependencyInjection.Infrastructure
RUN dotnet publish IP.Usuario/Infrastructure/IP.Mapper.Infrastructure/IP.Mapper.Infrastructure.csproj -c Release -o /app/out/IP.Usuario/Infrastructure/IP.Mapper.Infrastructure/IP.Mapper.Infrastructure
RUN dotnet publish IP.Usuario/Infrastructure/IP.Repository.Infrastructure/IP.Repository.Infrastructure.csproj -c Release -o /app/out/IP.Usuario/Infrastructure/IP.Repository.Infrastructure/IP.Repository.Infrastructure

RUN dotnet publish IP.Usuario/Domain/IP.Domain/IP.Domain.csproj -c Release -o /app/out/IP.Usuario/Domain/IP.Domain/IP.Domain
RUN dotnet publish IP.Usuario/Domain/IP.DomainMongo/IP.DomainMongo.csproj -c Release -o /app/out/IP.Usuario/Domain/IP.DomainMongo/IP.DomainMongo

RUN dotnet publish IP.Usuario/Application/IP.Application.Comands.Requests/IP.Application.Comands.Requests.csproj -c Release -o /app/out/IP.Usuario/Application/IP.Application.Comands.Requests/IP.Application.Comands.Requests
RUN dotnet publish IP.Usuario/Application/IP.Application.Comands.Responses/IP.Application.Comands.Responses.csproj -c Release -o /app/out/IP.Usuario/Application/IP.Application.Comands.Responses/IP.Application.Comands.Responses
RUN dotnet publish IP.Usuario/Application/IP.Application.Handlers/IP.Application.Handlers.csproj -c Release -o /app/out/IP.Usuario/Application/IP.Application.Handlers/IP.Application.Handlers

# Build the runtime image
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app

# Copy the published output from each project into the container
COPY --from=build-env /app/out/IP.Usuario/IP.Usuario /app/

# Set the entry point
ENTRYPOINT ["dotnet", "IP.Usuario.dll"]
