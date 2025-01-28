using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Volo.Abp;

namespace Dignite.Cms.Admin.Sections
{
    [RemoteService(Name = CmsAdminRemoteServiceConsts.RemoteServiceName)]
    [Area(CmsAdminRemoteServiceConsts.ModuleName)]
    [Authorize(Permissions.CmsAdminPermissions.Section.Default)]
    [Route("api/cms-admin/entry-types")]
    public class EntryTypeAdminController : CmsAdminController, IEntryTypeAdminAppService
    {
        private readonly IEntryTypeAdminAppService _entryTypeAppService;

        public EntryTypeAdminController(IEntryTypeAdminAppService entryTypeAppService)
        {
            _entryTypeAppService = entryTypeAppService;
        }

        [HttpPost]
        public async Task<EntryTypeDto> CreateAsync(CreateEntryTypeInput input)
        {
            return await _entryTypeAppService.CreateAsync(input);
        }


        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<EntryTypeDto> UpdateAsync(Guid id, UpdateEntryTypeInput input)
        {
            return await _entryTypeAppService.UpdateAsync(id, input);
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
            await _entryTypeAppService.DeleteAsync(id);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<EntryTypeDto> GetAsync(Guid id)
        {
            return await _entryTypeAppService.GetAsync(id);
        }

        [HttpGet]
        [Route("name-exists")]
        public async Task<bool> NameExistsAsync(EntryTypeNameExistsInput input)
        {
            return await _entryTypeAppService.NameExistsAsync(input);
        }
    }
}
