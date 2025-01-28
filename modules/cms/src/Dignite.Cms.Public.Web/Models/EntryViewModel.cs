using Dignite.Cms.Public.Entries;
using Dignite.Cms.Public.Sections;

namespace Dignite.Cms.Public.Web.Models
{
    public class EntryViewModel
    {
        public EntryViewModel(EntryDto entry, SectionDto section)
        {
            Entry = entry;
            Section = section;
        }

        public EntryDto Entry { get;  }
        public SectionDto Section { get; }
    }
}
