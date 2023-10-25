FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["GoogleOIDC/GoogleOIDC.csproj", "GoogleOIDC/"]
RUN dotnet restore "GoogleOIDC/GoogleOIDC.csproj"
COPY . .
WORKDIR "/src/GoogleOIDC"
RUN dotnet build "GoogleOIDC.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GoogleOIDC.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GoogleOIDC.dll"]
