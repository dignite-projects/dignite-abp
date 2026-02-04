using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace Dignite.Cms.Public.Sections
{
    [RemoteService(Name = CmsPublicRemoteServiceConsts.RemoteServiceName)]
    [Area(CmsPublicRemoteServiceConsts.ModuleName)]
    [Route("api/cms-public/sections")]
    public class SectionPublicController : CmsPublicController, ISectionPublicAppService
    {
        private readonly ISectionPublicAppService _sectionAppService;

        public SectionPublicController(ISectionPublicAppService sectionAppService)
        {
            _sectionAppService = sectionAppService;
        }

        [HttpGet]
        [Route("find-by-name")]
        public async Task<SectionDto> FindByNameAsync( string name)
        {
            return await _sectionAppService.FindByNameAsync( name);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="entryPath">
        /// The entry path does not contain culture.
        /// </param>
        /// <returns></returns>
        [HttpGet]
        [Route("find-by-entry-path")]
        public async Task<SectionDto> FindByEntryPathAsync(string entryPath)
        {
            return await _sectionAppService.FindByEntryPathAsync(entryPath);
        }

        [HttpGet]
        public async Task<ListResultDto<SectionDto>> GetListAsync(GetSectionsInput input)
        {
            return await _sectionAppService.GetListAsync(input);
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<SectionDto> GetAsync(Guid id)
        {
            return await _sectionAppService.GetAsync(id);
        }

        [HttpGet]
        [Route("default")]
        public async Task<SectionDto> GetDefaultAsync()
        {
            return await _sectionAppService.GetDefaultAsync();
        }
    }
}
