FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim as build
WORKDIR /app
EXPOSE 80
COPY Shared/Shared/*.csproj Shared/Shared/
COPY Services/Catalog/Service.Catalog/*.csproj Services/Catalog/Service.Catalog/

RUN dotnet restore Services/Catalog/Service.Catalog/*.csproj
COPY . .
RUN dotnet publish Services/Catalog/Service.Catalog/*.csproj -c Release -o out
FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim as runtime
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT ["dotnet","Service.Catalog.dll"]
