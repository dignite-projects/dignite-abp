using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc;

namespace Dignite.Abp.SettingManagement
{
    [Area(SettingManagementRemoteServiceConsts.ModuleName)]
    [RemoteService(Name = SettingManagementRemoteServiceConsts.RemoteServiceName)]
    [Route("api/setting-management/user-settings")]
    public class UserSettingsController : AbpControllerBase, IUserSettingsAppService
    {
        private readonly IUserSettingsAppService _userSettingsAppService;

        public UserSettingsController(IUserSettingsAppService userSettingsAppService)
        {
            _userSettingsAppService = userSettingsAppService;
        }


        [HttpGet]
        public async Task<ListResultDto<SettingNavigationDto>> GetAllAsync()
        {
            return await _userSettingsAppService.GetAllAsync();
        }

        [HttpPut]
        public async Task UpdateAsync(UpdateUserSettingsInput input)
        {
            await _userSettingsAppService.UpdateAsync(input);
        }
    }
}