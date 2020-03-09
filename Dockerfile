# NuGet restore
FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY *.sln .
COPY WebApp.Api/*.csproj WebApp.Api/
COPY WebApp.Core/*.csproj WebApp.Core/
COPY WebApp.Data/*.csproj WebApp.Data/
RUN dotnet restore
COPY . .

# publish
FROM build AS publish
WORKDIR /src/WebApp.Api
RUN dotnet publish -c Release -o /src/publish

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS runtime
WORKDIR /app
COPY --from=publish /src/publish .
ENTRYPOINT ["dotnet", "WebApp.Api.dll"]
# heroku uses the following
CMD ASPNETCORE_URLS=http://*:$PORT dotnet WebApp.Api.dll
