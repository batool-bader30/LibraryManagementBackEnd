FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["LibraryManagement.csproj", "./"]
RUN dotnet restore "./LibraryManagement.csproj"
COPY . .
RUN dotnet publish "LibraryManagement.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "LibraryManagement.dll"]