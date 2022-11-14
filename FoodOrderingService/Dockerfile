FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["FoodOrderingService/FoodOrderingService.csproj", "FoodOrderingService/"]
RUN dotnet restore "FoodOrderingService/FoodOrderingService.csproj"
COPY . .
WORKDIR "/src/FoodOrderingService"
RUN dotnet build "FoodOrderingService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FoodOrderingService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FoodOrderingService.dll"]
