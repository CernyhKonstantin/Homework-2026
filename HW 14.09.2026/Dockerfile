FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["HW 24.08.2026.csproj", "./"]
RUN dotnet restore "HW 24.08.2026.csproj"

COPY . .
RUN dotnet publish "HW 24.08.2026.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

EXPOSE 8080

COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "HW 24.08.2026.dll"]
