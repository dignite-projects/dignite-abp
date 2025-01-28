namespace Dignite.Cms.Entries
{
    public static class EntryStatusExtensions
    {
        public static string ToLocalizationKey(this EntryStatus status)
        {
            return $"Enum:EntryStatus:{status}";
        }
    }
}
