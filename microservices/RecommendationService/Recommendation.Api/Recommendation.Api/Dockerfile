#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Recommendation.Api/Recommendation.Api.csproj", "Recommendation.Api/"]
RUN dotnet restore "Recommendation.Api/Recommendation.Api.csproj"
COPY . .
WORKDIR "/src/Recommendation.Api"
RUN dotnet build "Recommendation.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Recommendation.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Recommendation.Api.dll"]