param (
    $BuildNumber = "1.0.0.0",
    $BuildConfiguration = "Release"
)

# Declare directories
$workingDir = $PSScriptRoot;
$srcDir = Join-Path $workingDir "src";
$outputDir = Join-Path $workingDir "artifacts";
$testsOutputDir = Join-Path $outputDir "unit-tests/";

# To understand more about Coverlet, read here: https://github.com/tonerdo/coverlet/blob/master/Documentation/MSBuildIntegration.md

# Run unit tests
$coverletExclusion = "";
$testCmd = "dotnet test `"$srcDir`" --configuration $BuildConfiguration -p:Version=$BuildNumber /p:CollectCoverage=true /p:CoverletOutputFormat=cobertura /p:CoverletOutput=`"$testsOutputDir`" /p:Exclude=`"$coverletExclusion`"";
Invoke-Expression -Command $testCmd;