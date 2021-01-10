# [Financial Peace Web API](https://github.com/KrylixZA/FinancialPeace-Web-Api) ![CI](https://github.com/KrylixZA/FinancialPeace-Web-Api/workflows/CI/badge.svg) [![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=FinancialPeace-Web-Api&metric=alert_status)](https://sonarcloud.io/dashboard?id=FinancialPeace-Web-Api)
The Financial Peace Web API exposes restful functions to serve the functions necessary for managing your way to fincancial peace!

# Build
There are many ways to build this repository. To build this application you will need to authenticate with GitHub Packages as the [Shared.WebApi.Core](https://github.com/KrylixZA/Shared-WebApi-Core/packages/554859) package is used. To do this, simply run the following command: `dotnet nuget add source https://nuget.pkg.github.com/krylixza/index.json -n github_krylixza -u <your_github_username> -p <your_github_personal_access_token> --store-password-in-clear-text`

## dotnet cli
Run the following command: `dotnet build ./src`

## GitHub Actions
Push code to GitHub and the [CI pipeline](https://github.com/KrylixZA/FinancialPeace-Web-Api/actions) will automatically build your branch and push an image to [Docker Hub](https://hub.docker.com/repository/docker/krylixza/financialpeace-web-api/general)

## Visual Studio Code
There is already a `tasks.json` file provided which will automatically build the project when you attempt to run.

# Running the application
You can either run this application through the dotnet runtime, IIS or through Docker.

## dotnet runtime
Ensure your project is built, then simply: `dotnet ./src/FinancialPeace.Web.Api/bin/Debug/netcoreapp3.1/FinancialPeace.Web.Api.dll`

## IIS
This can be configured through Visual Studio which will abstract the complexity away for you.

## Docker
You can pull and run the Docker image by running the following command: `docker run -d -p 5000:5000 krylixza/financialpeace-web-api:latest`

# Test
To test this, you can simply launch the project and run the Postman requests from the `test/postman-tests` directory. 

The swagger docs can be found when launching the API and browsing to `http://localhost:5000`

# Docker
This application is also built into a [Docker image](Dockerfile) and hosted on [Docker Hub](https://hub.docker.com/r/krylixza/financialpeace-web-api).

# Contribute
To contribute to this project, please following the [contributing guide](CONTRIBUTING.md).
