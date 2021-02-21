FROM mcr.microsoft.com/dotnet/sdk:3.1 as build
ARG BUILD_NUMBER
ARG FEED_USER
ARG FEED_ACCESSTOKEN

WORKDIR /src
COPY /src .
COPY nuget.config .
RUN dotnet restore
RUN dotnet publish --no-restore --configuration Release /p:Version=${BUILD_NUMBER} --output /app

FROM mcr.microsoft.com/dotnet/aspnet:3.1 as app
EXPOSE 5000
ENV ASPNETCORE_URLS=http://+:5000
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "FinancialPeace.Web.Api.dll"]