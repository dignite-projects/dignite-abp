using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace Dignite.Cms.Admin.Sections
{
    [RemoteService(Name = CmsAdminRemoteServiceConsts.RemoteServiceName)]
    [Area(CmsAdminRemoteServiceConsts.ModuleName)]
    [Authorize(Permissions.CmsAdminPermissions.Section.Default)]
    [Route("api/cms-admin/sections")]
    public class SectionAdminController : CmsAdminController, ISectionAdminAppService
    {
        private readonly ISectionAdminAppService _sectionAppService;

        public SectionAdminController(ISectionAdminAppService sectionAppService)
        {
            _sectionAppService = sectionAppService;
        }


        [HttpPost]
        public async Task<SectionDto> CreateAsync(CreateSectionInput input)
        {
            return await _sectionAppService.CreateAsync(input);
        }


        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<SectionDto> UpdateAsync(Guid id, UpdateSectionInput input)
        {
            return await _sectionAppService.UpdateAsync(id, input);
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
            await _sectionAppService.DeleteAsync(id);
        }

        [HttpGet]
        public async Task<PagedResultDto<SectionDto>> GetListAsync(GetSectionsInput input)
        {
            return await _sectionAppService.GetListAsync(input);
        }

        [HttpGet]
        [Route("{id:Guid}")]

        public async Task<SectionDto> GetAsync(Guid id)
        {
            return await _sectionAppService.GetAsync(id);
        }


        [HttpGet]
        [Route("name-exists")]
        public async Task<bool> NameExistsAsync(SectionNameExistsInput input)
        {
            return await _sectionAppService.NameExistsAsync(input);
        }

        [HttpGet]
        [Route("route-exists")]
        public async Task<bool> RouteExistsAsync(SectionRouteExistsInput input)
        {
            return await _sectionAppService.RouteExistsAsync(input);
        }
    }
}
