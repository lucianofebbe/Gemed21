#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Define o hostname usando a instrução LABEL
LABEL HOSTNAME=faturamentoService

COPY ["IP.Usuario/IP.Usuario/*.*", "./IP.Usuario/"]
COPY ["IP.Usuario/IP.Usuario/Properties/*.*", "./IP.Usuario/Properties/"]

COPY ["IP.Usuario/Domain/*.*", "./Domain/"]
COPY ["IP.Usuario/Domain/IP.Domain/*.*", "./Domain/IP.Domain/"]
COPY ["IP.Usuario/Infrastructure/IP.Dlls.Infrastructure/*.dll", "./Infrastructure/IP.Dlls.Infrastructure/"]
COPY ["IP.Usuario/Infrastructure/IP.DependencyInjection.Infrastructure/*.*", "./Infrastructure/IP.DependencyInjection.Infrastructure/"]

# Comands Requests
COPY ["IP.Usuario/Application/IP.Application.Comands.Requests/*.*", "./Application/IP.Application.Comands.Requests/"]
COPY ["IP.Usuario/Application/IP.Application.Comands.Requests/Authentication/*.*", "./Application/IP.Application.Comands.Requests/Authentication/"]
COPY ["IP.Usuario/Application/IP.Application.Comands.Requests/Cliente/*.*", "./Application/IP.Application.Comands.Requests/Cliente/"]
COPY ["IP.Usuario/Application/IP.Application.Comands.Requests/Services/*.*", "./Application/IP.Application.Comands.Requests/Services/"]
COPY ["IP.Usuario/Application/IP.Application.Comands.Requests/Usuario/*.*", "./Application/IP.Application.Comands.Requests/Usuario/"]
# Comands Responses
COPY ["IP.Usuario/Application/IP.Application.Comands.Responses/*.*", "./Application/IP.Application.Comands.Responses/"]
COPY ["IP.Usuario/Application/IP.Application.Comands.Responses/Authentication/*.*", "./Application/IP.Application.Comands.Responses/Authentication/"]
COPY ["IP.Usuario/Application/IP.Application.Comands.Responses/Cliente/*.*", "./Application/IP.Application.Comands.Responses/Cliente/"]
COPY ["IP.Usuario/Application/IP.Application.Comands.Responses/Services/*.*", "./Application/IP.Application.Comands.Responses/Services/"]
COPY ["IP.Usuario/Application/IP.Application.Comands.Responses/Usuario/*.*", "./Application/IP.Application.Comands.Responses/Usuario/"]

# Handlers
COPY ["IP.Usuario/Application/IP.Application.Handlers/*.*", "./Application/IP.Application.Handlers/"]
COPY ["IP.Usuario/Application/IP.Application.Handlers/Authentication/*.*", "./Application/IP.Application.Handlers/Authentication/"]
COPY ["IP.Usuario/Application/IP.Application.Handlers/Authorization/*.*", "./Application/IP.Application.Handlers/Authorization/"]
COPY ["IP.Usuario/Application/IP.Application.Handlers/Cliente/*.*", "./Application/IP.Application.Handlers/Cliente/"]
COPY ["IP.Usuario/Application/IP.Application.Handlers/Usuario/*.*", "./Application/IP.Application.Handlers/Usuario/"]

# Mapper
COPY ["IP.Usuario/Infrastructure/IP.Mapper.Infrastructure/*.*", "./Infrastructure/IP.Mapper.Infrastructure/"]
COPY ["IP.Usuario/Infrastructure/IP.Mapper.Infrastructure/Usuario/*.*", "./Infrastructure/IP.Mapper.Infrastructure/Usuario/"]
COPY ["IP.Usuario/Infrastructure/IP.Mapper.Infrastructure/UsuarioAcesso/*.*", "./Infrastructure/IP.Mapper.Infrastructure/UsuarioAcesso/"]

# Repository
COPY ["IP.Usuario/Infrastructure/IP.Repository.Infrastructure/*.*", "./Infrastructure/IP.Repository.Infrastructure/"]
COPY ["IP.Usuario/Infrastructure/IP.Repository.Infrastructure/Contexts/*.*", "./Infrastructure/IP.Repository.Infrastructure/Contexts/"]
COPY ["IP.Usuario/Infrastructure/IP.Repository.Infrastructure/Unit/*.*", "./Infrastructure/IP.Repository.Infrastructure/Unit/"]
COPY ["IP.Usuario/Infrastructure/IP.Repository.Infrastructure/Repositories/*.*", "./Infrastructure/IP.Repository.Infrastructure/Repositories/"]
COPY ["IP.Usuario/Infrastructure/IP.Repository.Infrastructure/Repositories/UsuarioAcessos/*.*", "./Infrastructure/IP.Repository.Infrastructure/Repositories/UsuarioAcessos/"]
COPY ["IP.Usuario/Infrastructure/IP.Repository.Infrastructure/Repositories/Usuarios/*.*", "./Infrastructure/IP.Repository.Infrastructure/Repositories/Usuarios/"]

# Services
COPY ["IP.Usuario/Services/IP.Services/*.*", "./Services/IP.Services/"]
COPY ["IP.Usuario/Services/IP.Services/VincularCliente/*.*", "./Services/IP.Services/VincularCliente/"]


RUN dotnet restore "./Application/IP.Application.Comands.Responses/IP.Application.Comands.Responses.csproj"
RUN dotnet restore "./Domain/IP.Domain/IP.Domain.csproj"
RUN dotnet restore "./Application/IP.Application.Comands.Requests/IP.Application.Comands.Requests.csproj"
RUN dotnet restore "./Infrastructure/IP.Repository.Infrastructure/IP.Repository.Infrastructure.csproj"
RUN dotnet restore "./Services/IP.Services/IP.Services.csproj"
RUN dotnet restore "./Infrastructure/IP.Mapper.Infrastructure/IP.Mapper.Infrastructure.csproj"
RUN dotnet restore "./Application/IP.Application.Handlers/IP.Application.Handlers.csproj"
RUN dotnet restore "./Infrastructure/IP.DependencyInjection.Infrastructure/IP.DependencyInjection.Infrastructure.csproj"
RUN dotnet restore "./IP.Usuario/IP.Usuario.csproj"
RUN dotnet restore "./IP.Usuario/IP.Usuario.sln"

USER root
RUN dotnet build "./Application/IP.Application.Comands.Responses/IP.Application.Comands.Responses.csproj" -c $BUILD_CONFIGURATION -o /src/IP.Usuario/obj/Release/net7.0/
RUN dotnet build "./Domain/IP.Domain/IP.Domain.csproj" -c $BUILD_CONFIGURATION -o /src/IP.Usuario/obj/Release/net7.0/
RUN dotnet build "./Application/IP.Application.Comands.Requests/IP.Application.Comands.Requests.csproj" -c $BUILD_CONFIGURATION -o /src/IP.Usuario/obj/Release/net7.0/
RUN dotnet build "./Infrastructure/IP.Repository.Infrastructure/IP.Repository.Infrastructure.csproj" -c $BUILD_CONFIGURATION -o /src/IP.Usuario/obj/Release/net7.0/
RUN dotnet build "./Services/IP.Services/IP.Services.csproj" -c $BUILD_CONFIGURATION -o /src/IP.Usuario/obj/Release/net7.0/
RUN dotnet build "./Infrastructure/IP.Mapper.Infrastructure/IP.Mapper.Infrastructure.csproj" -c $BUILD_CONFIGURATION -o /src/IP.Usuario/obj/Release/net7.0/
RUN dotnet build "./Application/IP.Application.Handlers/IP.Application.Handlers.csproj" -c $BUILD_CONFIGURATION -o /src/IP.Usuario/obj/Release/net7.0/
RUN dotnet build "./Infrastructure/IP.DependencyInjection.Infrastructure/IP.DependencyInjection.Infrastructure.csproj" -c $BUILD_CONFIGURATION -o /src/IP.Usuario/obj/Release/net7.0/
RUN dotnet build "./IP.Usuario/IP.Usuario.csproj" -c $BUILD_CONFIGURATION -o /src/IP.Usuario/obj/Release/net7.0/
RUN dotnet build "./IP.Usuario/IP.Usuario.sln" -c $BUILD_CONFIGURATION -o /src/IP.Usuario/obj/Release/net7.0/
USER nonrootuser

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./IP.Usuario/IP.Usuario/IP.Usuario.csproj" -c $BUILD_CONFIGURATION -o /src/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /src
COPY --from=publish /src/publish .
ENTRYPOINT ["dotnet", "IP.Usuario.dll"]