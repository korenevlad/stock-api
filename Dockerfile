FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["src/Edu.StockApi.Web/Edu.StockApi.Web.csproj","src/Edu.StockApi.Web/"]
RUN dotnet restore "src/Edu.StockApi.Web/Edu.StockApi.Web.csproj"
COPY . .
WORKDIR "/src/src/Edu.StockApi.Web"
RUN dotnet build "Edu.StockApi.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Edu.StockApi.Web.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM base AS final
WORKDIR /appы
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Edu.StockApi.Web.dll"]