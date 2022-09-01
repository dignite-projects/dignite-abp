using Dignite.Abp.NotificationCenter.Localization;
using Volo.Abp.Application.Services;

namespace Dignite.Abp.NotificationCenter
{
    public abstract class NotificationCenterAppService : ApplicationService
    {
        protected NotificationCenterAppService()
        {
            LocalizationResource = typeof(NotificationCenterResource);
            ObjectMapperContext = typeof(NotificationCenterApplicationModule);
        }
    }
}
