using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Dignite.Abp.SettingManagement;

public interface IUserSettingsAppService : IApplicationService
{
    Task<ListResultDto<SettingGroupDto>> GetAllGroupsAsync();

    Task<ListResultDto<SettingDto>> GetListAsync(GetSettingsInput input);

    Task UpdateAllAsync(UpdateUserSettingsInput input);
}