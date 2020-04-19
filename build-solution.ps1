param (
    $BuildVersion = "1.0.0.0",
    $BuildConfiguration = "Release"
)

# Declare directories
$workingDir = $PSScriptRoot;
$buildDir = Join-Path $workingDir "build";
$srcDir = Join-Path $workingDir "src";
$outputDir = Join-Path $workingDir "artifacts";
$apiOutputDir = Join-Path $outputDir "api";

# Cleanup build directories
$cleanupDirectories = @();
$cleanupDirectories += $outputDir;
$cleanupDirectories += (Get-ChildItem -Path $srcDir -Include ("bin", "obj") -Recurse).FullName;
foreach ($cleanupDir in $cleanupDirectories) {
    if (-not([string]::IsNullOrWhiteSpace($cleanupDirectories) -or [string]::IsNullOrEmpty($cleanupDir))) {
        if (Test-Path -Path $cleanupDir) {
            Remove-Item -Path $cleanupDir -Force -Recurse | Out-Null;
        }
    }
}

# Invoke build command
$buildCmd = "dotnet build `"$srcDir`" --configuration $BuildConfiguration -p:Version=$BuildVersion";
Invoke-Expression -Command $buildCmd;

$nuspecFile = Join-Path $buildDir "FinancialPeace.Web.Api.nuspec";
$packProperties = "/p:NuspecProperties=`"version=$BuildVersion`"";
$packCmd = "dotnet pack `"$srcDir`" -p:NuspecFile=`"$nuspecFile`" --output `"$apiOutputDir`" --configuration $BuildConfiguration /p:PackageVersion=$BuildVersion $packProperties --no-build";
Write-Host $packCmd;
Invoke-Expression -Command $packCmd;