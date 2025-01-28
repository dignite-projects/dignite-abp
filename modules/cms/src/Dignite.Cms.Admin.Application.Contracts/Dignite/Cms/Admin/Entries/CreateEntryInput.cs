using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Dignite.Cms.Admin.Entries
{
    public class CreateEntryInput : CreateOrUpdateEntryInputBase
    {
        public CreateEntryInput():base()
        {
        }


        public Guid? InitialVersionId { get; set; }

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            return base.Validate(validationContext);
        }
    }
}
