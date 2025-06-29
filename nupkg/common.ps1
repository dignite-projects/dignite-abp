# Paths
$packFolder = (Get-Item -Path "./" -Verbose).FullName
$rootFolder = Join-Path $packFolder "../"

function Write-Info   
{
	param(
        [Parameter(Mandatory = $true)]
        [string]
        $text
    )

	Write-Host $text -ForegroundColor Black -BackgroundColor Green

	try 
	{
	   $host.UI.RawUI.WindowTitle = $text
	}		
	catch 
	{
		#Changing window title is not suppoerted!
	}
}

function Write-Error   
{
	param(
        [Parameter(Mandatory = $true)]
        [string]
        $text
    )

	Write-Host $text -ForegroundColor Red -BackgroundColor Black 
}

function Seperator   
{
	Write-Host ("_" * 100)  -ForegroundColor gray 
}

function Get-Current-Version { 
	$commonPropsFilePath = resolve-path "../common.props"
	$commonPropsXmlCurrent = [xml](Get-Content $commonPropsFilePath ) 
	$currentVersion = $commonPropsXmlCurrent.Project.PropertyGroup.Version.Trim()
	return $currentVersion
}

function Get-Current-Branch {
	return git branch --show-current
}	   

function Read-File {
	param(
        [Parameter(Mandatory = $true)]
        [string]
        $filePath
    )
		
	$pathExists = Test-Path -Path $filePath -PathType Leaf
	if ($pathExists)
	{
		return Get-Content $filePath		
	}
	else{
		Write-Error  "$filePath path does not exist!"
	}
}

# List of solutions
$solutions = (
    "framework",
    "modules/ckeditor-component",
    "modules/file-explorer",
    "modules/files",
    "modules/notification-center",
    "modules/pure-theme",
    "modules/user-points",
    "modules/cms",
    "modules/regionalization-management",
    "modules/tenant-domain-management"
)

# List of projects
$projects = (

    # framework
    "framework/src/Dignite.Abp.AspNetCore.Mvc.UI.TenantTheme",
    "framework/src/Dignite.Abp.BlazoriseUI",
    "framework/src/Dignite.Abp.DynamicForms",
    "framework/src/Dignite.Abp.DynamicForms.Components",
    "framework/src/Dignite.Abp.DynamicForms.Components.BlazoriseUI",
    "framework/src/Dignite.Abp.TenantLocalization",
    "framework/src/Dignite.Abp.Notifications",
    "framework/src/Dignite.Abp.Notifications.Components",
    "framework/src/Dignite.Abp.Notifications.Shared",
    "framework/src/Dignite.Abp.Notifications.SignalRNotifier",
    "framework/src/Dignite.Abp.Points",
    "framework/src/Dignite.Abp.AspNetCore.Mvc.Regionalization",
    "framework/src/Dignite.Abp.Regionalization",
    "framework/src/Dignite.Abp.Seo",
    "framework/src/Dignite.Abp.TenantDomain",
    "framework/src/Dignite.Abp.TenantDomain.Caddy",
    "framework/src/Dignite.Abp.TenantDomain.OpenIddict",

    # modules/ckeditor-component
    "modules/ckeditor-component/src/Dignite.Abp.AspNetCore.Components.CkEditor",
    "modules/ckeditor-component/src/Dignite.Abp.AspNetCore.Components.CkEditor.Server",
    "modules/ckeditor-component/src/Dignite.Abp.AspNetCore.Components.CkEditor.WebAssembly",
    "modules/ckeditor-component/src/Dignite.Abp.DynamicForms.CkEditor",
    "modules/ckeditor-component/src/Dignite.Abp.DynamicForms.Components.CkEditor",
        
    # modules/file-explorer
    "modules/file-explorer/src/Dignite.Abp.DynamicForms.Components.FileExplorer",
    "modules/file-explorer/src/Dignite.Abp.DynamicForms.FileExplorer",
    "modules/file-explorer/src/Dignite.FileExplorer.Application",
    "modules/file-explorer/src/Dignite.FileExplorer.Application.Contracts",
    "modules/file-explorer/src/Dignite.FileExplorer.Blazor",
    "modules/file-explorer/src/Dignite.FileExplorer.Blazor.Server",
    "modules/file-explorer/src/Dignite.FileExplorer.Blazor.WebAssembly",
    "modules/file-explorer/src/Dignite.FileExplorer.Domain",
    "modules/file-explorer/src/Dignite.FileExplorer.Domain.Shared",
    "modules/file-explorer/src/Dignite.FileExplorer.EntityFrameworkCore",
    "modules/file-explorer/src/Dignite.FileExplorer.HttpApi",
    "modules/file-explorer/src/Dignite.FileExplorer.HttpApi.Client",
    "modules/file-explorer/src/Dignite.FileExplorer.Installer",
    "modules/file-explorer/src/Dignite.FileExplorer.MongoDB",

    # modules/files
    "modules/files/src/Dignite.Abp.Files.Domain",
    "modules/files/src/Dignite.Abp.Files.Domain.Shared",
    "modules/files/src/Dignite.Abp.Files.EntityFrameworkCore",
    "modules/files/src/Dignite.Abp.Files.Installer",
    "modules/files/src/Dignite.Abp.Files.MongoDB",

    # modules/notification-center
    "modules/notification-center/src/Dignite.Abp.NotificationCenter.Application",
    "modules/notification-center/src/Dignite.Abp.NotificationCenter.Application.Contracts",
    "modules/notification-center/src/Dignite.Abp.NotificationCenter.Blazor",
    "modules/notification-center/src/Dignite.Abp.NotificationCenter.Blazor.Server",
    "modules/notification-center/src/Dignite.Abp.NotificationCenter.Blazor.WebAssembly",
    "modules/notification-center/src/Dignite.Abp.NotificationCenter.Domain",
    "modules/notification-center/src/Dignite.Abp.NotificationCenter.Domain.Shared",
    "modules/notification-center/src/Dignite.Abp.NotificationCenter.EntityFrameworkCore",
    "modules/notification-center/src/Dignite.Abp.NotificationCenter.HttpApi",
    "modules/notification-center/src/Dignite.Abp.NotificationCenter.HttpApi.Client",
    "modules/notification-center/src/Dignite.Abp.NotificationCenter.Installer",
    "modules/notification-center/src/Dignite.Abp.NotificationCenter.MongoDB",
    "modules/notification-center/src/Dignite.Abp.Notifications.Identity",

    # modules/pure-theme
    "modules/pure-theme/src/Dignite.Abp.AspNetCore.Components.Server.PureTheme",
    "modules/pure-theme/src/Dignite.Abp.AspNetCore.Components.Web.PureTheme",
    "modules/pure-theme/src/Dignite.Abp.AspNetCore.Components.WebAssembly.PureTheme",
    "modules/pure-theme/src/Dignite.Abp.AspNetCore.Mvc.UI.Theme.Pure",

    # modules/user-points
    "modules/user-points/src/Dignite.Abp.UserPoints.Application",
    "modules/user-points/src/Dignite.Abp.UserPoints.Application.Contracts",
    "modules/user-points/src/Dignite.Abp.UserPoints.Domain",
    "modules/user-points/src/Dignite.Abp.UserPoints.Domain.Shared",
    "modules/user-points/src/Dignite.Abp.UserPoints.EntityFrameworkCore",
    "modules/user-points/src/Dignite.Abp.UserPoints.HttpApi",
    "modules/user-points/src/Dignite.Abp.UserPoints.HttpApi.Client",
    "modules/user-points/src/Dignite.Abp.UserPoints.Installer",
    "modules/user-points/src/Dignite.Abp.UserPoints.MongoDB",

    # modules/cms
    "modules/cms/src/Dignite.Cms.Application",
    "modules/cms/src/Dignite.Cms.Application.Contracts",
    "modules/cms/src/Dignite.Cms.Domain",
    "modules/cms/src/Dignite.Cms.Domain.Shared",
    "modules/cms/src/Dignite.Cms.EntityFrameworkCore",
    "modules/cms/src/Dignite.Cms.HttpApi",
    "modules/cms/src/Dignite.Cms.HttpApi.Client",
    "modules/cms/src/Dignite.Cms.Installer",
    "modules/cms/src/Dignite.Cms.Admin.Application",
    "modules/cms/src/Dignite.Cms.Admin.Application.Contracts",
    "modules/cms/src/Dignite.Cms.Admin.Blazor",
    "modules/cms/src/Dignite.Cms.Admin.Blazor.Server",
    "modules/cms/src/Dignite.Cms.Admin.Blazor.WebAssembly",
    "modules/cms/src/Dignite.Cms.Admin.HttpApi",    
    "modules/cms/src/Dignite.Cms.Admin.HttpApi.Client",
    "modules/cms/src/Dignite.Cms.Common.Application.Contracts",
    "modules/cms/src/Dignite.Cms.Public.Application",
    "modules/cms/src/Dignite.Cms.Public.Application.Contracts",
    "modules/cms/src/Dignite.Cms.Public.HttpApi",
    "modules/cms/src/Dignite.Cms.Public.HttpApi.Client",
    "modules/cms/src/Dignite.Cms.Public.Web",

    # modules/regionalization-management
    "modules/regionalization-management/src/Dignite.Abp.RegionalizationManagement.Application",
    "modules/regionalization-management/src/Dignite.Abp.RegionalizationManagement.Application.Contracts",
    "modules/regionalization-management/src/Dignite.Abp.RegionalizationManagement.HttpApi",
    "modules/regionalization-management/src/Dignite.Abp.RegionalizationManagement.HttpApi.Client",      
    "modules/regionalization-management/src/Dignite.Abp.RegionalizationManagement.Installer",
    
    # modules/tenant-domain-management
    "modules/tenant-domain-management/src/Dignite.Abp.TenantDomainManagement.Application",
    "modules/tenant-domain-management/src/Dignite.Abp.TenantDomainManagement.Application.Contracts",
    "modules/tenant-domain-management/src/Dignite.Abp.TenantDomainManagement.HttpApi",
    "modules/tenant-domain-management/src/Dignite.Abp.TenantDomainManagement.HttpApi.Client",
    "modules/tenant-domain-management/src/Dignite.Abp.TenantDomainManagement.Installer"
)
