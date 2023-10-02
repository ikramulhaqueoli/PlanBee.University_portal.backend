FROM mcr.microsoft.com/dotnet/aspnet:6.0-focal AS base
ENV ASPNETCORE_ENVIRONMENT=Staging
WORKDIR /app
EXPOSE 80
FROM mcr.microsoft.com/dotnet/sdk:6.0-focal AS build
WORKDIR /src
COPY ["Start/Start.csproj", "./"]
RUN dotnet restore "Start.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "Start/Start.csproj" -c Release -o /app/build
FROM build AS publish
RUN dotnet publish "Start/Start.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Start.dll"]