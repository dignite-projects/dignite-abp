using System.Threading.Tasks;
using Dignite.CmsKit.Permissions;
using Dignite.CmsKit.Settings;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.SettingManagement;

namespace Dignite.CmsKit.Admin.Brand;

[Authorize(CmsKitAdminPermissions.Brand.Default)]
public class BrandAdminAppService : CmsKitAdminAppServiceBase, IBrandAdminAppService
{
    protected readonly ISettingManager SettingManager;

    public BrandAdminAppService(ISettingManager settingManager)
    {
        SettingManager = settingManager;
    }


    [Authorize(CmsKitAdminPermissions.Brand.Update)]
    public async Task UpdateAsync(UpdateBrandInput input)
    {
        await SettingManager.SetForTenantOrGlobalAsync(CurrentTenant.Id, CmsKitSettings.BrandName, input.Name);
        await SettingManager.SetForTenantOrGlobalAsync(CurrentTenant.Id, CmsKitSettings.BrandLogo, input.LogoBlobName);
        await SettingManager.SetForTenantOrGlobalAsync(CurrentTenant.Id, CmsKitSettings.BrandLogoReverse, input.LogoReverseBlobName);
        await SettingManager.SetForTenantOrGlobalAsync(CurrentTenant.Id, CmsKitSettings.BrandIcon, input.IconBlobName);
    }
}

