using Dignite.Cms.Fields;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Volo.Abp;

namespace Dignite.Cms.Public.Fields
{
    [RemoteService(Name = CmsPublicRemoteServiceConsts.RemoteServiceName)]
    [Area(CmsPublicRemoteServiceConsts.ModuleName)]
    [Route("api/cms-public/fields")]
    public class FieldPublicController : CmsPublicController, IFieldPublicAppService
    {
        private readonly IFieldPublicAppService _fieldAppService;

        public FieldPublicController(IFieldPublicAppService fieldAppService)
        {
            _fieldAppService = fieldAppService;
        }

        [HttpGet]
        [Route("find-by-name")]
        public async Task<FieldDto> FindByNameAsync(string name)
        {
            return await _fieldAppService.FindByNameAsync(name);
        }
    }
}
