using Dignite.Abp.FieldCustomizing;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Dignite.Abp.SettingManagement
{
    public abstract class UpdateSettingsInput : CustomizableObject
    {
        [Required]
        public string NavigationName { get; set; }


        public override IReadOnlyList<BasicCustomizeFieldDefinition> GetFieldDefinitions(ValidationContext validationContext)
        {
            return 
                GetSettings(validationContext)
                .Select(fd => new BasicCustomizeFieldDefinition(
                        fd.Name,
                        fd.DisplayName,
                        fd.FieldProviderName,
                        fd.Value,
                        fd.FieldConfiguration
                        )).ToList();
        }

        public abstract IReadOnlyList<SettingDto> GetSettings(ValidationContext validationContext);
    }
}
