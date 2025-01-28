using JetBrains.Annotations;
using System;
using Volo.Abp;

namespace Dignite.Cms.Sections
{
    [Serializable]
    public class SectionRouteAlreadyExistException : BusinessException
    {
        public SectionRouteAlreadyExistException([NotNull]string route)
        {
            Code = CmsErrorCodes.Sections.RouteAlreadyExist;
            WithData(nameof(Section.Route), route);
        }
    }
}
