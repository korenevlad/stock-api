FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["src/Edu.StockApiWeb/Edu.StockApiWeb.csproj","src/Edu.StockApiWeb/"]
RUN dotnet restore "src/Edu.StockApiWeb/Edu.StockApiWeb.csproj"
COPY . .
WORKDIR "/src/src/Edu.StockApiWeb"
RUN dotnet build "Edu.StockApiWeb.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Edu.StockApiWeb.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM base AS final
WORKDIR /appы
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Edu.StockApiWeb.dll"]