# Notification System Core

Dignite Abp Notification System is developed based on the [Asp.Net Boilerplate Notification System](https://aspnetboilerplate.com/Pages/Documents/Notification-System) and relies on the [Distributed Event Bus](https://docs.abp.io/en/abp/latest/Distributed-Event-Bus) for publishing and subscribing to notifications.

## Installation

- Install the `Dignite.Abp.Notifications` NuGet package in the Domain project.
- Add `AbpNotificationsModule` to the `[DependsOn(...)]` attribute list in your [module class](https://docs.abp.io/en/abp/latest/Module-Development-Basics).

> If you have already installed the [Notification Center Module](Notification-Center.md), the `Dignite.Abp.Notifications` module is already installed.

## Subscribing to Notifications

There are two ways to send notifications to users: sending them directly to specific users or sending them to subscribed users. Users subscribe to specific notifications, and when the system publishes such a notification, it is sent to all subscribed users.

### Subscribing to Notifications Example

The `INotificationSubscriptionManager` provides methods to subscribe to notifications. Here's an example:

```csharp
public class SubscribeService : MyModuleAppService, ISubscribeService
{
    private readonly INotificationSubscriptionManager _notificationSubscriptionManager;

    public SubscribeService(INotificationSubscriptionManager notificationSubscriptionManager)
    {
        _notificationSubscriptionManager = notificationSubscriptionManager;
    }

    /// <summary>
    /// Sends a notification to the user when a friend request is sent.
    /// </summary>
    [Authorize]
    public async Task Subscribe_SentFriendshipRequest()
    {
        await _notificationSubscriptionManager.SubscribeAsync(CurrentUser.Id.Value, "SentFriendshipRequest");
    }

    /// <summary>
    /// Subscribes to a notification associated with a specific entity (PhotoEntity). When a user comments on that entity, a notification is sent to subscribed users.
    /// </summary>
    [Authorize]
    public async Task Subscribe_CommentPhoto(Guid photoId)
    {
        await _notificationSubscriptionManager.SubscribeAsync(CurrentUser.Id.Value, "CommentPhoto", new NotificationEntityIdentifier(typeof(PhotoEntity), photoId.ToString()));
    }
}
```

### API

The `INotificationSubscriptionManager` interface provides a set of methods to manage subscriptions:

- Subscribe to a notification

  ```csharp
  Task SubscribeAsync([NotNull] Guid userId, [NotNull] string notificationName, [CanBeNull] NotificationEntityIdentifier entityIdentifier = null)
  ```

  - userId: The ID of the subscribing user.
  - notificationName: The name of the notification to subscribe to.
  - entityIdentifier: Identifies a specific entity.
    - Type: The type of the specific entity.
    - Id: The ID of the specific entity.

- Unsubscribe from a notification

  ```csharp
  UnsubscribeAsync([NotNull] Guid userId, [NotNull] string notificationName, [CanBeNull] NotificationEntityIdentifier entityIdentifier = null)
  ```

  Parameters are the same as for subscription.

- Subscribe to all available notifications

  ```csharp
  Task SubscribeToAllAvailableNotificationsAsync([NotNull] Guid userId)
  ```

  - userId: The ID of the user to subscribe on behalf of.

- Get all subscribers of a notification

  ```csharp
  Task<List<NotificationSubscriptionInfo>> GetSubscriptionsAsync([NotNull] string notificationName, [CanBeNull] NotificationEntityIdentifier entityIdentifier = null)
  ```

  - notificationName: The name of the notification to get subscribers for.
  - entityIdentifier: Identifies a specific entity.
    - Type: The type of the specific entity.
    - Id: The ID of the specific entity.

- Get all notifications subscribed by a user

  ```csharp
  Task<List<NotificationSubscriptionInfo>> GetSubscribedNotificationsAsync([NotNull] Guid userId);
  ```

  - userId: The ID of the user to get subscribed notifications for.

- Check if a user is subscribed to a particular notification

  ```csharp
  Task<bool> IsSubscribedAsync([NotNull] Guid userId, [NotNull] string notificationName, [CanBeNull] NotificationEntityIdentifier entityIdentifier = null);
  ```

  - userId: The ID of the user to check.
  - notificationName: The name of the notification to check subscription for.
  - entityIdentifier: Identifies a specific entity.
    - Type: The type of the specific entity.
    - Id: The ID of the specific entity.

## Publishing Notifications

The `INotificationPublisher` provides methods for publishing notifications.

- Publishing a notification to specific users using the built-in `MessageNotificationData`

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

- Publishing a notification to all subscribed users using the built-in `LocalizableMessageNotificationData`

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
            // MyLocalizationSourceName is the localization resource name.
            // LowDiskWarningMessage is the key for localized content.
            var data = new LocalizableMessageNotificationData("LowDiskWarningMessage", "MyLocalizationSourceName");
            data["remainingDiskInMb"] = remainingDiskInMb;

            await _notificationPublisher.PublishAsync("System.LowDisk", data, severity: NotificationSeverity.Warn);
        }
    }
    ```

- Publishing a notification to the owner of a photo when a comment is made

    1. Create a notification data class that inherits from `NotificationData`:

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

    2. Publish the notification when a comment is made on a photo:

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

## Notifying Users

The Notification System uses the

 [Distributed Event Bus](https://docs.abp.io/en/abp/latest/Distributed-Event-Bus) to send notification events between microservices or applications. Developers can subscribe to notification events using the `IDistributedEventHandler<RealTimeNotifyEto>`.

The Notification System includes a built-in real-time notification event system based on [SignalR](https://learn.microsoft.com/en-us/aspnet/core/signalr/introduction) technology.

### Installing SignalRNotifier

- Install the `Dignite.Abp.Notifications.SignalRNotifier` NuGet package in the server-side project.
- Add `AbpNotificationsSignalRNotifierModule` to the `[DependsOn(...)]` attribute list in your [module class](https://docs.abp.io/en/abp/latest/Module-Development-Basics).

See also: [Blazor Notification Center](Blazor-Notification-Center.md)

## Managing User Notifications

The `IUserNotificationManager` interface provides methods for managing user notifications:

- Get user notifications

  ```csharp
  Task<List<UserNotificationWithNotification>> GetUserNotificationsAsync(Guid userId, UserNotificationState? state = null, int skipCount = 0, int maxResultCount = int.MaxValue, DateTime? startDate = null, DateTime? endDate = null);
  ```

  - userId: The ID of the user.
  - state: Enumeration for read and unread notifications.
  - skipCount: The number of records to skip when paging through user notifications.
  - maxResultCount: The number of records to return.
  - startDate: The time range for user notifications.
  - endDate: The time range for user notifications.

- Get the total count of user notifications

  ```csharp
  Task<int> GetUserNotificationCountAsync(Guid userId, UserNotificationState? state = null, DateTime? startDate = null, DateTime? endDate = null);
  ```

  Parameters are the same as for the previous method.

- Update the state of a user notification

  ```csharp
  Task UpdateUserNotificationStateAsync(Guid userId, Guid notificationId, UserNotificationState state);
  ```

  - userId: The ID of the user.
  - notificationId: The ID of the notification.
  - state: Enumeration for read and unread notifications.

- Update the state of all user notifications

  ```csharp
  Task UpdateAllUserNotificationStatesAsync(Guid userId, UserNotificationState state);
  ```

  - userId: The ID of the user.
  - state: Enumeration for read and unread notifications.

- Delete a user notification

  ```csharp
  Task DeleteUserNotificationAsync(Guid userId, Guid notificationId);
  ```

  - userId: The ID of the user.
  - notificationId: The ID of the notification.

- Delete a subset of user notifications

  ```csharp
  Task DeleteAllUserNotificationsAsync(Guid userId, UserNotificationState? state = null, DateTime? startDate = null, DateTime? endDate = null);
  ```

  - userId: The ID of the user.
  - state: Enumeration for read and unread notifications.
  - startDate: The time range for user notifications.
  - endDate: The time range for user notifications.

## Defining Notifications

Defining notifications is not mandatory, but it provides additional options for developers to create a personalized notification system.

### A Simple Example

Inherit from `NotificationDefinitionProvider` to create a notification definition provider:

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

### Options for Notification Definitions

- Name

  The unique name of the notification.

- EntityType (optional)

  The entity type associated with this notification.

- DisplayName (optional)

  The display name of the notification used for UI presentation.

- Description (optional)

  A friendly description of the notification.

- PermissionName (optional)

  The name of the permission.
  If a user has this permission, they can subscribe to the notification.

- FeatureName (optional)

  The name of the feature.
  If this feature is enabled, tenants can use the notification.

- Attributes (optional)

  Any objects related to this object, stored in a dictionary.
  Additionally, these objects must be serializable.

### Managing Notification Definitions

The `INotificationDefinitionManager` interface provides methods for managing notification definitions:

- Get a notification definition by its name

  ```csharp
  NotificationDefinition Get(string name);
  ```

  - name: The name of the notification definition.

- Get a notification definition that can be null

  This method does not throw an exception if a notification definition with the specified name is not found.

  ```csharp
  NotificationDefinition GetOrNull(string name);
  ```

  - name: The name of the notification definition.

- Get all notification definitions

  ```csharp
  IReadOnlyList<NotificationDefinition> GetAll();
  ```

- Check if a specified user has access to a notification definition by its name

  ```csharp
  Task<bool> IsAvailableAsync(string name, Guid userId);
  ```

  - name: The name of the notification definition.
  - userId: The user's ID.

- Get all available notification definitions for a specific user

  ```csharp
  Task<IReadOnlyList<NotificationDefinition>> GetAllAvailableAsync(Guid userId);
  ```

  - userId: The user's ID.

> To check if a user has permission to use a specific notification definition, you should use the [Dignite.Abp.Notifications.Identity](https://github.com/dignite-projects/dignite-abp/tree/main/modules/notification-center/src/Dignite.Abp.Notifications.Identity) module in conjunction with it.

## Notification Storage

The Notification System provides the `INotificationStore` interface to implement the persistence of notification and user subscription data. Developers can implement this interface or use the fully-featured [Notification Center Module](Notification-Center.md).

## Further Reading

- Notification Center Module

  The [Notification Center Module](Notification-Center.md) is a comprehensive notification system that can be installed in your application system.

- Blazor Notification System

  The [Blazor Notification System](Blazor-Notification-Center.md) is designed for installation in ASP.NET Blazor applications.
