using Dignite.Abp.UserPoints.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Dignite.Abp.UserPoints;

public abstract class UserPointsController : AbpControllerBase
{
    protected UserPointsController()
    {
        LocalizationResource = typeof(UserPointsResource);
    }
}
