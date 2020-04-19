# Introduction 
The Financial Peace Web API exposes restful functions to serve the functions necessary for managing your way to fincancial peace!

# Build
To build this code you can either build the solution through Visual Studio or you could simply run the the `build-bootstrapper.ps1` script passing in the argument `-Actions "build"` as follows:

`.\build-bootstrapper.ps1 -Actions "build"`

# Test
To test this, you can simply launch the project from Visual Studio and run the postman requests from the `tests/postman-tests` directory. 

The swagger docs can be found when launching the API and browsing to `http://localhost:<random_port>/swagger/ui`

# Contribute
To contribute to this project, please following the [contributing guide](CONTRIBUTING.md)