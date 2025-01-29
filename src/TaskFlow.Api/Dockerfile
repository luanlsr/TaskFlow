# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/TaskFlow.Api/TaskFlow.Api.csproj", "src/TaskFlow.Api/"]
COPY ["src/TaskFlow.Application/TaskFlow.Application.csproj", "src/TaskFlow.Application/"]
COPY ["src/TaskFlow.Domain.Core/TaskFlow.Domain.Core.csproj", "src/TaskFlow.Domain.Core/"]
COPY ["src/TaskFlow.Domain/TaskFlow.Domain.csproj", "src/TaskFlow.Domain/"]
COPY ["src/TaskFlow.Infrastructure.Messaging/TaskFlow.Infrastructure.Messaging.csproj", "src/TaskFlow.Infrastructure.Messaging/"]
COPY ["src/TaskFlow.Infrastructure/TaskFlow.Infrastructure.Data.csproj", "src/TaskFlow.Infrastructure/"]
COPY ["src/TaskFlow.CrossCutting.IoC/TaskFlow.CrossCutting.IoC.csproj", "src/TaskFlow.CrossCutting.IoC/"]
COPY ["src/TaskFlow.CrossCutting.Logging/TaskFlow.CrossCutting.Logging.csproj", "src/TaskFlow.CrossCutting.Logging/"]
COPY ["src/TaskFlow.CrossCutting.Utils/TaskFlow.CrossCutting.Utils.csproj", "src/TaskFlow.CrossCutting.Utils/"]
RUN dotnet restore "./src/TaskFlow.Api/TaskFlow.Api.csproj"
COPY . .
WORKDIR "/src/src/TaskFlow.Api"
RUN dotnet build "./TaskFlow.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./TaskFlow.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TaskFlow.Api.dll"]