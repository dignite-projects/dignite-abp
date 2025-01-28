using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace Dignite.Cms.Admin.Entries
{
    [RemoteService(Name = CmsAdminRemoteServiceConsts.RemoteServiceName)]
    [Area(CmsAdminRemoteServiceConsts.ModuleName)]
    [Authorize(Permissions.CmsAdminPermissions.Entry.Default)]
    [Route("api/cms-admin/entries")]
    public class EntryAdminController : CmsAdminController, IEntryAdminAppService
    {
        private readonly IEntryAdminAppService _entryAppService;

        public EntryAdminController(IEntryAdminAppService entryAppService)
        {
            _entryAppService = entryAppService;
        }


        /// <summary>
        /// 创建或更新条目
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>


        [HttpPost]
        public async Task<EntryDto> CreateAsync(CreateEntryInput input)
        {
            return await _entryAppService.CreateAsync(input);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>

        [HttpPut]
        public async Task<EntryDto> UpdateAsync(Guid id, UpdateEntryInput input)
        {
            return await _entryAppService.UpdateAsync(id, input);
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
            await _entryAppService.DeleteAsync(id);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<PagedResultDto<EntryDto>> GetListAsync(GetEntriesInput input)
        {
            return await _entryAppService.GetListAsync(input);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<EntryDto> GetAsync(Guid id)
        {
            return await _entryAppService.GetAsync(id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id:Guid}/all-versions")]
        public async Task<ListResultDto<EntryDto>> GetAllVersionsAsync(Guid id)
        {
            return await _entryAppService.GetAllVersionsAsync(id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("activate/{id:Guid}")]
        public async Task ActivateAsync(Guid id)
        {
            await _entryAppService.ActivateAsync(id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("move/{id:Guid}")]
        public async Task MoveAsync(Guid id, MoveEntryInput input)
        {
            await _entryAppService.MoveAsync(id, input);
        }

        [HttpGet]
        [Route("slug-exists")]
        public Task<bool> SlugExistsAsync(SlugExistsInput input)
        {
            return _entryAppService.SlugExistsAsync(input);
        }

        [HttpGet]
        [Route("culture-exists-with-single-section")]
        public Task<bool> CultureExistWithSingleSectionAsync(CultureExistWithSingleSectionInput input)
        {
            return _entryAppService.CultureExistWithSingleSectionAsync(input);
        }

        [HttpGet]
        [Route("search-by-ids")]
        public Task<ListResultDto<EntryDto>> GetListByIdsAsync(Guid sectionId, IEnumerable<Guid> ids)
        {
            return _entryAppService.GetListByIdsAsync(sectionId, ids);
        }
    }
}
