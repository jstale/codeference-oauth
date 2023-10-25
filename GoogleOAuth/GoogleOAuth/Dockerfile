FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["GoogleOAuth/GoogleOAuth.csproj", "GoogleOAuth/"]
RUN dotnet restore "GoogleOAuth/GoogleOAuth.csproj"
COPY . .
WORKDIR "/src/GoogleOAuth"
RUN dotnet build "GoogleOAuth.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GoogleOAuth.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GoogleOAuth.dll"]
