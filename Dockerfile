FROM mcr.microsoft.com/dotnet/aspnet:3.1
COPY src/FinancialPeace.Web.Api/bin/Release/netcoreapp3.1/ App/
WORKDIR /App
ENV ASPNETCORE_URLS=http://+:5000
ENTRYPOINT ["dotnet", "FinancialPeace.Web.Api.dll"]