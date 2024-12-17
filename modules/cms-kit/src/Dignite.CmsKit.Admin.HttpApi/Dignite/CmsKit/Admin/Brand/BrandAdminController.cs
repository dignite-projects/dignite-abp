using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;

namespace Dignite.CmsKit.Admin.Brand;

[RemoteService(Name = CmsKitAdminRemoteServiceConsts.RemoteServiceName)]
[Area(CmsKitAdminRemoteServiceConsts.ModuleName)]
[Route("api/cms-kit/admin/brand")]
public class BrandAdminController : CmsKitAdminControllerBase, IBrandAdminAppService
{
    public BrandAdminController(IBrandAdminAppService brandAdminAppService)
    {
        BrandAdminAppService = brandAdminAppService;
    }

    protected IBrandAdminAppService BrandAdminAppService { get; }

    [HttpPut]
    public async Task UpdateAsync(UpdateBrandInput input)
    {
        await BrandAdminAppService.UpdateAsync(input);
    }
}
