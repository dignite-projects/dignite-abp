using System.Threading.Tasks;
using Volo.Abp.Users;
using Xunit;
using Shouldly;

namespace Dignite.Abp.NotificationCenter;

public class NotificationAppService_Tests : NotificationCenterApplicationTestBase
{
    private readonly INotificationAppService _notificationAppService;
    private readonly ICurrentUser _currentUser;

    public NotificationAppService_Tests()
    {
        _notificationAppService = GetRequiredService<INotificationAppService>();
        _currentUser = GetRequiredService<ICurrentUser>();
    }

    [Fact]
    public async Task Should_Subscribed_NotificationsAsync()
    {
        var user = _currentUser;
        var result = await _notificationAppService.GetSubscribedAsync();
        result.Items.ShouldNotBeEmpty();
    }
}