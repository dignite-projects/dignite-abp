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
    [Route("api/cms-admin/fields")]
    public class FieldAdminController : CmsAdminController, IFieldAdminAppService
    {
        private readonly IFieldAdminAppService _fieldAppService;

        public FieldAdminController(IFieldAdminAppService fieldAppService)
        {
            _fieldAppService = fieldAppService;
        }


        [HttpPost]
        public async Task<FieldDto> CreateAsync(CreateFieldInput input)
        {
            return await _fieldAppService.CreateAsync(input);
        }


        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<FieldDto> UpdateAsync(Guid id, UpdateFieldInput input)
        {
            return await _fieldAppService.UpdateAsync(id, input);
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
            await _fieldAppService.DeleteAsync(id);
        }

        [HttpGet]
        public async Task<PagedResultDto<FieldDto>> GetListAsync(GetFieldsInput input)
        {
            return await _fieldAppService.GetListAsync(input);
        }

        [HttpGet]
        [Route("{id:Guid}")]

        public async Task<FieldDto> GetAsync(Guid id)
        {
            return await _fieldAppService.GetAsync(id);
        }

        [HttpGet]
        [Route("name-exists/{name}")]
        public async Task<bool> NameExistsAsync(string name)
        {
            return await _fieldAppService.NameExistsAsync(name);
        }
    }
}
