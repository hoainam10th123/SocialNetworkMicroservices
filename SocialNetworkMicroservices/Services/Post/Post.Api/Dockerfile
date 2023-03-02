#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /SocialNetworkMicroservices
COPY ["Services/Post/Post.Api/Post.Api.csproj", "Services/Post/Post.Api/"]
COPY ["Services/Post/Post.Infrastructure/Post.Infrastructure.csproj", "Services/Post/Post.Infrastructure/"]
COPY ["Services/Post/Post.Core/Post.Core.csproj", "Services/Post/Post.Core/"]
COPY ["BuildingBlocks/EventBus.Messages/EventBus.Messages.csproj", "BuildingBlocks/EventBus.Messages/"]
RUN dotnet restore "Services/Post/Post.Api/Post.Api.csproj"
COPY . .
WORKDIR "/SocialNetworkMicroservices/Services/Post/Post.Api"
RUN dotnet build "Post.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Post.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Post.Api.dll"]