using System.Text.Json;
using Shouldly;
using Xunit;

namespace Dignite.Abp.Notifications;

public class NotificationData_Serialization_Tests : NotificationsTestBase
{
    [Fact]
    public void Should_Deserialize_And_Serialize_MessageNotificationData()
    {
        var data = JsonSerializer.Deserialize(
                JsonSerializer.Serialize(new MessageNotificationData("Hello World!")),
                typeof(MessageNotificationData)
            ) as MessageNotificationData;

        Assert.NotNull(data);
        data.Message.ShouldBe("Hello World!");
    }

    [Fact]
    public void MessageNotificationData_Backward_Compatibility_Test()
    {
        const string serialized = "{\"Message\":\"a test message\",\"Type\":\"Abp.Notifications.MessageNotificationData\",\"Properties\":{}}";

        var data = JsonSerializer
            .Deserialize(
                serialized,
                typeof(MessageNotificationData)
            ) as MessageNotificationData;

        Assert.NotNull(data);
        data.Message.ShouldBe("a test message");
        data.Properties["Message"].ShouldBe("a test message");
    }
}