using Dignite.Cms.Sections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Validation;

namespace Dignite.Cms.Admin.Sections
{
    public class EntryFieldTabInput
    {
        public EntryFieldTabInput()
        {
        }

        public EntryFieldTabInput(string name)
        {
            Name = name;
            Fields = new List<EntryFieldInput>();
        }

        [Required]
        [DynamicMaxLength(typeof(EntryTypeConsts), nameof(EntryTypeConsts.MaxDisplayNameLength))]
        public string Name { get; set; }

        [Required]
        public IList<EntryFieldInput> Fields { get; set; }
    }
}
