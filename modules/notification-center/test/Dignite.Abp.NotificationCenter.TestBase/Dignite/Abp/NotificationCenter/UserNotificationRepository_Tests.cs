using System.Linq;
using System.Threading.Tasks;
using Dignite.Abp.Notifications;
using Shouldly;
using Volo.Abp.Modularity;
using Xunit;

namespace Dignite.Abp.NotificationCenter;
public abstract class UserNotificationRepository_Tests<TStartupModule> : NotificationCenterTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{
    private readonly NotificationCenterTestData _notificationCenterTestData;
    private readonly IUserNotificationRepository _userNotificationRepository;

    public UserNotificationRepository_Tests()
    {
        _notificationCenterTestData = GetRequiredService<NotificationCenterTestData>();
        _userNotificationRepository = GetRequiredService<IUserNotificationRepository>();
    }


    [Fact]
    public async Task GetListForUserAsync()
    {
        var notifications = await _userNotificationRepository.GetListAsync(
            _notificationCenterTestData.User1Id
            );

        notifications.Count.ShouldBe(1);
        notifications.Count(x => x.State == UserNotificationState.Unread).ShouldBe(1);
        notifications.Count(x => x.State == UserNotificationState.Read).ShouldBe(0);
    }
}
