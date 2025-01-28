using System;
using System.ComponentModel.DataAnnotations;

namespace Dignite.Cms.Admin.Sections
{
    public class EntryTypeNameExistsInput
    {
        public EntryTypeNameExistsInput()
        {
        }

        public EntryTypeNameExistsInput(Guid sectionId, string name)
        {
            SectionId = sectionId;
            Name = name;
        }

        [Required]
        public Guid SectionId { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
