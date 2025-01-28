using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace Dignite.Cms.Admin.Fields
{
    [RemoteService(Name = CmsAdminRemoteServiceConsts.RemoteServiceName)]
    [Area(CmsAdminRemoteServiceConsts.ModuleName)]
    [Authorize(Permissions.CmsAdminPermissions.Field.Default)]
    [Route("api/cms-admin/field-groups")]
    public class FieldGroupAdminController : CmsAdminController, IFieldGroupAdminAppService
    {
        private readonly IFieldGroupAdminAppService _fieldGroupAppService;

        public FieldGroupAdminController(IFieldGroupAdminAppService fieldGroupAppService)
        {
            _fieldGroupAppService = fieldGroupAppService;
        }

        [HttpPost]
        public async Task<FieldGroupDto> CreateAsync(CreateOrUpdateFieldGroupInput input)
        {
            return await _fieldGroupAppService.CreateAsync(input);
        }


        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<FieldGroupDto> UpdateAsync(Guid id, CreateOrUpdateFieldGroupInput input)
        {
            return await _fieldGroupAppService.UpdateAsync(id, input);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task DeleteAsync(Guid id)
        {
            await _fieldGroupAppService.DeleteAsync(id);
        }

        [HttpGet]
        public async Task<PagedResultDto<FieldGroupDto>> GetListAsync(GetFieldGroupsInput input)
        {
            return await _fieldGroupAppService.GetListAsync(input);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<FieldGroupDto> GetAsync(Guid id)
        {
            return await _fieldGroupAppService.GetAsync(id);
        }
    }
}
