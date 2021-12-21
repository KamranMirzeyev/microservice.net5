FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim as build
WORKDIR /app
EXPOSE 80
COPY Shared/Shared/*.csproj Shared/Shared/
COPY Services/Order/Service.Order.API/*.csproj Services/Order/Service.Order.API/
COPY Service/Order/Service.Order.Application/*.csproj Service/Order/Service.Order.Application/
COPY Service/Order/Service.Order.Core/*.csproj Service/Order/Service.Order.Core/
COPY Service/Order/Service.Order.Domain/*.csproj Service/Order/Service.Order.Domain/
COPY Service/Order/Service.Order.Infrastructure/*.csproj Service/Order/Service.Order.Infrastructure/

RUN dotnet restore Services/Order/Service.Order.API/*.csproj
COPY . .
RUN dotnet publish Services/Order/Service.Order.API/*.csproj -c Release -o out
FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim as runtime
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT ["dotnet","Service.Order.API.dll"]
