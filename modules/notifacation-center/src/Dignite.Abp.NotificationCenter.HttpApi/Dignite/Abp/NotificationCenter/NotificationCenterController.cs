using Dignite.Abp.NotificationCenter.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Dignite.Abp.NotificationCenter
{
    public abstract class NotificationCenterController : AbpControllerBase
    {
        protected NotificationCenterController()
        {
            LocalizationResource = typeof(NotificationCenterResource);
        }
    }
}
