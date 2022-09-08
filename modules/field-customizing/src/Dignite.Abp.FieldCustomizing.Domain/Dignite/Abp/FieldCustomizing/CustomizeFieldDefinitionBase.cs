using System;
using Dignite.Abp.FieldCustomizing.Fields;
using Volo.Abp.Domain.Entities;
using Volo.Abp.MultiTenancy;

namespace Dignite.Abp.FieldCustomizing;

public abstract class CustomizeFieldDefinitionBase :Entity<Guid>, ICustomizeFieldDefinition, IMultiTenant
{

    public Guid? TenantId { get; set; }

    /// <summary>
    /// Field Unique Name
    /// </summary>
    public string Name { get; set; }

    public string DisplayName { get; set; }

    public string DefaultValue { get; set; }

    /// <summary>
    /// Field Provider <see cref="IFieldProvider.Name"/>
    /// </summary>
    public string FieldProviderName { get; set; }

    /// <summary>
    /// Field Configuration
    /// </summary>
    public FieldConfigurationDictionary Configuration { get; set; }
}
