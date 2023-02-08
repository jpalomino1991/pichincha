FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 8084

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Cliente.WEBAPI/Cliente.WEBAPI.csproj", "Cliente.WEBAPI/"]
COPY ["Cliente.Domain/Cliente.Domain.csproj", "Cliente.Domain/"]
COPY ["Cliente.Infraestructure/Cliente.Infraestructure.csproj", "Cliente.Infraestructure/"]
COPY ["Cliente.Application/Cliente.Application.csproj", "Cliente.Application/"]

RUN dotnet restore "Cliente.WEBAPI/Cliente.WEBAPI.csproj"
COPY . .
WORKDIR "/src/Cliente.WEBAPI"
RUN dotnet build "Cliente.WEBAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Cliente.WEBAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_URLS="http://*:8084"
ENTRYPOINT ["dotnet", "Cliente.WEBAPI.dll"]