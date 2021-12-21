FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim as build
WORKDIR /app
EXPOSE 80
COPY Shared/Shared/*.csproj Shared/Shared/
COPY Frontend/ASP.NET-WEB/*.csproj  Frontend/ASP.NET-WEB/

RUN dotnet restore Frontend/ASP.NET-WEB/*.csproj
COPY . .
RUN dotnet publish Frontend/ASP.NET-WEB/*.csproj -c Release -o out
FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim as runtime
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT ["dotnet","ASP.NET-WEB.dll"]
