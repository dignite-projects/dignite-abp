using System.Collections.Generic;

namespace Dignite.Cms.Sections
{
    public class EntryFieldTab
    {
        public EntryFieldTab(string name, ICollection<EntryField> fields)
        {
            Name = name;
            Fields = fields;
        }

        public string Name { get; protected set; }

        public ICollection<EntryField> Fields { get; protected set; }
    }
}
