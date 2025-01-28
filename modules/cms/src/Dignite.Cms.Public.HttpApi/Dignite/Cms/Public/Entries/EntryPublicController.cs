using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace Dignite.Cms.Public.Entries
{
    [RemoteService(Name = CmsPublicRemoteServiceConsts.RemoteServiceName)]
    [Area(CmsPublicRemoteServiceConsts.ModuleName)]
    [Route("api/cms-public/entries")]
    public class EntryPublicController : CmsPublicController, IEntryPublicAppService
    {
        private readonly IEntryPublicAppService _entryAppService;

        public EntryPublicController(IEntryPublicAppService entryAppService)
        {
            _entryAppService = entryAppService;
        }



        [HttpGet]
        [Route("find-by-slug")]
        public async Task<EntryDto> FindBySlugAsync(FindBySlugInput input)
        {
            var entry = await _entryAppService.FindBySlugAsync(input);
            return entry;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id:Guid}/prev")]
        public async Task<EntryDto> FindPrevAsync(Guid id)
        {
            var entry = await _entryAppService.FindPrevAsync(id);
            return entry;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id:Guid}/next")]
        public async Task<EntryDto> FindNextAsync(Guid id)
        {
            var entry = await _entryAppService.FindNextAsync(id);
            return entry;
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
            var entry = await _entryAppService.GetAsync(id);
            return entry;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<PagedResultDto<EntryDto>> GetListAsync(GetEntriesInput input)
        {
            var result = await _entryAppService.GetListAsync(input);
            return result;
        }

    }
}
