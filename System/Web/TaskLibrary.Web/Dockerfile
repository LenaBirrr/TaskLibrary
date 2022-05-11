FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["System/Web/TaskLibrary.Web/TaskLibrary.Web.csproj", "System/Web/TaskLibrary.Web/TaskLibrary.Web.csproj"]
RUN dotnet restore "System/Web/TaskLibrary.Web/TaskLibrary.Web.csproj"
COPY . .
WORKDIR "/src/System/Web/TaskLibrary.Web"
RUN dotnet build "TaskLibrary.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TaskLibrary.Web.csproj" -c Release -o /app/publish

FROM nginx:alpine
WORKDIR /var/www/web

COPY --from=publish /app/publish/wwwroot .
COPY System/Web/TaskLibrary.Web/nginx.conf /etc/nginx/nginx.conf
EXPOSE 80