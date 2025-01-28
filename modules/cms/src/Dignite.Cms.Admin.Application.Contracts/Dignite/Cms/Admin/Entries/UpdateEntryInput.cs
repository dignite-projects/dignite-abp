namespace Dignite.Cms.Admin.Entries
{
    public class UpdateEntryInput : CreateOrUpdateEntryInputBase
    {
        public string ConcurrencyStamp { get; set; }
    }
}
