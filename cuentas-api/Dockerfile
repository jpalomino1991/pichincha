FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 8085

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Cuenta.WEBAPI/Cuenta.WEBAPI.csproj", "Cuenta.WEBAPI/"]
COPY ["Cuenta.Domain/Cuenta.Domain.csproj", "Cuenta.Domain/"]
COPY ["Cuenta.Infraestructure/Cuenta.Infraestructure.csproj", "Cuenta.Infraestructure/"]
COPY ["Cuenta.Application/Cuenta.Application.csproj", "Cuenta.Application/"]

RUN dotnet restore "Cuenta.WEBAPI/Cuenta.WEBAPI.csproj"
COPY . .
WORKDIR "/src/Cuenta.WEBAPI"
RUN dotnet build "Cuenta.WEBAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Cuenta.WEBAPI.csproj" -c Release -o /app/publish

FROM base AS final
RUN addgroup --group tpapgrp --gid 2000 && adduser --uid 1000 --gid 2000 tpapusr --disabled-password
RUN chown -R tpapusr:tpapgrp /app
USER tpapusr:tpapgrp
WORKDIR /app
COPY --chown=tpapusr:tpapgrp --from=publish /app/publish .
ENV ASPNETCORE_URLS="http://*:8085"
ENTRYPOINT ["dotnet", "Cuenta.WEBAPI.dll"]