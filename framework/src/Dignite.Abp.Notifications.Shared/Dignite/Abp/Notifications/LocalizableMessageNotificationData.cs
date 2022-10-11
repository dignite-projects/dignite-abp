using System;
using JetBrains.Annotations;
using Volo.Abp;

namespace Dignite.Abp.Notifications;

/// <summary>
/// Can be used to store a simple message as notification data.
/// </summary>
[Serializable]
public class LocalizableMessageNotificationData : NotificationData
{
    /// <summary>
    /// The localizabl string ResourceName.
    /// </summary>
    public string ResourceName {
        get { return _resourceName ?? (this[nameof(ResourceName)] as string); }
        set {
            this[nameof(ResourceName)] = value;
            _resourceName = value;
        }
    }
    private string _resourceName;

    /// <summary>
    /// The localizabl string name.
    /// </summary>
    public string Name {
        get { return _name ?? (this[nameof(Name)] as string); }
        set {
            this[nameof(Name)] = value;
            _name = value;
        }
    }
    private string _name;


    /// <summary>
    /// Needed for serialization.
    /// </summary>
    private LocalizableMessageNotificationData()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="LocalizableMessageNotificationData"/> class.
    /// </summary>
    /// <param name="name">The localizabl string name.</param>
    /// <param name="resourceName">The localizabl string resourceName.</param>
    public LocalizableMessageNotificationData([NotNull] string name, [CanBeNull] string resourceName = null)
    {
        Name = Check.NotNullOrEmpty(name, nameof(name));
        ResourceName = resourceName;
    }
}