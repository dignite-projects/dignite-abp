using System.Collections.Generic;

namespace Dignite.Cms.Sections
{
    public class EntryFieldTabDto
    {
        public string Name { get; set; }

        public ICollection<EntryFieldDto> Fields { get; set; }
    }
}
