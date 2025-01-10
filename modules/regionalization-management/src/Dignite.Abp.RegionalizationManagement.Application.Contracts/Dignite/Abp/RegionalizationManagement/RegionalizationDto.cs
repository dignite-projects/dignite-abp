using System.Collections.Generic;

namespace Dignite.Abp.RegionalizationManagement;

public class RegionalizationDto
{
    public RegionalizationDto()
    {
    }

    public RegionalizationDto(string defaultCultureName, IEnumerable<string> availableCultureNames)
    {
        DefaultCultureName = defaultCultureName;
        AvailableCultureNames = availableCultureNames;
    }

    public string DefaultCultureName { get; set; }

    public  IEnumerable<string> AvailableCultureNames { get; set; }
}
