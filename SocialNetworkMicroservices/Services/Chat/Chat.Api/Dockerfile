#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /SocialNetworkMicroservices
COPY ["Services/Chat/Chat.Api/Chat.Api.csproj", "Services/Chat/Chat.Api/"]
COPY ["BuildingBlocks/EventBus.Messages/EventBus.Messages.csproj", "BuildingBlocks/EventBus.Messages/"]
RUN dotnet restore "Services/Chat/Chat.Api/Chat.Api.csproj"
COPY . .
WORKDIR "/SocialNetworkMicroservices/Services/Chat/Chat.Api"
RUN dotnet build "Chat.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Chat.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Chat.Api.dll"]