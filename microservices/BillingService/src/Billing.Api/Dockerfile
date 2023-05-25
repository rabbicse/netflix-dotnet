#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Billing.Api/Billing.Api.csproj", "Billing.Api/"]
RUN dotnet restore "Billing.Api/Billing.Api.csproj"
COPY . .
WORKDIR "/src/Billing.Api"
RUN dotnet build "Billing.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Billing.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Billing.Api.dll"]