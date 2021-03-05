FROM mcr.microsoft.com/dotnet/sdk:3.1 as build
WORKDIR /src
COPY /src .
RUN dotnet publish --configuration Release

FROM mcr.microsoft.com/dotnet/aspnet:3.1 as app
WORKDIR /app
COPY --from=build /src/FinancialPeace.Web.Api/bin/Debug/netcoreapp3.1/publish .
EXPOSE 5000
ENV ASPNETCORE_URLS=http://+:5000
ENTRYPOINT ["dotnet", "FinancialPeace.Web.Api.dll"]