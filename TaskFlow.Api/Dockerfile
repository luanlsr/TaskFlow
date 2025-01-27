# .NET Backend
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["TaskFlow/Apresentation/TaskFlow.Api/TaskFlow.Api.csproj", "TaskFlow/"]
RUN dotnet restore "TaskFlow/TaskFlow.Api/TaskFlow.Api.csproj"
COPY . .
WORKDIR "/src/TaskFlow/1 - Apresentation/TaskFlow.Api"
RUN dotnet build -c Release -o /app/build

FROM build AS publish
RUN dotnet publish -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TaskFlow.Api.dll"]
