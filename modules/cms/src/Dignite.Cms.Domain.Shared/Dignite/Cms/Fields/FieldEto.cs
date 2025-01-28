using System;
using Dignite.Abp.DynamicForms;
using Volo.Abp.EventBus;

namespace Dignite.Cms.Fields;

[EventName("Dignite.Cms.Fields.FieldEto")]
public class FieldEto
{
    public Guid Id { get; set; }

    public Guid? TenantId { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public Guid? GroupId { get; set; }

    /// <summary>
    /// Field Unique Name
    /// </summary>
    public string Name { get; set; }


    /// <summary>
    /// 
    /// </summary>
    public string DisplayName { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Field <see cref="IFormControl.Name"/>
    /// </summary>
    public string FormControlName { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public FormConfigurationDictionary FormConfiguration { get; set; }
}