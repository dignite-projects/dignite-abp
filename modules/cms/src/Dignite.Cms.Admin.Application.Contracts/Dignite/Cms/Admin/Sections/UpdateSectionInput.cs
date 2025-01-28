
using Volo.Abp.Domain.Entities;

namespace Dignite.Cms.Admin.Sections
{
    public class UpdateSectionInput : CreateOrUpdateSectionInputBase, IHasConcurrencyStamp
    {
        public UpdateSectionInput() : base()
        {
        }
        public string ConcurrencyStamp { get; set; }
    }
}
