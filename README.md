# [Financial Peace Web API](https://github.com/KrylixZA/FinancialPeace-Web-Api) ![CI](https://github.com/KrylixZA/FinancialPeace-Web-Api/workflows/CI/badge.svg) [![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=FinancialPeace-Web-Api&metric=alert_status)](https://sonarcloud.io/dashboard?id=FinancialPeace-Web-Api)
The Financial Peace Web API exposes restful functions to serve the functions necessary for managing your way to fincancial peace!

# Docker
This application is also built into a [Docker image](Dockerfile) and hosted on [Docker Hub](https://hub.docker.com/r/krylixza/financialpeace-web-api).

## Running the application
In the repository is a [docker-compose.yml](docker-compose.yml) file. Use this file to launch both the Web API and the database. To do this, simply clone the repository, open your CLI and run:

    docker-compose up

## ARM support
If you have a device running on an ARM processor, there is both an ARM Docker image and a `docker-compose.arm.yml` to launch the API in an ARM Docker container. To do this, run the following command:

    docker-compose -f "docker-compose.arm.yml" up

# Contribute
To contribute to this project, please following the [contributing guide](CONTRIBUTING.md).
