#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /SocialNetworkMicroservices
COPY ["Services/User/User.Grpc/User.Grpc.csproj", "Services/User/User.Grpc/"]
RUN dotnet restore "Services/User/User.Grpc/User.Grpc.csproj"
COPY . .
WORKDIR "/SocialNetworkMicroservices/Services/User/User.Grpc"
RUN dotnet build "User.Grpc.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "User.Grpc.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "User.Grpc.dll"]