using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Features;

namespace Dignite.Abp.Notifications;

public class FakeNotificationDefinitionManager : NotificationDefinitionManager, ISingletonDependency
{
    public FakeNotificationDefinitionManager(
        IOptions<NotificationOptions> options,
        IServiceProvider serviceProvider,
        IFeatureChecker featureChecker
    ) : base(
            options,
            serviceProvider,
            featureChecker
            )
    {
    }

    protected override Task<bool> PermissionCheckAsync(NotificationDefinition notificationDefinition, Guid userId)
    {
        return Task.FromResult( true);
    }
}

