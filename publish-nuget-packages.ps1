param (
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
$orgNugetSource = "https://pkgs.dev.azure.com/headleysj/_packaging/headleysj/nuget/v3/index.json";
if (-not([string]::IsNullOrWhiteSpace($NugetAccessToken))) {
    if (-not $(Get-PackageSource -Name 'headleysj' -ProviderName NuGet -ErrorAction Ignore))
    {
        $addOrgNugetCmd = "nuget sources add -Name headleysj -Source $orgNugetSource -Username AzureNugetUser -Password $NugetAccessToken";
        Write-Host "Registering with $orgNugetSource";
        Invoke-Expression -Command $addOrgNugetCmd;
    }
}

$publishCmd = "nuget push -Source $orgNugetSource -ApiKey AzureArtifacts $outputDir\**\*.nupkg";
Write-Host $publishCmd;
Invoke-Expression -Command $publishCmd;