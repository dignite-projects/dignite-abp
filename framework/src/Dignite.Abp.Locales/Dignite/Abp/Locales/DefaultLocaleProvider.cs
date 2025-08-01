using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Settings;

namespace Dignite.Abp.Locales;
/// <summary>
/// TODO:Using a singleton approach improves performance, see DefaultAbpRequestLocalizationOptionsProvider.cs for an implementation.
/// </summary>
public class DefaultLocaleProvider :
    ILocaleProvider,
    ITransientDependency
{
    private readonly ISettingProvider _settingProvider;

    public DefaultLocaleProvider(ISettingProvider settingProvider)
    {
        _settingProvider = settingProvider;
    }

    public async Task<LocaleInfo> GetLocaleAsync()
    {
        var defaultCultureName = await _settingProvider.GetOrNullAsync(LocaleSettingNames.DefaultCultureName);
        var availableCultureNames = await _settingProvider.GetOrNullAsync(LocaleSettingNames.AvailableCultureNames);

        if (defaultCultureName.IsNullOrEmpty())
        {
            throw new ArgumentNullException(nameof(defaultCultureName), "The data cannot be null.");
        }
        if (availableCultureNames.IsNullOrEmpty())
        {
            throw new ArgumentNullException(nameof(availableCultureNames), "The data cannot be null.");
        }

        return new LocaleInfo(
            new CultureInfo(defaultCultureName),
            availableCultureNames.Split(',')
                .Select(cultrueName => new CultureInfo(cultrueName))
                .ToList()
            );
    }
}
