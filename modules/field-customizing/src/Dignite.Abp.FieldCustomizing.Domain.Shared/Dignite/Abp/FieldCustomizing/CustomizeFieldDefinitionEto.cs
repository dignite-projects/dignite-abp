using System;
using Dignite.Abp.DynamicForms;
using Volo.Abp.EventBus;

namespace Dignite.Abp.FieldCustomizing;

[EventName("Dignite.Abp.FieldCustomizing.FieldDefinition")]
public class CustomizeFieldDefinitionEto : ICustomizeField
{
    public Guid Id { get; set; }

    public Guid? TenantId { get; set; }

    public string Name { get; set; }

    public string DisplayName { get; set; }

    public string DefaultValue { get; set; }

    public string FormProviderName { get; set; }

    public FormConfigurationDictionary FormConfiguration { get; set; }
}