using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Dignite.Cms.Admin.DynamicForms
{
    public interface IFormAdminAppService : IApplicationService
    {
        Task<ListResultDto<FormControlDto>> GetFormControlsAsync();
    }
}
