using System.Threading.Tasks;
using Xunit;
using Shouldly;

namespace Dignite.Abp.NotificationCenter;

public class NotificationAppService_Tests : NotificationCenterApplicationTestBase
{
    private readonly INotificationAppService _notificationAppService;

    public NotificationAppService_Tests()
    {
        _notificationAppService = GetRequiredService<INotificationAppService>();
    }

    [Fact]
    public async Task Should_Subscribed_NotificationsAsync()
    {
        var result = await _notificationAppService.GetSubscribedAsync();
        result.Items.ShouldNotBeEmpty();
    }
}