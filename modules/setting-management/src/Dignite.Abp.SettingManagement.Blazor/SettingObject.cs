using Dignite.Abp.FieldCustomizing;

namespace Dignite.Abp.SettingManagement.Blazor
{
    public class SettingObject: IHasCustomizableFields
    {
        public SettingObject()
        {
            this.CustomizedFields = new CustomizeFieldDictionary();
        }

        public CustomizeFieldDictionary CustomizedFields { get; }
    }
}
