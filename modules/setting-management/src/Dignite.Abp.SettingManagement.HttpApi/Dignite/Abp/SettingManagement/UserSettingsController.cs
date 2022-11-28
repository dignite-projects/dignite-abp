using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc;

namespace Dignite.Abp.SettingManagement;

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
    [Route("groups")]
    public async Task<ListResultDto<SettingGroupDto>> GetAllGroupsAsync()
    {
        return await _userSettingsAppService.GetAllGroupsAsync();
    }

    [HttpGet]
    public async Task<ListResultDto<SettingDto>> GetListAsync(GetSettingsInput input)
    {
        return await _userSettingsAppService.GetListAsync(input);
    }

    [HttpPut]
    public async Task UpdateAllAsync(UpdateUserSettingsInput input)
    {
        await _userSettingsAppService.UpdateAllAsync(input);
    }
}