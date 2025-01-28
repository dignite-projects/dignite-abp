using Dignite.Cms.Fields;
using JetBrains.Annotations;
using System;
using System.Linq;

namespace Dignite.Cms.Public.Sections
{
    public static class SectionDtoExtensions
    {
        public static FieldDto GetField([NotNull] this SectionDto source, Guid entryTypeId, [NotNull] string name)
        {
            var fields = source.EntryTypes.Single(et => et.Id == entryTypeId).FieldTabs.SelectMany(ft => ft.Fields).Select(f => f.Field);
            return fields.FirstOrDefault(f => f.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }
    }
}
