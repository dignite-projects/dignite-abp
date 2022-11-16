using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Dignite.Abp.SettingManagement;

public interface IUserSettingsAppService : IApplicationService
{
    Task<ListResultDto<SettingProviderDto>> GetAllAsync();

    Task UpdateAsync(UpdateUserSettingsInput input);
}