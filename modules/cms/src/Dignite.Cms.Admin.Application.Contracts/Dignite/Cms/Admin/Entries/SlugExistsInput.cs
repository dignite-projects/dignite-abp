using System;
using System.ComponentModel.DataAnnotations;

namespace Dignite.Cms.Admin.Entries
{
    public class SlugExistsInput
    {
        public SlugExistsInput()
        {
        }

        public SlugExistsInput(string culture, Guid sectionId, string slug)
        {
            Culture = culture;
            SectionId = sectionId;
            Slug = slug;
        }

        [Required]
        public string Culture { get; set; }
        [Required]
        public Guid SectionId { get; set; }
        public string Slug { get; set; }
    }
}
