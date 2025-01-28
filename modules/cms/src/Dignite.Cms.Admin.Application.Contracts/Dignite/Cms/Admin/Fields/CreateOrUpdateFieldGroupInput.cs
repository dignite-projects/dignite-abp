using Dignite.Cms.Fields;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Validation;

namespace Dignite.Cms.Admin.Fields
{
    public class CreateOrUpdateFieldGroupInput
    {
        [Required]
        [DynamicMaxLength(typeof(FieldGroupConsts), nameof(FieldGroupConsts.MaxNameLength))]
        public string Name { get; set; }
    }
}
