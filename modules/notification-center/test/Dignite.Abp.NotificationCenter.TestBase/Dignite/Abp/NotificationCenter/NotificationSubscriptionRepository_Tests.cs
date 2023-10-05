using System.Threading.Tasks;
using Shouldly;
using Volo.Abp.Modularity;
using Xunit;

namespace Dignite.Abp.NotificationCenter;

public abstract class NotificationSubscriptionRepository_Tests<TStartupModule> : NotificationCenterTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{
    private readonly NotificationCenterTestData _notificationCenterTestData;
    private readonly INotificationSubscriptionRepository _notificationSubscriptionRepository;

    public NotificationSubscriptionRepository_Tests()
    {
        _notificationCenterTestData = GetRequiredService<NotificationCenterTestData>();
        _notificationSubscriptionRepository = GetRequiredService<INotificationSubscriptionRepository>();
    }


    [Fact]
    public async Task GetListForUserAsync()
    {
        var sdfsdf = await _notificationSubscriptionRepository.GetListAsync();

        var notifications = await _notificationSubscriptionRepository.GetListAsync(
            _notificationCenterTestData.User1Id
            );

        notifications.Count.ShouldBe(1);
    }
}