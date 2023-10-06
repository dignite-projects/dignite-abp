param(
  [string]$branch,
  [string]$newVersion,
  [string]$isRcVersion
) 

. ..\nupkg\common.ps1

Start-Transcript -Append _run_all_log.txt

if (!$branch)
{
	$branch = Read-Host "Enter the branch name"
} 

if (!$newVersion)
{
	$currentVersion = Get-Current-Version
	$newVersion = Read-Host "Current version is '$currentVersion'. Enter the new version (empty for no change) "
	if($newVersion -eq "")
	{
		$newVersion = $currentVersion
	}
} 
	 
if ($isRcVersion -eq "")
{
	$isRcVersion = Read-Host "Is this a RC/Preview version? (y/n)"	
}

$publishGithubReleaseParams = @{
    branchName=$branch 
	isRcVersion=$isRcVersion 
}
  

./1-fetch-and-build.ps1 $branch $newVersion
./2-nuget-pack.ps1
./3-nuget-push.ps1
./4-publish-github-release.ps1 @publishGithubReleaseParams

Stop-Transcript