using System.Threading.Tasks;
using Dignite.CmsKit.Settings;

namespace Dignite.CmsKit.Brand;
public class BrandAppService : CmsKitCommonAppServiceBase, IBrandAppService
{
    public async Task<BrandDto> GetAsync()
    {
        var brandName = await SettingProvider.GetOrNullAsync(CmsKitSettings.BrandName);
        var brandLogo = await SettingProvider.GetOrNullAsync(CmsKitSettings.BrandLogo);
        var brandLogoReverse = await SettingProvider.GetOrNullAsync(CmsKitSettings.BrandLogoReverse);
        var brandIcon = await SettingProvider.GetOrNullAsync(CmsKitSettings.BrandIcon);

        return new BrandDto(brandName, brandLogo, brandLogoReverse, brandIcon);
    }
}
