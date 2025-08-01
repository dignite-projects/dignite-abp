using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;

namespace Dignite.Abp.LocaleManagement;

[Area(LocaleManagementRemoteServiceConsts.ModuleName)]
[RemoteService(Name = LocaleManagementRemoteServiceConsts.RemoteServiceName)]
[Route("api/locale-management/locale")]
public class LocaleController : LocaleManagementController, ILocaleAppService
{
    private readonly ILocaleAppService _localeAppService;

    public LocaleController(ILocaleAppService localeAppService)
    {
        _localeAppService = localeAppService;
    }

    [HttpGet]
    public async Task<LocaleDto> GetAsync()
    {
        return await _localeAppService.GetAsync();
    }

    [HttpPost]
    public async Task<LocaleDto> UpdateAsync(UpdateLocaleInput input)
    {
        return await _localeAppService.UpdateAsync(input);
    }
}
