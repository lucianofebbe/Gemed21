# Use the .NET SDK 7.0 as the base image
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env

# Define the working directory
WORKDIR /app

# Define o hostname usando a instrução LABEL
LABEL HOSTNAME=segurancaService

# Copy the project files into the container
COPY . .

# Build each project in the respective directories
RUN dotnet build IP.Seguranca/Domain/IP.Domains/IP.Domains.csproj -c Release
RUN dotnet build IP.Seguranca/Application/IP.Application.Comands.Requests/IP.Application.Comands.Requests.csproj -c Release
RUN dotnet build IP.Seguranca/Application/IP.Application.Comands.Responses/IP.Application.Comands.Responses.csproj -c Release
RUN dotnet build IP.Seguranca/Infrastructure/IP.DependencyInjection.Infrastructure/IP.DependencyInjection.Infrastructure.csproj -c Release
RUN dotnet build IP.Seguranca/Infrastructure/IP.Repository.Infrastructure/IP.Repository.Infrastructure.csproj -c Release
RUN dotnet build IP.Seguranca/Services/IP.Services/IP.Services.csproj -c Release
RUN dotnet build IP.Seguranca/Application/IP.Application.Handlers/IP.Application.Handlers.csproj -c Release
RUN dotnet build IP.Seguranca/IP.Seguranca/IP.Seguranca.csproj -c Release

# Create the publish directory for each project
RUN dotnet publish IP.Seguranca/Application/IP.Application.Comands.Requests/IP.Application.Comands.Requests.csproj -c Release -o /app/out/IP.Seguranca/Application/IP.Application.Comands.Requests/
RUN dotnet publish IP.Seguranca/Application/IP.Application.Comands.Responses/IP.Application.Comands.Responses.csproj -c Release -o /app/out/IP.Seguranca/Application/IP.Application.Comands.Responses/
RUN dotnet publish IP.Seguranca/Application/IP.Application.Handlers/IP.Application.Handlers.csproj -c Release -o /app/out/IP.Seguranca/Application/IP.Application.Handlers/
RUN dotnet publish IP.Seguranca/Domain/IP.Domains/IP.Domains.csproj -c Release -o /app/out/IP.Seguranca/Domains/IP.Domains/
RUN dotnet publish IP.Seguranca/Infrastructure/IP.DependencyInjection.Infrastructure/IP.DependencyInjection.Infrastructure.csproj -c Release -o /app/out/IP.Seguranca/Infrastructure/IP.DependencyInjection.Infrastructure/
RUN dotnet publish IP.Seguranca/Infrastructure/IP.Repository.Infrastructure/IP.Repository.Infrastructure.csproj -c Release -o /app/out/IP.Seguranca/Infrastructure/IP.Repository.Infrastructure/
RUN dotnet publish IP.Seguranca/IP.Seguranca/IP.Seguranca.csproj -c Release -o /app/out/IP.Seguranca/IP.Seguranca/
RUN dotnet publish IP.Seguranca/Services/IP.Services/IP.Services.csproj -c Release -o /app/out/IP.Seguranca/Services/IP.Services/

# Build the runtime image
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app

# Copy the published output from each project into the container
COPY --from=build-env /app/out/IP.Seguranca/IP.Seguranca /app/

# Set the entry point
ENTRYPOINT ["dotnet", "IP.Seguranca.dll"]
