using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Dignite.Abp.LocaleManagement;

public interface ILocaleAppService : IApplicationService
{
    Task<LocaleDto> GetAsync();

    Task<LocaleDto> UpdateAsync(UpdateLocaleInput input);
}
