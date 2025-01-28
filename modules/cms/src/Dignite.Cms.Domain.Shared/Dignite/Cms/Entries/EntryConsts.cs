
namespace Dignite.Cms.Entries
{
    public static class EntryConsts
    {
        /// <summary>
        /// Maximum length of the section Culture property.
        /// Default value: 16
        /// </summary>
        public static int MaxLanguageCultureNameLength { get; set; } = 16;

        /// <summary>
        /// Maximum length of the section title property.
        /// Default value: 256
        /// </summary>
        public static int MaxTitleLength { get; set; } = 256;

        /// <summary>
        /// Maximum length of the entry name property.
        /// Default value: 256
        /// </summary>
        public static int MaxSlugLength { get; set; } = 256;


        /// <summary>
        /// Regular Expression of the Name property.
        /// </summary>
        public const string SlugRegularExpression = "^[a-zA-Z0-9_\\-\\.]+$";

        /// <summary>
        /// Maximum length of the entry revision notes property.
        /// Default value: 512
        /// </summary>
        public static int MaxRevisionNotesLength { get; set; } = 512;

        /// <summary>
        /// Default value: index
        /// </summary>
        public static string DefaultSlug { get; set; } = "index";
    }
}
