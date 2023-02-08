FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 8086

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Movimiento.WEBAPI/Movimiento.WEBAPI.csproj", "Movimiento.WEBAPI/"]
COPY ["Movimiento.Domain/Movimiento.Domain.csproj", "Movimiento.Domain/"]
COPY ["Movimiento.Infraestructure/Movimiento.Infraestructure.csproj", "Movimiento.Infraestructure/"]
COPY ["Movimiento.Application/Movimiento.Application.csproj", "Movimiento.Application/"]

RUN dotnet restore "Movimiento.WEBAPI/Movimiento.WEBAPI.csproj"
COPY . .
WORKDIR "/src/Movimiento.WEBAPI"
RUN dotnet build "Movimiento.WEBAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Movimiento.WEBAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_URLS="http://*:8086"
ENTRYPOINT ["dotnet", "Movimiento.WEBAPI.dll"]