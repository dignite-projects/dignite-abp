using System.Collections.Generic;
using System.Globalization;

namespace Dignite.Abp.Locales;
public class LocaleInfo
{
    protected LocaleInfo() { }

    public LocaleInfo(CultureInfo defaultCulture, IReadOnlyList<CultureInfo> availableCultures)
    {
        DefaultCulture = defaultCulture;
        AvailableCultures = availableCultures;
    }

    public virtual CultureInfo DefaultCulture { get; protected set; } = default!;

    public virtual IReadOnlyList<CultureInfo> AvailableCultures { get; protected set; } = default!;
}