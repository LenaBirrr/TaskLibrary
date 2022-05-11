#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["System/Api/TaskLibrary.Api/TaskLibrary.Api.csproj", "System/Api/TaskLibrary.Api/"]
COPY ["Services/SMTPService/TaskLibrary.SMTPService.csproj", "Services/SMTPService/"]
COPY ["Services/TaskLibrary.RabbitMQService/TaskLibrary.RabbitMQService.csproj", "Services/TaskLibrary.RabbitMQService/"]
COPY ["Services/TaskLibrary.Settings/TaskLibrary.Settings.csproj", "Services/TaskLibrary.Settings/"]
COPY ["Data/Context/TaskLibrary.Db.Context/TaskLibrary.Db.Context.csproj", "Data/Context/TaskLibrary.Db.Context/"]
COPY ["Data/Entities/TaskLibrary.Db.Entities/TaskLibrary.Db.Entities.csproj", "Data/Entities/TaskLibrary.Db.Entities/"]
COPY ["Services/TaskLibrary.CategoryService/TaskLibrary.CategoryService.csproj", "Services/TaskLibrary.CategoryService/"]
COPY ["Shared/TaskLibrary.Common/TaskLibrary.Common.csproj", "Shared/TaskLibrary.Common/"]
COPY ["Services/TaskLibrary.ProgrammingTaskService/TaskLibrary.ProgrammingTaskService.csproj", "Services/TaskLibrary.ProgrammingTaskService/"]
COPY ["Services/TaskLibrary.CommentService/TaskLibrary.CommentService.csproj", "Services/TaskLibrary.CommentService/"]
COPY ["Services/TaskLibrary.NotificationService/TaskLibrary.NotificationService.csproj", "Services/TaskLibrary.NotificationService/"]
COPY ["Services/TaskLibrary.SubscribtionService/TaskLibrary.SubscriptionService.csproj", "Services/TaskLibrary.SubscribtionService/"]
COPY ["Services/TaskLibrary.UserAccount/TaskLibrary.UserAccount.csproj", "Services/TaskLibrary.UserAccount/"]
COPY ["Services/TaskLibrary.SolutionService/TaskLibrary.SolutionService.csproj", "Services/TaskLibrary.SolutionService/"]
COPY ["Services/TaskLibrary.ProgrammingLanguageService/TaskLibrary.ProgrammingLanguageService.csproj", "Services/TaskLibrary.ProgrammingLanguageService/"]
RUN dotnet restore "System/Api/TaskLibrary.Api/TaskLibrary.Api.csproj"
COPY . .
WORKDIR "/src/System/Api/TaskLibrary.Api"
RUN dotnet build "TaskLibrary.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TaskLibrary.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TaskLibrary.Api.dll"]