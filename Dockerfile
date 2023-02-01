FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["open-ai-example.csproj", "."]
RUN dotnet restore "./open-ai-example.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "open-ai-example.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "open-ai-example.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "open-ai-example.dll"]