# Dignite Abp 通知システムコア

Dignite Abp 通知システムは、[Asp.Net Boilerplate通知システム](https://aspnetboilerplate.com/Pages/Documents/Notification-System)を基に開発され、通知の発行と購読に[分散イベントバス](https://docs.abp.io/en/abp/latest/Distributed-Event-Bus)を使用しています。

## インストール

Dignite Abp通知システムを使用するには、まず以下の手順を実行する必要があります：

1. `Dignite.Abp.Notifications` NuGet パッケージをドメインプロジェクトにインストールします。
2. モジュールクラスの`[DependsOn(...)]`属性リストに`DigniteAbpNotificationsModule`を追加します。

すでに[通知センターモジュール](Notification-Center.md)をインストールしている場合、`Dignite.Abp.Notifications`モジュールを別途インストールする必要はありません。

## 通知の購読

Dignite Abp通知システムは、ユーザーに通知を送信する2つの方法をサポートしています：

1. 特定のユーザーに送信します。
2. 購読ユーザーに送信します。ユーザーは特定の通知を購読し、システムがその通知を発行すると、購読済みユーザー全員に通知されます。

### 購読の例

`INotificationSubscriptionManager` インターフェースを使用して通知の購読を実装します。以下はいくつかのサンプルコードです：

```csharp
public class SubscribeService : MyModuleAppService, ISubscribeService
{
    private readonly INotificationSubscriptionManager _notificationSubscriptionManager;

    public SubscribeService(INotificationSubscriptionManager notificationSubscriptionManager)
    {
        _notificationSubscriptionManager = notificationSubscriptionManager;
    }

    /// <summary>
    /// 友達のリクエストを送信すると、ユーザーにリクエスト通知を送信します
    /// </summary>
    [Authorize]
    public async Task Subscribe_SentFriendshipRequest()
    {
        await _notificationSubscriptionManager.SubscribeAsync(CurrentUser.Id.Value, "SentFriendshipRequest");
    }

    /// <summary>
    /// 特定のエンティティ（PhotoEntity）に関連付けられた通知を購読します。ユーザーがそのエンティティにコメントを投稿した場合、購読ユーザーに通知が送信されます。
    /// </summary>
    [Authorize]
    public async Task Subscribe_CommentPhoto(Guid photoId)
    {
        await _notificationSubscriptionManager.SubscribeAsync(CurrentUser.Id.Value, "CommentPhoto", new NotificationEntityIdentifier(typeof(PhotoEntity), photoId.ToString()));
    }
}
```

### 購読管理 API

`INotificationSubscriptionManager` インターフェースは、通知の購読操作を管理するための一連のメソッドを提供します。これには次のものが含まれます：

- 通知の購読：

  ```csharp
  Task SubscribeAsync([NotNull] Guid userId, [NotNull] string notificationName, [CanBeNull] NotificationEntityIdentifier entityIdentifier = null)
  ```

  - userId：通知を購読するユーザーのID。
  - notificationName：通知の名前。
  - entityIdentifier：特定のエンティティと関連付けます。
    - Type：特定のエンティティの型。
    - Id：特定のエンティティのID。

- 通知の購読を解除：

  ```csharp
  Task UnsubscribeAsync([NotNull] Guid userId, [NotNull] string notificationName, [CanBeNull] NotificationEntityIdentifier entityIdentifier = null)
  ```

- すべての利用可能な通知を購読：

  ```csharp
  Task SubscribeToAllAvailableNotificationsAsync([NotNull] Guid userId)
  ```

- 通知のすべての購読者を取得：

  ```csharp
  Task<List<NotificationSubscriptionInfo>> GetSubscriptionsAsync([NotNull] string notificationName, [CanBeNull] NotificationEntityIdentifier entityIdentifier = null)
  ```

- ユーザーのすべての通知購読を取得：

  ```csharp
  Task<List<NotificationSubscriptionInfo>> GetSubscribedNotificationsAsync([NotNull] Guid userId);
  ```

- ユーザーが特定の通知を購読しているかどうかを確認：

  ```csharp
  Task<bool> IsSubscribedAsync([NotNull] Guid userId, [NotNull] string notificationName, [CanBeNull] NotificationEntityIdentifier entityIdentifier = null);
  ```

## 通知の発行

`INotificationPublisher` インターフェースを使用して通知を発行します：

- 指定したユーザーに通知を発行するために、組み込みの `MessageNotificationData` を使用します：

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

- すべての購読ユーザーに通知を発行するために、組み込みの `LocalizableMessageNotificationData` を使用します：

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

- 画像の所有者に画像コメントの通知を発行する例：

  1. `NotificationData` を継承した通知データクラスを作成します：

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

  2. 画像コメントの通知を発行します：

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

## ユーザーへの通知

通知システムは[分散イベントバス](https://docs.abp.io/en/abp/latest/Distributed-Event-Bus)を使用して通知イベントを発行します。開発者は `IDistributedEventHandler<RealTimeNotifyEto>` を使用して通知イベントを購読できます。通知システムはまた、[SignalR](https://learn.microsoft.com/en-us/aspnet/core/signalr/introduction) テクノロジーをベースにしたリアルタイム通知機能をサポートしています。

### SignalRNotifier のインストール

リアルタイム通知機能を使用するには、以下の手順を実行して SignalRNotifier をインストールします：

1. サーバープロジェクトに `Dignite.Abp.Notifications.SignalRNotifier` NuGet パッケージをインストールします。
2. モジュールクラスの `[DependsOn(...)]` 属性リストに `DigniteAbpNotificationsSignalRNotifierModule` を追加します。

詳細については、[Blazor 版通知システム](Blazor-Notification-Center.md)を参照してください。

## ユーザーの通知管理

`IUserNotificationManager` インターフェースを使用して、ユーザーの通知を管理します：

- ユーザーの通知を取得：

  ```csharp
  Task<List<UserNotificationWithNotification>> GetUserNotificationsAsync(Guid userId, UserNotificationState? state = null, int skipCount = 0, int maxResultCount = int.MaxValue, DateTime? startDate = null, DateTime? endDate = null);
  ```

- ユーザーの通知の総数を取得：

  ```csharp
  Task<int> GetUserNotificationCountAsync(Guid userId, UserNotificationState? state = null, DateTime? startDate = null, DateTime? endDate = null);
  ```

- ユーザーの通知の状態を更新：

  ```csharp
  Task UpdateUserNotificationStateAsync(Guid userId, Guid notificationId, UserNotificationState state);
  ```

- ユーザーのすべての通知の状態を更新：

  ```csharp
  Task UpdateAllUserNotificationStatesAsync(Guid userId, UserNotificationState state);
  ```

- ユーザーの特定の通知を削除：

  ```csharp
  Task DeleteUserNotificationAsync(Guid userId, Guid notificationId);
  ```

- ユーザーの一部の通知を削除：

  ```csharp
  Task DeleteAllUserNotificationsAsync(Guid userId, UserNotificationState? state = null, DateTime? startDate = null, DateTime? endDate = null);
  ```

## 通知の定義

通知を定義すると、カスタマイズされた通知システムを開発するためのいくつかの追加オプションが提供されます。通知の定義には通常、次の情報が含まれます：

- Name：通知の一意の名前。
- EntityType（オプション）：通知に関連するエンティティのタイプ。
- DisplayName（オプション）：UIで表示される通知の名前。
- Description（オプション）：通知のフレンドリーな説明。
- PermissionName（オプション）：関連する権限の名前。
- FeatureName（オプション）：関連する機能の名前。
- Attributes（オプション）：通知に関連する任意のオブジェクトを、ディクショナリ形式で格納します。

### 通知の定義の例

以下は通知の定義プロバイダーの例です：

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

### 通知の定義の管理

通知の定義を管理するには、`INotificationDefinitionManager` インターフェースを使用します：

- 名前に基づいて通知定義を取得：

  ```csharp
  NotificationDefinition Get(string name);
  ```

- 名前に基づいて通知定義を取得し、存在しない場合は `null` を返します：

  ```csharp
  NotificationDefinition GetOrNull(string name);
  ```

- すべての通知定義を取得：

  ```csharp
  IReadOnlyList<NotificationDefinition> GetAll();
  ```

- 指定されたユーザーが指定された通知定義を使用できるかどうかを判断：

  ```csharp
  Task<bool> IsAvailableAsync(string name, Guid userId);
  ```

- 指定されたユーザーに利用可能なすべての通知定義を取得：

  ```csharp
  Task<IReadOnlyList<NotificationDefinition>> GetAllAvailableAsync(Guid userId);
  ```

> ユーザーが特定の通知定義を使用できるかどうかを判断する際には、[Dignite.Abp.Notifications.Identity](https://github.com/dignite-projects/dignite-abp/tree/main/modules/notification-center/src/Dignite.Abp.Notifications.Identity) モジュールを使用する必要があります。

## 通知の保存

通知システムは `INotificationStore` インターフェースを使用して通知とユーザーの購読データを永続化します。開発者はこのインターフェースを実装することで独自のデータストアを作成できますが、既存の完全な実装が提供されている[通知センターモジュール](Notification-Center.md)を使用することもできます。

## 追加リーディング

- 通知センターモジュール

  [通知センターモジュール](Notification-Center.md)は完全な通知システムを提供し、アプリケーションに統合できます。

- Blazor 版通知システム

  [Blazor 版通知システム](Blazor-Notification-Center.md)はAsp.Net Blazorアプリケーションで使用できます。
