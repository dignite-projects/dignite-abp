using Dignite.Cms.Fields;
using System;

namespace Dignite.Cms.Sections
{
    public class EntryFieldDto
    {
        public Guid FieldId { get; set; }

        public FieldDto Field { get; set; }

        /// <summary>
        /// Text to override field definition
        /// </summary>
        public string DisplayName { get; set; }
        public virtual bool Required { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool ShowOnList { get; set; }
    }
}
