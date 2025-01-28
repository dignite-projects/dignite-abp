namespace Dignite.Cms.Fields
{
    public static class FieldConsts
    {
        /// <summary>
        /// Default value: 64
        /// </summary>
        public static int MaxNameLength { get; set; } = 64;

        /// <summary>
        /// </summary>
        public const string NameRegularExpression = "^[a-zA-Z0-9_\\-\\.]+$";

        /// <summary>
        /// Default value: 128
        /// </summary>
        public static int MaxDisplayNameLength { get; set; } = 128;

        /// <summary>
        /// Default value: 64
        /// </summary>
        public static int MaxFormControlNameLength { get; set; } = 64;

        /// <summary>
        /// Default value: 64
        /// </summary>
        public static int MaxDescriptionLength { get; set; } = 256;
    }
}
