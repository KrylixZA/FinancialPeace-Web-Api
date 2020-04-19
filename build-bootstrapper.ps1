param (
    $BuildId = "1",
    $BranchName = "master",
    $BuildConfiguration = "Release",
    $Actions = @("build", "unit-test"),
    $NugetAccessToken
)

# Create an alias for nuget.exe
$workingDir = $PSScriptRoot;
$toolsDir = Join-Path $workingDir "tools";
$nugetPath = Join-Path $toolsDir 'nuget.exe';
Set-Alias -Name "nuget" -Value $nugetPath -Scope Script;

# Define build versioning
. .\build\versioning.ps1;
$buildVersion = "$Major.$Minor.$Patch.$BuildId";
$nugetPkgVersion = $buildVersion;
If (-not([string]::IsNullOrWhiteSpace($BranchName)) -and ($BranchName -ne "master")) {
    $nugetPkgVersion = "$nugetPkgVersion-$BranchName";
}
Write-Host "Package version: $nugetPkgVersion";

# Authenticate with organization nuget feed
if (-not([string]::IsNullOrWhiteSpace($NugetAccessToken))) {
    if (-not $(Get-PackageSource -Name 'headleysj' -ProviderName NuGet -ErrorAction Ignore))
    {
        $orgNugetSource = "https://pkgs.dev.azure.com/headleysj/_packaging/headleysj/nuget/v3/index.json";
        $addOrgNugetCmd = "nuget sources add -Name headleysj -Source $orgNugetSource -Username AzureNugetUser -Password $NugetAccessToken";
        Write-Host "Registering with $orgNugetSource";
        Invoke-Expression -Command $addOrgNugetCmd;
    }
}

# Run the build step if it exists
if ($Actions -contains "build") {
    .\build-solution.ps1 -BuildVersion $nugetPkgVersion -BuildConfiguration $BuildConfiguration;
}

# Tun the unit test step if it exists
if ($Actions -contains "unit-test") {
    .\build-unit-test.ps1 -BuildVersion $nugetPkgVersion -BuildConfiguration $BuildConfiguration;
}