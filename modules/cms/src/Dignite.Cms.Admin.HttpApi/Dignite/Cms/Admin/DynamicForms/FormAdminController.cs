using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace Dignite.Cms.Admin.DynamicForms
{
    [RemoteService(Name = CmsAdminRemoteServiceConsts.RemoteServiceName)]
    [Area(CmsAdminRemoteServiceConsts.ModuleName)]
    [Authorize(Permissions.CmsAdminPermissions.Field.Default)]
    [Route("api/cms-admin/dynamic-forms")]
    public class FormAdminController : CmsAdminController, IFormAdminAppService
    {
        private readonly IFormAdminAppService _formAdminAppService;

        public FormAdminController(IFormAdminAppService formAdminAppService)
        {
            _formAdminAppService = formAdminAppService;
        }

        [HttpGet]
        [Route("controls")]
        public async Task<ListResultDto<FormControlDto>> GetFormControlsAsync()
        {
            return await _formAdminAppService.GetFormControlsAsync();
        }
    }
}
