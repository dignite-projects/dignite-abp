using System.Collections.Generic;
using System.Globalization;

namespace Dignite.Abp.Regionalization;
public class Regionalization
{
    protected Regionalization() { }

    public Regionalization(CultureInfo defaultCulture, IReadOnlyList<CultureInfo> availableCultures)
    {
        DefaultCulture = defaultCulture;
        AvailableCultures = availableCultures;
    }

    public virtual CultureInfo DefaultCulture { get; protected set; } = default!;

    public virtual IReadOnlyList<CultureInfo> AvailableCultures { get; protected set; } = default!;
}