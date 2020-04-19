# Contributing Guide
Detailed below are a set of rules for contributing to this repository.

1. Clone the repository at https://headleysj@dev.azure.com/headleysj/FinancialPeace/_git/FinancialPeace-Web-Api
2. All work must be done on a branch as the master branch is protected from commits.
3. Branches should be created under the following style guides:
    * `task/[A-Za-z]+[0-9]*-<semantic branch name>`; OR
    * `feature/[A-Za-z]+[0-9]*-<semantic branch name>`; OR
    * `bug/A-Za-z]+[0-9]*-<semantic branch name>`
4. When adding a new controller action to the API, ensure that your pull request includes updates to the postman collection.
5. When adding a new controller action to the API, ensure that there are XML comments documenting the changes as these are used in the swagger docs.
6. All new C# code should be covered by unit tests.
7. Branches shall be merged into master through a pull request.