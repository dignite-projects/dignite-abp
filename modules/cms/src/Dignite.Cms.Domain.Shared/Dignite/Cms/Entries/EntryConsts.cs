
namespace Dignite.Cms.Entries
{
    public static class EntryConsts
    {
        /// <summary>
        /// Maximum length of the Entry Culture property.
        /// Default value: 16
        /// </summary>
        public static int MaxCultureLength { get; set; } = 16;

        /// <summary>
        /// Maximum length of the entry name property.
        /// Default value: 128
        /// </summary>
        public static int MaxSlugLength { get; set; } = 128;


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
        /// Default value: default
        /// </summary>
        public static string DefaultSlug { get; set; } = "default";
    }
}
