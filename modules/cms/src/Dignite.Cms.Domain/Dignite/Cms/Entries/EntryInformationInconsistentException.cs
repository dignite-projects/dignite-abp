using JetBrains.Annotations;
using System;
using Volo.Abp;

namespace Dignite.Cms.Entries
{
    [Serializable]
    public class EntryInformationInconsistentException : BusinessException
    {
        public EntryInformationInconsistentException([NotNull] string culture, [NotNull] Guid entryTypeId,Guid? parentId)
        {
            Code = CmsErrorCodes.Entries.InformationInconsistent;
            WithData(nameof(Entry.Culture), culture);
            WithData(nameof(Entry.EntryTypeId), entryTypeId);
            WithData(nameof(Entry.ParentId), parentId);
        }
    }
}
