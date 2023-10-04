# 通知系统核心

Dignite Abp 通知系统是参考[Asp.Net Boilerplate 通知系统](https://aspnetboilerplate.com/Pages/Documents/Notification-System)开发，基于[分布式事件总线](https://docs.abp.io/en/abp/latest/Distributed-Event-Bus)发布和订阅通知。

## 安装

- 将 `Dignite.Abp.Notifications` NuGet 包安装到 Domain 项目中。
- 添加 `DigniteAbpNotificationsModule` 到 [模块类](https://docs.abp.io/en/abp/latest/Module-Development-Basics) `[DependsOn(...)]`属性列表中.

> 如果您已安装了[通知中心模块](Notification-Center.md)，那么 `Dignite.Abp.Notifications` 模块已安装。

## 订阅通知

向用户发送通知有两种方式，一种是发送给指定用户，另外一种是发送给订阅用户，用户订阅特定通知，当系统发布这种通知时，系统会通知给所有已经订阅的用户。

### 订阅通知示例

`INotificationSubscriptionManager` 提供了订阅通知的方法，示例：

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
    public async Task Subscribe_SentFrendshipRequest()
    {
        await _notificationSubscriptionManager.SubscribeAsync(CurrentUser.Id.Value, "SentFrendshipRequest");
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

### API

`INotificationSubscriptionManager` 接口还提供了一系列方法管理订阅：

- 订阅通知

  ```csharp
  Task SubscribeAsync([NotNull] Guid userId, [NotNull] string notificationName, [CanBeNull] NotificationEntityIdentifier entityIdentifier = null)
  ```

  - userId：订阅通知的用户ID。
  - notificationName：订阅通知的名称。
  - entityIdentifier：关联特定的实体。
    - Type：特定的实体的类型
    - Id：特定实体的编号

- 取消订阅

  ```csharp
  UnsubscribeAsync([NotNull] Guid userId, [NotNull] string notificationName, [CanBeNull] NotificationEntityIdentifier entityIdentifier = null)
  ```

  参数同上。

- 订阅所有可用的通知

  ```csharp
  Task SubscribeToAllAvailableNotificationsAsync([NotNull] Guid userId)
  ```

  - userId：取消订阅的用户ID。

- 获取通知的所有订阅者

  ```csharp
  Task<List<NotificationSubscriptionInfo>> GetSubscriptionsAsync([NotNull] string notificationName, [CanBeNull] NotificationEntityIdentifier entityIdentifier = null)
  ```

  - notificationName：订阅通知的名称。
  - entityIdentifier：关联实体。
    - Type：实体的类型
    - Id：实体的编号

- 获取用户的所有通知

  ```csharp
  Task<List<NotificationSubscriptionInfo>> GetSubscribedNotificationsAsync([NotNull] Guid userId);
  ```

  - userId：指定用户ID。

- 判断用户是否订阅某类通知

  ```csharp
  Task<bool> IsSubscribedAsync([NotNull] Guid userId, [NotNull] string notificationName, [CanBeNull] NotificationEntityIdentifier entityIdentifier = null);
  ```

  - userId：指定用户ID。
  - notificationName：通知的名称。
  - entityIdentifier：关联实体。
    - Type：实体的类型
    - Id：实体的编号

## 发布通知

`INotificationPublisher` 提供了发布通知的方法。

- 使用内置的 `MessageNotificationData` 向指定用户发布通知

    ```csharp
    public class PublishService : MyModuleAppService, IPublishService
    {
        private readonly INotificationPublisher _notiticationPublisher;

        public PublishService(INotificationPublisher notiticationPublisher)
        {
            _notiticationPublisher = notiticationPublisher;
        }
        
        public async Task Publish_Message(string message, Guid targetUserId)
        {
            await _notiticationPublisher.PublishAsync("Message", new MessageNotificationData(message), userIds: new[] { targetUserId });
        }
    }
    ```

- 使用内置的 `LocalizableMessageNotificationData` 向所有订阅用户发布通知

    ```csharp
    public class PublishService : MyModuleAppService, IPublishService
    {
        private readonly INotificationPublisher _notiticationPublisher;

        public PublishService(INotificationPublisher notiticationPublisher)
        {
            _notiticationPublisher = notiticationPublisher;
        }

        
        public async Task Publish_LowDisk(int remainingDiskInMb)
        {
            // MyLocalizationSourceName 是本地化资源名称
            // LowDiskWarningMessage 是本地化内容的Key。
            var data = new LocalizableMessageNotificationData("LowDiskWarningMessage", "MyLocalizationSourceName");
            data["remainingDiskInMb"] = remainingDiskInMb;

            await _notiticationPublisher.PublishAsync("System.LowDisk", data, severity: NotificationSeverity.Warn);
        }
    }
    ```

- 向图片所有者发布图片评论的通知

    1. 创建一个继承自 `NotificationData` 的通知数据类:

        ```csharp
        [Serializable]
        public class CommentPhotoNotificationData : NotificationData
        {
            public string CommenterUserName { get; set; }

            public string Comment { get; set; }

            public SentFrendshipRequestNotificationData(string commenterUserName, string comment)
            {
                CommenterUserName = commenterUserName;
                Comment = comment;
            }
        }
        ```

    2. 发布图片评论通知

        ```csharp
        public class PublishService : MyModuleAppService, IPublishService
        {
            private readonly INotificationPublisher _notiticationPublisher;

            public PublishService(INotificationPublisher notiticationPublisher)
            {
                _notiticationPublisher = notiticationPublisher;
            }

            public async Task Publish_CommentPhoto(string commenterUserName, string comment, Guid photoId, Guid photoOwnerUserId)
            {
                await _notiticationPublisher.PublishAsync("CommentPhoto", new CommentPhotoNotificationData(commenterUserName, comment), new NotificationEntityIdentifier(typeof(PhotoEntity), photoId.ToString()), userIds: new[] { photoOwnerUserId });
            }
        }
        ```

## 通知用户

通知系统使用 [分布式事件总线](https://docs.abp.io/en/abp/latest/Distributed-Event-Bus) 在微服务或应用程序之间发送通知事件，开发者可以使用 `IDistributedEventHandler<RealTimeNotifyEto>` 订阅通知事件。

通知系统内置一套基于 [SignalR](https://learn.microsoft.com/en-us/aspnet/core/signalr/introduction) 技术的实时接收通知事件。

### 安装SignalRNotifier

- 将 `Dignite.Abp.Notifications.SignalRNotifier` NuGet 包安装到服务端项目中。
- 添加 `DigniteAbpNotificationsSignalRNotifierModule` 到 [模块类](https://docs.abp.io/en/abp/latest/Module-Development-Basics) `[DependsOn(...)]`属性列表中.

请参阅[Blazor 版通知系统](Blazor-Notification-Center.md)

## 管理用户的通知

`IUserNotificationManager` 接口提供管理用户通知的一些方法：

- 获取用户的通知

  ```csharp
  Task<List<UserNotificationWithNotification>> GetUserNotificationsAsync(Guid userId, UserNotificationState? state = null, int skipCount = 0, int maxResultCount = int.MaxValue, DateTime? startDate = null, DateTime? endDate = null);
  ```

  - userId：用户ID。
  - state：已读和未读的枚举。
  - skipCount：分页获取用户通知时跳过的记录数。
  - maxResultCount：返回的记录数。
  - startDate：用户通知的时间段。
  - endDate：用户通知的时间段。

- 获取用户通知的总数量

  ```csharp
  Task<int> GetUserNotificationCountAsync(Guid userId, UserNotificationState? state = null, DateTime? startDate = null, DateTime? endDate = null);
  ```

  参数同上。

- 更新用户通知的状态

  ```csharp
  Task UpdateUserNotificationStateAsync(Guid userId, Guid notificationId, UserNotificationState state);
  ```

  - userId：用户ID。
  - notificationId：通知的ID。
  - state：已读和未读的枚举。

- 更新用户所有通知的状态

  ```csharp
  Task UpdateAllUserNotificationStatesAsync(Guid userId, UserNotificationState state);
  ```

  - userId：用户ID。
  - state：已读和未读的枚举。

- 删除用户的某条通知

  ```csharp
  Task DeleteUserNotificationAsync(Guid userId, Guid notificationId);
  ```

  - userId：用户ID。
  - notificationId：通知的ID。

- 删除用户的部分通知

  ```csharp
  Task DeleteAllUserNotificationsAsync(Guid userId, UserNotificationState? state = null, DateTime? startDate = null, DateTime? endDate = null);
  ```

  - userId：用户ID。
  - state：已读和未读的枚举。
  - startDate：用户通知的时间段。
  - endDate：用户通知的时间段。

## 定义通知

定义通知不是一项必须的工作，但是定义通知提供了一些额外选项，方便开发者开发个性化的通知系统。

### 一个简单的示例

继承 `NotificationDefinitionProvider` 创建一个通知定义提供者：

```csharp
public class NotificationCenterSampleNotificationDefinitionProvider : NotificationDefinitionProvider
{
    public override void Define(INotificationDefinitionContext context)
    {
        context.Add(new NotificationDefinition(NotificationCenterSampleNotifications.TestNotification, null,L("TestNotification"), L("TestNotificationDescription")));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<NotificationCenterSampleResource>(name);
    }
}
```

### 通知定义的选项

- Name

  通知的唯一名称

- EntityType（可选）

  与此通知相关的实体类型。

- DisplayName（可选）

  用于UI显示的通知名称

- Description（可选）

  通知的友好说明。

- PermissionName（可选）

  权限名称。
  如果用户有此权限，就可以订阅该通知。

- FeatureName（可选）

  功能名称。
  如果启用了该功能，租户就可以使用该通知。

- Attributes（可选）

  与此对象相关的任意对象，以字典方式存储。
  另外，这些对象必须可序列化。

### 通知定义的管理

`INotificationDefinitionManager` 接口提供了管理通知定义的一些方法：

- 根据名称获取一个通知定义

  ```csharp
  NotificationDefinition Get(string name);
  ```

  - name：通知定义的名称。

- 获取一个可以为 `null` 的通知定义

  当没有找到指定名称的通知定义时，将不抛出异常错误。

  ```csharp
  NotificationDefinition GetOrNull(string name);
  ```

  - name：通知定义的名称。

- 获取所有的通知定义

  ```csharp
  IReadOnlyList<NotificationDefinition> GetAll();
  ```

- 判断指定名称的通知定义，对指定的用户是否可用

  ```csharp
  Task<bool> IsAvailableAsync(string name, Guid userId);
  ```

  - name：通知定义的名称。
  - userId：用户ID。

- 获取指定用户所有可用的通知定义

  ```csharp
  Task<IReadOnlyList<NotificationDefinition>> GetAllAvailableAsync(Guid userId);
  ```

  - userId：用户ID。

> 在判断用户是否有权限使用某个通知定义时，需要配合 [Dignite.Abp.Notifications.Identity](https://github.com/dignite-projects/dignite-abp/tree/main/modules/notification-center/src/Dignite.Abp.Notifications.Identity) 模块使用。

## 通知的存储

通知系统提供了 `INotificationStore` 接口来实现通知、用户订阅数据的持久化。开发者可以通过继承该接口处行实现，也可以使用已经有完整实现的 [通知中心模块](Notification-Center.md)。

## 后续阅读

- 通知中心模块
  
  [通知中心模块](Notification-Center.md)是一套完整的通知系统，可以安装在你的应用系统中使用。

- Blazor 版通知系统
  
  [Blazor 版通知系统](Blazor-Notification-Center.md)用于安装在 Asp.Net Blazor应用中。
