using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;

namespace Dignite.CmsKit.Brand;

[RemoteService(Name = CmsKitCommonRemoteServiceConsts.RemoteServiceName)]
[Area(CmsKitCommonRemoteServiceConsts.ModuleName)]
[Route("api/cms-kit/brand")]
public class BrandController : CmsKitCommonControllerBase, IBrandAppService
{
    public BrandController(IBrandAppService brandPublicAppService)
    {
        BrandPublicAppService = brandPublicAppService;
    }

    public IBrandAppService BrandPublicAppService { get; }

    [HttpGet]
    public async Task<BrandDto> GetAsync()
    {
        return await BrandPublicAppService.GetAsync();
    }
}
