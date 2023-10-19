using Dignite.Abp.UserPoints.Localization;
using Volo.Abp.Application.Services;

namespace Dignite.Abp.UserPoints;

public abstract class UserPointsAppService : ApplicationService
{
    protected UserPointsAppService()
    {
        LocalizationResource = typeof(UserPointsResource);
        ObjectMapperContext = typeof(UserPointsApplicationModule);
    }
}
