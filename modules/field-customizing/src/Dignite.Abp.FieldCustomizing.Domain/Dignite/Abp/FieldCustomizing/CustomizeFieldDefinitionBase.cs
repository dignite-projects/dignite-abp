using System;
using Dignite.Abp.DynamicForms;
using Volo.Abp.Domain.Entities;

namespace Dignite.Abp.FieldCustomizing;

public abstract class CustomizeFieldDefinitionBase : AggregateRoot<Guid>, ICustomizeFieldInfo
{
    protected CustomizeFieldDefinitionBase(Guid id,string displayName, string name, string defaultValue, string formName, FormConfigurationDictionary formConfiguration)
        :base(id)
    {
        DisplayName = displayName;
        Name = name;
        DefaultValue = defaultValue;
        FormName = formName;
        FormConfiguration = formConfiguration;
    }

    /// <summary>
    /// 
    /// </summary>
    public virtual string DisplayName { get; set; }

    /// <summary>
    /// Field Unique Name
    /// </summary>
    public virtual string Name { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public virtual string DefaultValue { get; set; }

    /// <summary>
    /// Field <see cref="IForm.Name"/>
    /// </summary>
    public virtual string FormName { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public virtual FormConfigurationDictionary FormConfiguration { get; set; }

    public virtual void SetDisplayName(string displayName)
    {
        this.DisplayName = displayName;
    }
    public virtual void SetName(string name)
    {
        this.Name = name;
    }
    public virtual void SetDefaultValue(string defaultValue)
    {
        this.DefaultValue = defaultValue;
    }
    public virtual void SetFormName(string formName)
    {
        this.FormName = formName;
    }
    public virtual void SetFormConfigurationDictionary(FormConfigurationDictionary formConfiguration)
    {
        this.FormConfiguration = formConfiguration;
    }
}
