FROM mcr.microsoft.com/dotnet/aspnet:5.0
ARG PUBLISH_API_DIR="src/FinancialPeace.Web.Api/bin/Release/net5.0/publish"
ARG TARGET_APPSETTINGS_FILE="appsettings.docker.json"
WORKDIR /app
COPY ${PUBLISH_API_DIR} .
COPY ${PUBLISH_API_DIR}/${TARGET_APPSETTINGS_FILE} ./appsettings.json
EXPOSE 5000
ENV ASPNETCORE_URLS=http://+:5000
CMD ["dotnet", "FinancialPeace.Web.Api.dll"]