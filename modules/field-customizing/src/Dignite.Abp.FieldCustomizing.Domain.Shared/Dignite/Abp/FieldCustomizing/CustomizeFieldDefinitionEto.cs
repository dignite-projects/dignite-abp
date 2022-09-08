using System;
using Dignite.Abp.FieldCustomizing.Fields;
using Volo.Abp.EventBus;

namespace Dignite.Abp.FieldCustomizing;

[EventName("Dignite.Abp.FieldCustomizing.FieldDefinition")]
public class CustomizeFieldDefinitionEto : ICustomizeFieldDefinition
{
    public Guid Id { get; set; }

    public Guid? TenantId { get; set; }


    public string Name { get; set; }

    public string DisplayName { get; set; }


    public string DefaultValue { get; set; }

    public string FieldProviderName { get; set; }

    public FieldConfigurationDictionary Configuration { get; set; }
}
