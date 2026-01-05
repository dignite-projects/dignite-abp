using System;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Localization;

namespace Dignite.Abp.UserPoints;

public class UserPointTypeDefinition : IEquatable<UserPointTypeDefinition>
{
    public UserPointTypeDefinition(
        [NotNull] string name,
        ILocalizableString? displayName = null,
        ILocalizableString? description = null,
        int defaultExpirationDays = 365 * 100,
        bool isEnabled = true,
        int consumptionPriority = 100
        )
    {
        Name = Check.NotNullOrEmpty(name, nameof(name));
        DisplayName = displayName ?? new FixedLocalizableString(name);
        Description = description;
        DefaultExpirationDays = defaultExpirationDays;
        IsEnabled = isEnabled;
        ConsumptionPriority = consumptionPriority;
    }

    /// <summary>
    /// Unique name of the point type.
    /// </summary>
    [NotNull]
    public string Name { get; }

    [NotNull]
    public ILocalizableString DisplayName {
        get => _displayName;
        set => _displayName = Check.NotNull(value, nameof(value));
    }
    private ILocalizableString _displayName = default!;

    public ILocalizableString? Description { get; set; }

    public int DefaultExpirationDays { get; set; }

    public bool IsEnabled { get; set; }

    /// 消费优先级（数值越小越优先消费）
    /// 默认值：100
    /// 推荐范围：1-999
    [ValueRange(1, 999)]
    public int ConsumptionPriority { get; set; }

    public bool Equals(UserPointTypeDefinition other)
    {
        return Name == other?.Name;
    }
}
