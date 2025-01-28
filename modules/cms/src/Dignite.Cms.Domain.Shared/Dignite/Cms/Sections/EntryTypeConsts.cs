

namespace Dignite.Cms.Sections
{
    public static class EntryTypeConsts
    {
        /// <summary>
        /// Maximum length of the entry type display name property.
        /// Default value: 64
        /// </summary>
        public static int MaxDisplayNameLength { get; set; } = 64;

        /// <summary>
        /// Maximum length of the entry type name property.
        /// Default value: 64
        /// </summary>
        public static int MaxNameLength { get; set; } = 64;

        /// <summary>
        /// Regular Expression of the entry type name property.
        /// </summary>
        public const string NameRegularExpression = "^[a-zA-Z0-9_\\-\\.]+$";
    }
}
