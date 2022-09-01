using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc;

namespace Dignite.Abp.SettingManagement
{
    [Area(SettingManagementRemoteServiceConsts.ModuleName)]
    [RemoteService(Name = SettingManagementRemoteServiceConsts.RemoteServiceName)]
    [Route("api/setting-management/tenant-settings")]
    public class TenantSettingsController : AbpControllerBase, ITenantSettingsAppService
    {
        private readonly ITenantSettingsAppService _tenantSettingsAppService;

        public TenantSettingsController(ITenantSettingsAppService tenantSettingsAppService)
        {
            _tenantSettingsAppService = tenantSettingsAppService;
        }


        [HttpGet]
        public async Task<ListResultDto<SettingNavigationDto>> GetAllAsync()
        {
            return await _tenantSettingsAppService.GetAllAsync();
        }

        [HttpPut]
        public async Task UpdateAsync(UpdateTenantSettingsInput input)
        {
            await _tenantSettingsAppService.UpdateAsync(input);
        }
    }
}