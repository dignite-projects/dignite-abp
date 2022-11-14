namespace Dignite.Abp.FieldCustomizing;

public class CustomizeField
{
    public CustomizeField(IHasCustomFields entity, ICustomizeFieldDefinition definition)
    {
        Entity = entity;
        Definition = definition;
    }

    public IHasCustomFields Entity { get; set; }

    public ICustomizeFieldDefinition Definition { get; set; }
}