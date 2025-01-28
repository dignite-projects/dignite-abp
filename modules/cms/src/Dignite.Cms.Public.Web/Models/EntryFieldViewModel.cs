using Dignite.Abp.Data;
using Dignite.Abp.DynamicForms;
using Dignite.Cms.Fields;

namespace Dignite.Cms.Public.Web.Models
{
    public class EntryFieldViewModel
    {
        public EntryFieldViewModel(FormField field, IHasCustomFields entry)
        {
            Field = field;
            Entry = entry;
        }

        public FormField Field { get; set; }

        public IHasCustomFields Entry { get; set; }
    }
}
