
param(
  [string]$Version,
  [string]$Registry
)
yarn login

yarn install

$NextVersion = $(node publish-utils.js --nextVersion)
$RootFolder = (Get-Item -Path "./" -Verbose).FullName

if (-Not $Version) {
  $Version = $NextVersion;
}

if (-Not $Registry) {
 $Registry = "https://registry.npmjs.org";
}
$UpdateNgPacksCommand = "yarn update-version $Version"
$NgPacksPublishCommand = "npm run publish-packages -- --nextVersion $Version --skipGit --registry $Registry --skipVersionValidation"
$UpdateGulpCommand = "yarn update-gulp --registry $Registry"


$IsPrerelease = $(node publish-utils.js --prerelease --customVersion $Version) -eq "true";

if ($IsPrerelease) {
  $UpdateGulpCommand += " --prerelease"
  $UpdateNgPacksCommand += " --prerelease"
}


$commands = (
  "cd ng-packs",
  "yarn install",
  $UpdateNgPacksCommand,
 "cd scripts",
 "yarn install",
  $NgPacksPublishCommand,
  "cd ../../",
  "cd scripts",
  "yarn remove-lock-files",
  "cd ..",
  $UpdateGulpCommand
)


foreach ($command in $commands) { 
  $timer = [System.Diagnostics.Stopwatch]::StartNew()
  Write-Host $command
  Invoke-Expression $command

if ($LASTEXITCODE -ne '0' -And $command -notlike '*cd *') {
    Write-Host ("Process failed! " + $command)
    Set-Location $RootFolder
    exit $LASTEXITCODE
  }
  $timer.Stop()
  $total = $timer.Elapsed
  Write-Output "-------------------------"
  Write-Output "$command command took $total (Hours:Minutes:Seconds.Milliseconds)"
  Write-Output "-------------------------"

}






<#


#脚本接受两个参数，$Version和$Registry。如果这些参数没有被提供，脚本会使用默认值。
param(
  [string]$Version,
  [string]$Registry
)
#脚本使用yarn install命令来安装项目的依赖项。
yarn install
#脚本使用node publish-utils.js --nextVersion命令来获取下一个版本号。
$NextVersion = $(node publish-utils.js --nextVersion)
#获取当前目录的完整路径，并将其存储在$RootFolder变量中12。
$RootFolder = (Get-Item -Path "./" -Verbose).FullName
#设置版本号和注册表：如果$Version和$Registry参数没有被提供，脚本会使用默认的版本号和注册表。
if (-Not $Version) {
  $Version = $NextVersion;
}

if (-Not $Registry) {
  $Registry = "https://registry.npmjs.org";
}
#定义命令：脚本定义了一些命令，这些命令用于更新版本号、发布包和更新gulp
$UpdateNgPacksCommand = "yarn update-version $Version"
$NgPacksPublishCommand = "npm run publish-packages -- --nextVersion $Version --skipGit --registry $Registry --skipVersionValidation"
$UpdateGulpCommand = "yarn update-gulp --registry $Registry"

#检查是否为预发布版本：脚本使用node publish-utils.js --prerelease --customVersion $Version命令来检查当前的版本是否为预发布版本
$IsPrerelease = $(node publish-utils.js --prerelease --customVersion $Version) -eq "true";

if ($IsPrerelease) {
  $UpdateGulpCommand += " --prerelease"
  $UpdateNgPacksCommand += " --prerelease"
}

#脚本定义了一个命令列表，这个列表包含了需要执行的所有命令。
$commands = (
  "cd ng-packs",
  "yarn install",
  $UpdateNgPacksCommand,
 "cd scripts",
 "yarn install",
  $NgPacksPublishCommand,
  "cd ../../",
  "cd scripts",
  "yarn remove-lock-files",
  "cd ..",
  $UpdateGulpCommand
)

#执行命令：脚本使用foreach循环来遍历并执行命令列表中的每个命令。对于每个命令，脚本会计时，打印命令，执行命令，然后打印命令的执行时间。
foreach ($command in $commands) { 
  $timer = [System.Diagnostics.Stopwatch]::StartNew()
  Write-Host $command
  Invoke-Expression $command

	if ($LASTEXITCODE -ne '0' -And $command -notlike '*cd *') {
		Write-Host ("Process failed! " + $command)
		Set-Location $RootFolder
		exit $LASTEXITCODE
	}
  $timer.Stop()
  $total = $timer.Elapsed
  Write-Output "-------------------------"
  Write-Output "$command command took $total (Hours:Minutes:Seconds.Milliseconds)"
  Write-Output "-------------------------"

}





#>
