using Dignite.Cms.Admin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
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
        [Route("forms")]
        public async Task<ListResultDto<FormControlDto>> GetFormControlsAsync()
        {
            return await _formAdminAppService.GetFormControlsAsync();
        }
    }
}
