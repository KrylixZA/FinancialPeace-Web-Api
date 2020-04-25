param (
    $NugetFeedName,
    $NugetFeedSource,
    $NugetFeedUser,
    $NugetAccessToken
)

# Declare directories
$workingDir = $PSScriptRoot;
$outputDir = Join-Path $workingDir "artifacts";

# Create an alias for nuget.exe
$workingDir = $PSScriptRoot;
$toolsDir = Join-Path $workingDir "tools";
$nugetPath = Join-Path $toolsDir 'nuget.exe';
Set-Alias -Name "nuget" -Value $nugetPath -Scope Script;

# Authenticate with organization nuget feed
if (-not([string]::IsNullOrWhiteSpace($NugetAccessToken))) {
    if (-not $(Get-PackageSource -Name $NugetFeedName -ProviderName NuGet -ErrorAction Ignore))
    {
        $addOrgNugetCmd = "nuget sources add -Name $NugetFeedName -Source $NugetFeedSource -Username $NugetFeedUser -Password $NugetAccessToken";
        Write-Host "Registering with $NugetFeedSource";
        Invoke-Expression -Command $addOrgNugetCmd;
    }
}

$publishCmd = "nuget push -Source $NugetFeedSource -ApiKey AzureArtifacts $outputDir\**\*.nupkg";
Write-Host $publishCmd;
Invoke-Expression -Command $publishCmd;