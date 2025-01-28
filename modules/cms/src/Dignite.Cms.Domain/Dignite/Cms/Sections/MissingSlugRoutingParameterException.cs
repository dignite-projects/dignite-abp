using JetBrains.Annotations;
using System;
using Volo.Abp;

namespace Dignite.Cms.Sections
{
    [Serializable]
    public class MissingSlugRoutingParameterException : BusinessException
    {
        public MissingSlugRoutingParameterException([NotNull] SectionType type,[NotNull]string route)
        {
            Code = CmsErrorCodes.Sections.RouteMissingSlugRoutingParameter;
            WithData(nameof(Section.Type), type);
            WithData(nameof(Section.Route), route);
        }
    }
}
