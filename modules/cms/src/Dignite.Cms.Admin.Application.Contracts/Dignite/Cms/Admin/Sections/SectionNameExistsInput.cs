using System.ComponentModel.DataAnnotations;

namespace Dignite.Cms.Admin.Sections
{
    public class SectionNameExistsInput
    {
        public SectionNameExistsInput()
        {
        }

        public SectionNameExistsInput(string name)
        {
            Name = name;
        }
        
        [Required]
        public string Name { get; set; }
    }
}
