#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Thunders.Test.Task.Manager.Api/Thunders.Test.Task.Manager.Api.csproj", "Thunders.Test.Task.Manager.Api/"]
COPY ["Thunders.Test.Task.Manager.Application/Thunders.Test.Task.Manager.Application.csproj", "Thunders.Test.Task.Manager.Application/"]
COPY ["Thunders.Test.Task.Manager.Domain/Thunders.Test.Task.Manager.Domain.csproj", "Thunders.Test.Task.Manager.Domain/"]
COPY ["Thunders.Test.Task.Manager.Infra.EF/Thunders.Test.Task.Manager.Infra.EF.csproj", "Thunders.Test.Task.Manager.Infra.EF/"]
COPY ["Thunders.Test.TaskManager.Infra.IoC/Thunders.Test.TaskManager.Infra.IoC.csproj", "Thunders.Test.TaskManager.Infra.IoC/"]
RUN dotnet restore "./Thunders.Test.Task.Manager.Api/./Thunders.Test.Task.Manager.Api.csproj"
COPY . .
WORKDIR "/src/Thunders.Test.Task.Manager.Api"
RUN dotnet build "./Thunders.Test.Task.Manager.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Thunders.Test.Task.Manager.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Thunders.Test.Task.Manager.Api.dll"]