using System;
using Dignite.Abp.DynamicForms;
using Volo.Abp.Domain.Entities;
using Volo.Abp.MultiTenancy;

namespace Dignite.Abp.FieldCustomizing;

public abstract class CustomizeFieldDefinitionBase : Entity<Guid>, ICustomizeFieldInfo, IMultiTenant
{
    public Guid? TenantId { get; set; }

    /// <summary>
    /// Field Unique Name
    /// </summary>
    public string Name { get; set; }

    public string DisplayName { get; set; }

    public string DefaultValue { get; set; }

    /// <summary>
    /// Field <see cref="IForm.Name"/>
    /// </summary>
    public string FormName { get; set; }

    public FormConfigurationDictionary FormConfiguration { get; set; }
}
