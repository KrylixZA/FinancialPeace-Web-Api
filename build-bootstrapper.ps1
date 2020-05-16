param (
    $BuildId = "1",
    $BranchName = "master",
    $BuildConfiguration = "Release",
    $Actions = @("build", "unit-test"),
    $NugetFeedName,
    $NugetFeedSource,
    $NugetFeedUser,
    $NugetAccessToken
)

# Create an alias for nuget.exe
$workingDir = $PSScriptRoot;
$toolsDir = Join-Path $workingDir "tools";
$nugetPath = Join-Path $toolsDir 'nuget.exe';
Set-Alias -Name "nuget" -Value $nugetPath -Scope Script;

# Define build versioning
. .\version.ps1;
$buildVersion = "$Major.$Minor.$Patch.$BuildId";
$nugetPkgVersion = $buildVersion;
If (-not([string]::IsNullOrWhiteSpace($BranchName)) -and ($BranchName -ne "master")) {
    $nugetPkgVersion = "$nugetPkgVersion-$BranchName";
}
Write-Host "Package version: $nugetPkgVersion";

# Authenticate with organization nuget feed
if (-not([string]::IsNullOrWhiteSpace($NugetAccessToken))) {
    if (-not $(Get-PackageSource -Name $NugetFeedName -ProviderName NuGet -ErrorAction Ignore))
    {
        $addOrgNugetCmd = "nuget sources add -Name $NugetFeedName -Source $NugetFeedSource -Username $NugetFeedUser -Password $NugetAccessToken";
        Write-Host "Registering with $NugetFeedSource";
        Invoke-Expression -Command $addOrgNugetCmd;
    }
}

# Run the build step if it exists
if ($Actions -contains "build") {
    .\build.ps1 -BuildVersion $nugetPkgVersion -BuildConfiguration $BuildConfiguration;
}

# Tun the unit test step if it exists
if ($Actions -contains "unit-test") {
    .\unit-test.ps1 -BuildVersion $nugetPkgVersion -BuildConfiguration $BuildConfiguration;
}

# Set build display number
if ($Actions -contains "set-build-display-number") {
    Write-Host "##vso[build.updateBuildNumber]$nugetPkgVersion";
}