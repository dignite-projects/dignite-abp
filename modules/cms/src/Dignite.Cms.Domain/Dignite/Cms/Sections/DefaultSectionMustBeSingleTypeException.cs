using JetBrains.Annotations;
using System;
using Volo.Abp;

namespace Dignite.Cms.Sections
{
    [Serializable]
    public class DefaultSectionMustBeSingleTypeException : BusinessException
    {
        public DefaultSectionMustBeSingleTypeException([NotNull]string name)
        {
            Code = CmsErrorCodes.Sections.DefaultSectionMustBeSingleType;
            WithData(nameof(Section.Name), name);
        }
    }
}
