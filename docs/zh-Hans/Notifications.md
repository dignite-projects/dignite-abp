# Dignite Abp 通知系统核心

Dignite Abp 通知系统是基于[Asp.Net Boilerplate 通知系统](https://aspnetboilerplate.com/Pages/Documents/Notification-System)开发的，它使用[分布式事件总线](https://docs.abp.io/en/abp/latest/Distributed-Event-Bus)来发布和订阅通知。

## 安装

要开始使用Dignite Abp 通知系统，首先需要执行以下步骤：

1. 在您的 Domain 项目中安装 `Dignite.Abp.Notifications` NuGet 包。
2. 在您的模块类的 `[DependsOn(...)]` 属性列表中添加 `DigniteAbpNotificationsModule`。

如果您已经安装了[通知中心模块](Notification-Center.md)，则无需单独安装 `Dignite.Abp.Notifications` 模块。

## 订阅通知

Dignite Abp 通知系统支持两种方式向用户发送通知：

1. 发送给指定用户。
2. 发送给已订阅通知的用户。

### 订阅通知示例

使用 `INotificationSubscriptionManager` 接口来实现订阅通知的功能，以下是一些示例代码：

```csharp
public class SubscribeService : MyModuleAppService, ISubscribeService
{
    private readonly INotificationSubscriptionManager _notificationSubscriptionManager;

    public SubscribeService(INotificationSubscriptionManager notificationSubscriptionManager)
    {
        _notificationSubscriptionManager = notificationSubscriptionManager;
    }

    /// <summary>
    /// 当发送添加好友的请求时，向用户发送请求通知
    /// </summary>
    [Authorize]
    public async Task Subscribe_SentFriendshipRequest()
    {
        await _notificationSubscriptionManager.SubscribeAsync(CurrentUser.Id.Value, "SentFriendshipRequest");
    }

    /// <summary>
    /// 订阅一个和特定实体（PhotoEntity）关联的通知。用户对该实体发表了评论，向订阅用户发送通知。
    /// </summary>
    [Authorize]
    public async Task Subscribe_CommentPhoto(Guid photoId)
    {
        await _notificationSubscriptionManager.SubscribeAsync(CurrentUser.Id.Value, "CommentPhoto", new NotificationEntityIdentifier(typeof(PhotoEntity), photoId.ToString()));
    }
}
```

### 订阅管理 API

`INotificationSubscriptionManager` 接口提供了一系列方法来管理订阅通知的操作，包括：

- 订阅通知：

  ```csharp
  Task SubscribeAsync([NotNull] Guid userId, [NotNull] string notificationName, [CanBeNull] NotificationEntityIdentifier entityIdentifier = null)
  ```

  - userId：订阅通知的用户ID。
  - notificationName：订阅通知的名称。
  - entityIdentifier：关联特定的实体。
    - Type：特定实体的类型。
    - Id：特定实体的编号。

- 取消订阅通知：

  ```csharp
  Task UnsubscribeAsync([NotNull] Guid userId, [NotNull] string notificationName, [CanBeNull] NotificationEntityIdentifier entityIdentifier = null)
  ```

- 订阅所有可用的通知：

  ```csharp
  Task SubscribeToAllAvailableNotificationsAsync([NotNull] Guid userId)
  ```

- 获取通知的所有订阅者：

  ```csharp
  Task<List<NotificationSubscriptionInfo>> GetSubscriptionsAsync([NotNull] string notificationName, [CanBeNull] NotificationEntityIdentifier entityIdentifier = null)
  ```

- 获取用户的所有通知订阅：

  ```csharp
  Task<List<NotificationSubscriptionInfo>> GetSubscribedNotificationsAsync([NotNull] Guid userId);
  ```

- 判断用户是否订阅某类通知：

  ```csharp
  Task<bool> IsSubscribedAsync([NotNull] Guid userId, [NotNull] string notificationName, [CanBeNull] NotificationEntityIdentifier entityIdentifier = null);
  ```

## 发布通知

使用 `INotificationPublisher` 接口来发布通知：

- 使用内置的 `MessageNotificationData` 向指定用户发布通知：

  ```csharp
  public class PublishService : MyModuleAppService, IPublishService
  {
      private readonly INotificationPublisher _notificationPublisher;

      public PublishService(INotificationPublisher notificationPublisher)
      {
          _notificationPublisher = notificationPublisher;
      }
      
      public async Task Publish_Message(string message, Guid targetUserId)
      {
          await _notificationPublisher.PublishAsync("Message", new MessageNotificationData(message), userIds: new[] { targetUserId });
      }
  }
  ```

- 使用内置的 `LocalizableMessageNotificationData` 向所有订阅用户发布通知：

  ```csharp
  public class PublishService : MyModuleAppService, IPublishService
  {
      private readonly INotificationPublisher _notificationPublisher;

      public PublishService(INotificationPublisher notificationPublisher)
      {
          _notificationPublisher = notificationPublisher;
      }

      
      public async Task Publish_LowDisk(int remainingDiskInMb)
      {
          var data = new LocalizableMessageNotificationData("LowDiskWarningMessage", "MyLocalizationSourceName");
          data["remainingDiskInMb"] = remainingDiskInMb;

          await _notificationPublisher.PublishAsync("System.LowDisk", data, severity: NotificationSeverity.Warn);
      }
  }
  ```

- 向图片所有者发布图片评论的通知：

  1. 创建一个继承自 `NotificationData` 的通知数据类：

      ```csharp
      [Serializable]
      public class CommentPhotoNotificationData : NotificationData
      {
          public string CommenterUserName { get; set; }

          public string Comment { get; set; }

          public CommentPhotoNotificationData(string commenterUserName, string comment)
          {
              CommenterUserName = commenterUserName;
              Comment = comment;
          }
      }
      ```

  2. 发布图片评论通知：

      ```csharp
      public class PublishService : MyModuleAppService, IPublishService
      {
          private readonly INotificationPublisher _notificationPublisher;

          public PublishService(INotificationPublisher notificationPublisher)
          {
              _notificationPublisher = notificationPublisher;
          }

          public async Task Publish_CommentPhoto(string commenterUserName, string comment, Guid photoId, Guid photoOwnerUserId)
          {
              await _notificationPublisher.PublishAsync("CommentPhoto", new CommentPhotoNotificationData(commenterUserName, comment), new NotificationEntityIdentifier(typeof(PhotoEntity), photoId.ToString()), userIds: new[] { photoOwnerUserId });
          }
      }
      ```

## 通知用户

通知系统使用 [分布式事件总线](https://docs.abp.io/en/abp/latest/Distributed-Event-Bus) 来发送通知事件，可以使用 `IDistributedEventHandler<RealTimeNotifyEto>` 来订

阅通知事件。通知系统还支持基于 [SignalR](https://learn.microsoft.com/en-us/aspnet/core/signalr/introduction) 的实时通知功能。

### 安装 SignalRNotifier

要使用 SignalR 实现实时通知功能，请执行以下步骤：

1. 在服务端项目中安装 `Dignite.Abp.Notifications.SignalRNotifier` NuGet 包。
2. 在您的模块类的 `[DependsOn(...)]` 属性列表中添加 `DigniteAbpNotificationsSignalRNotifierModule`。

详细信息可以参考[Blazor 版通知系统](Blazor-Notification-Center.md)。

## 管理用户的通知

`IUserNotificationManager` 接口提供了一些方法来管理用户通知：

- 获取用户的通知：

  ```csharp
  Task<List<UserNotificationWithNotification>> GetUserNotificationsAsync(Guid userId, UserNotificationState? state = null, int skipCount = 0, int maxResultCount = int.MaxValue, DateTime? startDate = null, DateTime? endDate = null);
  ```

- 获取用户通知的总数量：

  ```csharp
  Task<int> GetUserNotificationCountAsync(Guid userId, UserNotificationState? state = null, DateTime? startDate = null, DateTime? endDate = null);
  ```

- 更新用户通知的状态：

  ```csharp
  Task UpdateUserNotificationStateAsync(Guid userId, Guid notificationId, UserNotificationState state);
  ```

- 更新用户所有通知的状态：

  ```csharp
  Task UpdateAllUserNotificationStatesAsync(Guid userId, UserNotificationState state);
  ```

- 删除用户的某条通知：

  ```csharp
  Task DeleteUserNotificationAsync(Guid userId, Guid notificationId);
  ```

- 删除用户的部分通知：

  ```csharp
  Task DeleteAllUserNotificationsAsync(Guid userId, UserNotificationState? state = null, DateTime? startDate = null, DateTime? endDate = null);
  ```

## 定义通知

定义通知可以提供一些额外选项，以便开发者创建个性化的通知系统。通知的定义通常包括以下信息：

- Name：通知的唯一名称。
- EntityType（可选）：与通知相关的实体类型。
- DisplayName（可选）：用于在UI中显示的通知名称。
- Description（可选）：通知的友好说明。
- PermissionName（可选）：与通知相关的权限名称。
- FeatureName（可选）：与通知相关的功能名称。
- Attributes（可选）：与通知相关的任意对象，以字典方式存储。

### 通知定义的示例

以下是一个示例通知定义提供者：

```csharp
public class NotificationCenterSampleNotificationDefinitionProvider : NotificationDefinitionProvider
{
    public override void Define(INotificationDefinitionContext context)
    {
        context.Add(new NotificationDefinition(NotificationCenterSampleNotifications.TestNotification, null, L("TestNotification"), L("TestNotificationDescription")));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<NotificationCenterSampleResource>(name);
    }
}
```

### 通知定义的管理

使用 `INotificationDefinitionManager` 接口来管理通知定义：

- 根据名称获取通知定义：

  ```csharp
  NotificationDefinition Get(string name);
  ```

- 获取一个可以为 `null` 的通知定义：

  ```csharp
  NotificationDefinition GetOrNull(string name);
  ```

- 获取所有的通知定义：

  ```csharp
  IReadOnlyList<NotificationDefinition> GetAll();
  ```

- 判断指定名称的通知定义对指定的用户是否可用：

  ```csharp
  Task<bool> IsAvailableAsync(string name, Guid userId);
  ```

- 获取指定用户所有可用的通知定义：

  ```csharp
  Task<IReadOnlyList<NotificationDefinition>> GetAllAvailableAsync(Guid userId);
  ```

> 注意：在判断用户是否有权限使用某个通知定义时，通常需要结合[Dignite.Abp.Notifications.Identity](https://github.com/dignite-projects/dignite-abp/tree/main/modules/notification-center/src/Dignite.Abp.Notifications.Identity) 模块一起使用。

## 通知的存储

通知系统提供了 `INotificationStore` 接口来实现通知、用户订阅数据的持久化。您可以根据需要自行实现该接口，或者使用已经有完整实现的 [通知中心模块](Notification-Center.md)。

## 后续阅读

- 通知中心模块

  [通知中心模块](Notification-Center.md)是一个完整的通知系统，可以安装到您的应用程序中使用。

- Blazor 版通知系统

  [Blazor 版通知系统](Blazor-Notification-Center.md)适用于 Asp.Net Blazor 应用程序。
