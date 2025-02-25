using System;

namespace Dignite.Cms.Sections
{
    public class EntryField
    {
        public EntryField(Guid fieldId, string displayName, bool required,  bool showInList, bool enableSearch)
        {
            FieldId = fieldId;
            DisplayName = displayName;
            Required = required;
            ShowInList = showInList;
            EnableSearch = enableSearch;
        }

        public Guid FieldId { get; protected set; }


        /// <summary>
        /// Text to override field definition
        /// </summary>
        public virtual string DisplayName { get; protected set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual bool Required { get; protected set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual bool ShowInList { get; protected set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual bool EnableSearch { get; protected set; }
    }
}
