

namespace Dignite.Abp.FieldCustomizing
{
    public class CustomizeField
    {

        public CustomizeField(IHasCustomizableFields entity, ICustomizeFieldDefinition definition)
        {
            Entity = entity;
            Definition = definition;
        }

        public IHasCustomizableFields Entity { get; set; }

        public ICustomizeFieldDefinition Definition { get; set; }
    }
}
