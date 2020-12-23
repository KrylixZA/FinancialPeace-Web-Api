# Financial Peace Web API [![Build Status](https://dev.azure.com/headleysj/Source%20Code/_apis/build/status/FinancialPeace-Web-Api?branchName=master)](https://dev.azure.com/headleysj/Source%20Code/_build/latest?definitionId=12&branchName=master) [![FinancialPeace.Web.Api package in headleysj feed in Azure Artifacts](https://feeds.dev.azure.com/headleysj/_apis/public/Packaging/Feeds/404449e0-6d24-4a4e-bc3e-4634d3f54a5a/Packages/19bede14-4978-45fb-ab14-e922583f21f4/Badge)](https://dev.azure.com/headleysj/Source%20Code/_packaging?_a=package&feed=404449e0-6d24-4a4e-bc3e-4634d3f54a5a&package=19bede14-4978-45fb-ab14-e922583f21f4&preferRelease=true) [![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=FinancialPeace-Web-Api&metric=alert_status)](https://sonarcloud.io/dashboard?id=FinancialPeace-Web-Api)
The Financial Peace Web API exposes restful functions to serve the functions necessary for managing your way to fincancial peace!

# Build
To build this repository you simply check in code and the automatic [build pipeline in Azure DevOps](https://dev.azure.com/headleysj/Source%20Code/_build?definitionId=12) will validate your code.

# Test
To test this, you can simply launch the project from Visual Studio and run the postman requests from the `test/postman-tests` directory. 

The swagger docs can be found when launching the API and browsing to `http://localhost:<random_port>/swagger/ui`

# Docker
This application is also built into a [Docker image](Dockerfile) and hosted on [Docker Hub](https://hub.docker.com/r/krylixza/financialpeace-web-api).

You can run the application by:
1. `docker pull krylixza/financialpeace-web-api`
2. `docker run -d -p 5000:5000 docker pull krylixza/financialpeace-web-api`
3. Browse to http://localhost:5000

# Contribute
To contribute to this project, please following the [contributing guide](CONTRIBUTING.md)