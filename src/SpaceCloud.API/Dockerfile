FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
RUN apt-get update -qq && apt-get -y install libgdiplus libc6-dev
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Coworking.View/Coworking.Api.csproj", "Coworking.View/"]
COPY ["Coworking.Infrastructure/Coworking.Infrastructure.csproj", "Coworking.Infrastructure/"]
COPY ["Coworking.Application/Coworking.Application.csproj", "Coworking.Application/"]
COPY ["Coworking.Domain/Coworking.Domain.csproj", "Coworking.Domain/"]
RUN dotnet restore "Coworking.View/Coworking.Api.csproj"
COPY . .
WORKDIR "/src/Coworking.View"
RUN dotnet build "Coworking.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Coworking.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
RUN chmod 755 ./Rotativa/Linux/wkhtmltopdf
ENTRYPOINT ["dotnet", "Coworking.Api.dll"]