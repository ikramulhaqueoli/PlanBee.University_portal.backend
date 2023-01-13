FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["PlanBee.University-portal/PlanBee.University-portal.csproj", "PlanBee.University-portal/"]
RUN dotnet restore "PlanBee.University-portal/PlanBee.University-portal.csproj"
COPY . .
WORKDIR "/src/PlanBee.University-portal"
RUN dotnet build "PlanBee.University-portal.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PlanBee.University-portal.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PlanBee.University-portal.dll"]
