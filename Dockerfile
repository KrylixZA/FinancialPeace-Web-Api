FROM mcr.microsoft.com/dotnet/aspnet:3.1
ARG PUBLISH_API_DIR="src/FinancialPeace.Web.Api/bin/Release/netcoreapp3.1/publish"
WORKDIR /app
COPY ${PUBLISH_API_DIR} .
EXPOSE 5000
ENV ASPNETCORE_URLS=http://+:5000
CMD ["dotnet", "FinancialPeace.Web.Api.dll"]