using System;
using System.ComponentModel.DataAnnotations;

namespace Dignite.Cms.Admin.Entries
{
    public class MoveEntryInput
    {
        public Guid? ParentId { get; set; }

        [Required]
        public int Order { get; set; }
    }
}
