using JetBrains.Annotations;
using System;
using Volo.Abp;

namespace Dignite.Cms.Entries
{
    [Serializable]
    public class EntryCultureAlreadyExistException : BusinessException
    {
        public EntryCultureAlreadyExistException([NotNull] string culture, [NotNull] Guid entryTypeId)
        {
            Code = CmsErrorCodes.Entries.CultureAlreadyExist;
            WithData(nameof(Entry.Culture), culture);
            WithData(nameof(Entry.EntryTypeId), entryTypeId);
        }
    }
}
