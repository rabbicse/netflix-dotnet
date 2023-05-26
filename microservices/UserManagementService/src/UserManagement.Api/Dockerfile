#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["UserManagementService/src/UserManagement.Api/UserManagement.Api.csproj", "UserManagementService/src/UserManagement.Api/"]
COPY ["Shared/Work.Rabbi.Common.Api/Work.Rabbi.Common.Api.csproj", "Shared/Work.Rabbi.Common.Api/"]
COPY ["Shared/Work.Rabbi.Common/Work.Rabbi.Common.csproj", "Shared/Work.Rabbi.Common/"]
COPY ["UserManagementService/src/UserManagement.Infrastructure/UserManagement.Infrastructure.csproj", "UserManagementService/src/UserManagement.Infrastructure/"]
COPY ["UserManagementService/src/UserManagement.Domain/UserManagement.Domain.csproj", "UserManagementService/src/UserManagement.Domain/"]
COPY ["UserManagementService/src/UserManagement.Application/UserManagement.Application.csproj", "UserManagementService/src/UserManagement.Application/"]
COPY ["Shared/Work.Rabbi.Common.Infrastructure/Work.Rabbi.Common.Infrastructure.csproj", "Shared/Work.Rabbi.Common.Infrastructure/"]
COPY ["Shared/Work.Rabbi.Common.Domain/Work.Rabbi.Common.Domain.csproj", "Shared/Work.Rabbi.Common.Domain/"]
RUN dotnet restore "UserManagementService/src/UserManagement.Api/UserManagement.Api.csproj"
COPY . .
WORKDIR "/src/UserManagementService/src/UserManagement.Api"
RUN dotnet build "UserManagement.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "UserManagement.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UserManagement.Api.dll"]