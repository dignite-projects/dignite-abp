using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Settings;

namespace Dignite.Abp.Regionalization;
/// <summary>
/// TODO:Using a singleton approach improves performance, see DefaultAbpRequestLocalizationOptionsProvider.cs for an implementation.
/// </summary>
public class DefaultRegionalizationProvider :
    IRegionalizationProvider,
    ITransientDependency
{
    private readonly ISettingProvider _settingProvider;

    public DefaultRegionalizationProvider(ISettingProvider settingProvider)
    {
        _settingProvider = settingProvider;
    }

    public async Task<Regionalization> GetRegionalizationAsync()
    {
        var defaultCultureName = await _settingProvider.GetOrNullAsync(RegionalizationSettingNames.DefaultCultureName);
        var availableCultureNames = await _settingProvider.GetOrNullAsync(RegionalizationSettingNames.AvailableCultureNames);

        if (defaultCultureName.IsNullOrEmpty())
        {
            throw new ArgumentNullException(nameof(defaultCultureName), "The data cannot be null.");
        }
        if (availableCultureNames.IsNullOrEmpty())
        {
            throw new ArgumentNullException(nameof(availableCultureNames), "The data cannot be null.");
        }

        return new Regionalization(
            new CultureInfo(defaultCultureName),
            availableCultureNames.Split(',')
                .Select(cultrueName => new CultureInfo(cultrueName))
                .ToList()
            );
    }
}
