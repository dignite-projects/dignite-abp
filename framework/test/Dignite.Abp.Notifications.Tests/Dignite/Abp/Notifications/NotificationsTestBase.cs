using Volo.Abp;
using Volo.Abp.Testing;

namespace Dignite.Abp.Notifications;

public class NotificationsTestBase : AbpIntegratedTest<AbpNotificationsTestModule>
{
    protected override void SetAbpApplicationCreationOptions(AbpApplicationCreationOptions options)
    {
        options.UseAutofac();
    }

}
