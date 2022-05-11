#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["System/Worker/TaskLibrary.Worker/TaskLibrary.Worker.csproj", "System/Worker/TaskLibrary.Worker/"]
COPY ["Services/SMTPService/TaskLibrary.SMTPService.csproj", "Services/SMTPService/"]
COPY ["Services/TaskLibrary.RabbitMQService/TaskLibrary.RabbitMQService.csproj", "Services/TaskLibrary.RabbitMQService/"]
COPY ["Services/TaskLibrary.Settings/TaskLibrary.Settings.csproj", "Services/TaskLibrary.Settings/"]
COPY ["Data/Context/TaskLibrary.Db.Context/TaskLibrary.Db.Context.csproj", "Data/Context/TaskLibrary.Db.Context/"]
COPY ["Data/Entities/TaskLibrary.Db.Entities/TaskLibrary.Db.Entities.csproj", "Data/Entities/TaskLibrary.Db.Entities/"]
RUN dotnet restore "System/Worker/TaskLibrary.Worker/TaskLibrary.Worker.csproj"
COPY . .
WORKDIR "/src/System/Worker/TaskLibrary.Worker"
RUN dotnet build "TaskLibrary.Worker.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TaskLibrary.Worker.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TaskLibrary.Worker.dll"]