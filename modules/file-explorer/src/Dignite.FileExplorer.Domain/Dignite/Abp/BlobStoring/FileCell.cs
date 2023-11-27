using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Localization;

namespace Dignite.Abp.BlobStoring;
public class FileCell
{
    /// <summary>
    /// Cell name of the File Grid.
    /// </summary>
    [NotNull]
    public string Name { get; }

    [NotNull]
    public ILocalizableString DisplayName {
        get => _displayName;
        set => _displayName = Check.NotNull(value, nameof(value));
    }
    private ILocalizableString _displayName = default!;

    public FileCell(string name, ILocalizableString? displayName=null)
    {
        Name = name;
        DisplayName = displayName ?? new FixedLocalizableString(name);
    }
}
