using System.Collections.Generic;

namespace Dignite.Abp.LocaleManagement;

public class LocaleDto
{
    public LocaleDto()
    {
    }

    public LocaleDto(string defaultCultureName, IEnumerable<string> availableCultureNames)
    {
        DefaultCultureName = defaultCultureName;
        AvailableCultureNames = availableCultureNames;
    }

    public string DefaultCultureName { get; set; }

    public  IEnumerable<string> AvailableCultureNames { get; set; }
}
