

namespace Dignite.Cms.Sections
{
    public static class SectionConsts
    {
        /// <summary>
        /// Maximum length of the section display name property.
        /// Default value: 64
        /// </summary>
        public static int MaxDisplayNameLength { get; set; } = 64;

        /// <summary>
        /// Maximum length of the section name property.
        /// Default value: 64
        /// </summary>
        public static int MaxNameLength { get; set; } = 64;

        /// <summary>
        /// Regular Expression of the section name property.
        /// </summary>
        public const string NameRegularExpression = "^[a-zA-Z0-9_\\-\\.]+$";

        /// <summary>
        /// Maximum length of the section page route property.
        /// Default value: 256
        /// </summary>dddd
        public static int MaxPageRouteLength { get; set; } = 256;

        /// <summary>
        /// Regular Expression of the page route property.
        /// </summary>dddd
        public const string PageRouteRegularExpression = "^[a-zA-Z0-9_\\-.\\{\\}:\\\\/\\\\\\\\]+$";

        /// <summary>
        /// Maximum length of the section page template property.
        /// Default value: 256
        /// </summary>dddd
        public static int MaxPagetemplateLength { get; set; } = 256;

        /// <summary>
        /// Regular Expression of the template path property.
        /// </summary>
        public const string PageTemplateRegularExpression = "^[a-zA-Z0-9_\\-.\\\\/\\\\\\\\]+$";
    }
}
