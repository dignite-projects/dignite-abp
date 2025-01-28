using JetBrains.Annotations;
using System;
using Volo.Abp;

namespace Dignite.Cms.Sections
{
    [Serializable]
    public class EntryTypeNameAlreadyExistException : BusinessException
    {
        public EntryTypeNameAlreadyExistException([NotNull]string name)
        {
            Code = CmsErrorCodes.Sections.NameAlreadyExist;
            WithData(nameof(EntryType.Name), name);
        }
    }
}
