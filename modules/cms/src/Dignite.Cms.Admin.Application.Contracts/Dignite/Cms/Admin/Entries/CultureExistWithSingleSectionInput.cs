using System;
using System.ComponentModel.DataAnnotations;

namespace Dignite.Cms.Admin.Entries
{
    public class CultureExistWithSingleSectionInput
    {
        public CultureExistWithSingleSectionInput()
        {
        }

        public CultureExistWithSingleSectionInput(string culture, Guid sectionId, Guid entryTypeId)
        {
            Culture = culture;
            SectionId = sectionId;
            EntryTypeId = entryTypeId;
        }

        [Required]
        public string Culture { get; set; }

        [Required]
        public Guid SectionId { get; set; }

        [Required]
        public Guid EntryTypeId { get; set; }
    }
}
