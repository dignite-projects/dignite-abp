
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Dignite.Abp.SettingManagement
{
    public interface ITenantSettingsAppService : IApplicationService
    {
        Task<ListResultDto<SettingNavigationDto>> GetAllAsync();

        Task UpdateAsync(UpdateTenantSettingsInput input);
    }
}
