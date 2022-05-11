#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["System/Identity/TaskLibrary.Identity/TaskLibrary.Identity.csproj", "System/Identity/TaskLibrary.Identity/"]
COPY ["Data/Context/TaskLibrary.Db.Context/TaskLibrary.Db.Context.csproj", "Data/Context/TaskLibrary.Db.Context/"]
COPY ["Data/Entities/TaskLibrary.Db.Entities/TaskLibrary.Db.Entities.csproj", "Data/Entities/TaskLibrary.Db.Entities/"]
COPY ["Services/TaskLibrary.Settings/TaskLibrary.Settings.csproj", "Services/TaskLibrary.Settings/"]
COPY ["Shared/TaskLibrary.Common/TaskLibrary.Common.csproj", "Shared/TaskLibrary.Common/"]
RUN dotnet restore "System/Identity/TaskLibrary.Identity/TaskLibrary.Identity.csproj"
COPY . .
WORKDIR "/src/System/Identity/TaskLibrary.Identity"
RUN dotnet build "TaskLibrary.Identity.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TaskLibrary.Identity.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TaskLibrary.Identity.dll"]