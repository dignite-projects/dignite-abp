using System;
using System.Reflection;
using JetBrains.Annotations;
using Volo.Abp;

namespace Dignite.Abp.Settings.DynamicForms;

public class SettingDefinitionProviderNameAttribute : Attribute
{
    [NotNull]
    public string Name { get; }

    public SettingDefinitionProviderNameAttribute([NotNull] string name)
    {
        Check.NotNullOrWhiteSpace(name, nameof(name));

        Name = name;
    }

    public virtual string GetName(Type type)
    {
        return Name;
    }

    public static string GetProviderName<T>()
        where T: ISettingDefinitionFormProvider
    {
        return GetProviderName(typeof(T));
    }

    public static string GetProviderName(Type type)
    {
        var nameAttribute = type.GetCustomAttribute<SettingDefinitionProviderNameAttribute>();

        if (nameAttribute == null)
        {
            return type.FullName;
        }

        return nameAttribute.GetName(type);
    }
}
