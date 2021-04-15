FROM mcr.microsoft.com/dotnet/core/sdk:3.1-alpine as builder

WORKDIR /src

COPY ["src/Marketplace.Core/Marketplace.Core.csproj", "src/Marketplace.Core/"]
COPY ["src/Marketplace.Domain.Shared/Marketplace.Domain.Shared.csproj", "src/Marketplace.Domain.Shared/"]
COPY ["src/Marketplace.Domain/Marketplace.Domain.csproj", "src/Marketplace.Domain/"]
COPY ["src/Marketplace.Data/Marketplace.Data.csproj", "src/Marketplace.Data/"]
COPY ["src/Marketplace.Application.BasketServices/Marketplace.Application.BasketServices.csproj", "src/Marketplace.Application.BasketServices/"]
COPY ["src/Marketplace.Api.Core/Marketplace.Api.Core.csproj", "src/Marketplace.Api.Core/"]
COPY ["src/Marketplace.Api/Marketplace.Api.csproj", "src/Marketplace.Api/"]


RUN dotnet restore "src/Marketplace.Api/Marketplace.Api.csproj"
COPY . .
WORKDIR "/src/src/Marketplace.Api"
RUN dotnet build "Marketplace.Api.csproj" -c Release -o /app/build

RUN dotnet publish "Marketplace.Api.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-alpine as baseimage
WORKDIR /app
COPY --from=builder /app/publish .
ENV ASPNETCORE_ENVIRONMENT=Production
ENV ASPNETCORE_URLS=http://*:5000

CMD [ "dotnet", "Marketplace.Api.dll" ]