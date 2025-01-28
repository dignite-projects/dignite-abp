namespace Dignite.Cms.Sections
{
    public static class SectionTypeExtensions
    {
        public static string ToLocalizationKey(this SectionType sectionType)
        {
            return $"Enum:SectionType:{sectionType}";
        }
    }
}
