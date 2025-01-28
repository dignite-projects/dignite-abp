using JetBrains.Annotations;
using System;
using Volo.Abp;

namespace Dignite.Cms.Sections
{
    [Serializable]
    public class SectionNameAlreadyExistException : BusinessException
    {
        public SectionNameAlreadyExistException([NotNull]string name)
        {
            Code = CmsErrorCodes.Sections.NameAlreadyExist;
            WithData(nameof(Section.Name), name);
        }
    }
}
