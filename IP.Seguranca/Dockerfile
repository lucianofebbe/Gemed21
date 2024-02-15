#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["IP.Seguranca/IP.Seguranca/*.*", "./IP.Seguranca/"]
COPY ["IP.Seguranca/IP.Seguranca/Properties/*.*", "./IP.Seguranca/Properties/"]

COPY ["IP.Seguranca/Domain/*.*", "./Domain/"]
COPY ["IP.Seguranca/Domain/IP.Domain/*.*", "./Domain/IP.Domain/"]
COPY ["IP.Seguranca/Infrastructure/IP.Dlls.Infrastructure/*.dll", "./Infrastructure/IP.Dlls.Infrastructure/"]
COPY ["IP.Seguranca/Infrastructure/IP.DependencyInjection.Infrastructure/*.*", "./Infrastructure/IP.DependencyInjection.Infrastructure/"]

# Comands Requests
COPY ["IP.Seguranca/Application/IP.Application.Comands.Requests/*.*", "./Application/IP.Application.Comands.Requests/"]
COPY ["IP.Seguranca/Application/IP.Application.Comands.Requests/Authentication/*.*", "./Application/IP.Application.Comands.Requests/Authentication/"]

# Comands Responses
COPY ["IP.Seguranca/Application/IP.Application.Comands.Responses/*.*", "./Application/IP.Application.Comands.Responses/"]
COPY ["IP.Seguranca/Application/IP.Application.Comands.Responses/Authentication/*.*", "./Application/IP.Application.Comands.Responses/Authentication/"]

# Handlers
COPY ["IP.Seguranca/Application/IP.Application.Handlers/*.*", "./Application/IP.Application.Handlers/"]
COPY ["IP.Seguranca/Application/IP.Application.Handlers/Authentication/*.*", "./Application/IP.Application.Handlers/Authentication/"]

# Mapper
COPY ["IP.Seguranca/Infrastructure/IP.Mapper.Infrastructure/*.*", "./Infrastructure/IP.Mapper.Infrastructure/"]
COPY ["IP.Seguranca/Infrastructure/IP.Mapper.Infrastructure/SegEmpresa/*.*", "./Infrastructure/IP.Mapper.Infrastructure/SegEmpresa/"]
COPY ["IP.Seguranca/Infrastructure/IP.Mapper.Infrastructure/SegUsuario/*.*", "./Infrastructure/IP.Mapper.Infrastructure/SegUsuario/"]

# Repository
COPY ["IP.Seguranca/Infrastructure/IP.Repository.Infrastructure/*.*", "./Infrastructure/IP.Repository.Infrastructure/"]
COPY ["IP.Seguranca/Infrastructure/IP.Repository.Infrastructure/Contexts/*.*", "./Infrastructure/IP.Repository.Infrastructure/Contexts/"]
COPY ["IP.Seguranca/Infrastructure/IP.Repository.Infrastructure/Unit/*.*", "./Infrastructure/IP.Repository.Infrastructure/Unit/"]
COPY ["IP.Seguranca/Infrastructure/IP.Repository.Infrastructure/Repositories/*.*", "./Infrastructure/IP.Repository.Infrastructure/Repositories/"]
COPY ["IP.Seguranca/Infrastructure/IP.Repository.Infrastructure/Repositories/SegEmpresa/*.*", "./Infrastructure/IP.Repository.Infrastructure/Repositories/SegEmpresa/"]
COPY ["IP.Seguranca/Infrastructure/IP.Repository.Infrastructure/Repositories/SegUsuario/*.*", "./Infrastructure/IP.Repository.Infrastructure/Repositories/SegUsuario/"]

# Services
COPY ["IP.Seguranca/Services/IP.Services/*.*", "./Services/IP.Services/"]


RUN dotnet restore "./Application/IP.Application.Comands.Responses/IP.Application.Comands.Responses.csproj"
RUN dotnet restore "./Domain/IP.Domain/IP.Domain.csproj"
RUN dotnet restore "./Application/IP.Application.Comands.Requests/IP.Application.Comands.Requests.csproj"
RUN dotnet restore "./Infrastructure/IP.Repository.Infrastructure/IP.Repository.Infrastructure.csproj"
RUN dotnet restore "./Services/IP.Services/IP.Services.csproj"
RUN dotnet restore "./Infrastructure/IP.Mapper.Infrastructure/IP.Mapper.Infrastructure.csproj"
RUN dotnet restore "./Application/IP.Application.Handlers/IP.Application.Handlers.csproj"
RUN dotnet restore "./Infrastructure/IP.DependencyInjection.Infrastructure/IP.DependencyInjection.Infrastructure.csproj"
RUN dotnet restore "./IP.Seguranca/IP.Seguranca.csproj"
RUN dotnet restore "./IP.Seguranca/IP.Seguranca.sln"

USER root
RUN dotnet build "./Application/IP.Application.Comands.Responses/IP.Application.Comands.Responses.csproj" -c $BUILD_CONFIGURATION -o /src/IP.Seguranca/obj/Release/net7.0/
RUN dotnet build "./Domain/IP.Domain/IP.Domain.csproj" -c $BUILD_CONFIGURATION -o /src/IP.Seguranca/obj/Release/net7.0/
RUN dotnet build "./Application/IP.Application.Comands.Requests/IP.Application.Comands.Requests.csproj" -c $BUILD_CONFIGURATION -o /src/IP.Seguranca/obj/Release/net7.0/
RUN dotnet build "./Infrastructure/IP.Repository.Infrastructure/IP.Repository.Infrastructure.csproj" -c $BUILD_CONFIGURATION -o /src/IP.Seguranca/obj/Release/net7.0/
RUN dotnet build "./Services/IP.Services/IP.Services.csproj" -c $BUILD_CONFIGURATION -o /src/IP.Seguranca/obj/Release/net7.0/
RUN dotnet build "./Infrastructure/IP.Mapper.Infrastructure/IP.Mapper.Infrastructure.csproj" -c $BUILD_CONFIGURATION -o /src/IP.Seguranca/obj/Release/net7.0/
RUN dotnet build "./Application/IP.Application.Handlers/IP.Application.Handlers.csproj" -c $BUILD_CONFIGURATION -o /src/IP.Seguranca/obj/Release/net7.0/
RUN dotnet build "./Infrastructure/IP.DependencyInjection.Infrastructure/IP.DependencyInjection.Infrastructure.csproj" -c $BUILD_CONFIGURATION -o /src/IP.Seguranca/obj/Release/net7.0/
RUN dotnet build "./IP.Seguranca/IP.Seguranca.csproj" -c $BUILD_CONFIGURATION -o /src/IP.Seguranca/obj/Release/net7.0/
RUN dotnet build "./IP.Seguranca/IP.Seguranca.sln" -c $BUILD_CONFIGURATION -o /src/IP.Seguranca/obj/Release/net7.0/
USER nonrootuser

# FROM build AS publish
# ARG BUILD_CONFIGURATION=Release
# RUN dotnet publish "./IP.Seguranca/IP.Seguranca/IP.Seguranca.csproj" -c $BUILD_CONFIGURATION -o /src/publish /p:UseAppHost=false

# FROM base AS final
# WORKDIR /src
# COPY --from=publish /src/publish .
# ENTRYPOINT ["dotnet", "IP.Seguranca.dll"]