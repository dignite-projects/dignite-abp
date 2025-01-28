using JetBrains.Annotations;
using System;
using Volo.Abp;

namespace Dignite.Cms.Sections
{
    [Serializable]
    public class DefaultSectionCannotSetNotActiveException : BusinessException
    {
        public DefaultSectionCannotSetNotActiveException([NotNull]string displayName)
        {
            Code = CmsErrorCodes.Sections.DefaultSectionCannotSetNotActive;
            WithData(nameof(Section.DisplayName), displayName);
        }
    }
}
