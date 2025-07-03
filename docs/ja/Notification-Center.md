# 通知センターモジュール

```json
//[doc-params]
{
    "DB": ["EF", "Mongo"]
}
```

通知センターモジュールは、Abpアプリケーションモジュールであり、通知の発行と受信を実現するために、簡単にアプリケーションシステムに統合できます。

## インストール

1. `Dignite.Abp.NotificationCenter.Domain.Shared` NuGetパッケージを`Domain.Shared`プロジェクトにインストールします。

   `[DependsOn(...)]`属性リストに`AbpNotificationCenterDomainSharedModule`を追加して、 [モジュールクラス](https://docs.abp.io/en/abp/latest/Module-Development-Basics) にも追加します。

2. `Dignite.Abp.NotificationCenter.Domain` NuGet パッケージを Domain プロジェクトにインストールします。

   同様に、[モジュールクラス](https://docs.abp.io/en/abp/latest/Module-Development-Basics) に `AbpNotificationCenterDomainModule` を追加します。

3. Entity Framework Core（EF）を使用している場合、`Dignite.Abp.NotificationCenter.EntityFrameworkCore` NuGet パッケージを Entity Framework Core プロジェクトにインストールします。

   `[DependsOn(...)]`属性リストに `AbpNotificationCenterEntityFrameworkCoreModule` を追加します。

   `OnModelCreating()` メソッドに以下の設定を追加します：

   ```csharp
   protected override void OnModelCreating(ModelBuilder modelBuilder)
   {
       base.OnModelCreating(modelBuilder);

       modelBuilder.ConfigurePermissionManagement();
       modelBuilder.ConfigureSettingManagement();
       modelBuilder.ConfigureAuditLogging();
       modelBuilder.ConfigureIdentity();
       modelBuilder.ConfigureFeatureManagement();
       modelBuilder.ConfigureTenantManagement();
       modelBuilder.ConfigureNotificationCenter(); // この行を追加して NotificationCenter モジュールを構成します
   }
   ```

   Visual Studio のパッケージマネージャーコンソールを開き、デフォルトプロジェクトとして `DbMigrations` を選択し、次のコマンドを実行して通知センターモジュールにマイグレーションを追加します：

   ```csharp
   add-migration Added_NotificationCenter_Module
   ```

   次に、データベースを更新するために次のコマンドを実行します：

   ```csharp
   update-database
   ```

4. MongoDBを使用している場合、`Dignite.Abp.NotificationCenter.MongoDB` NuGet パッケージを MongoDB プロジェクトにインストールします。

   `[DependsOn(...)]`属性リストに `AbpNotificationCenterMongoDbModule` を追加します。

   同様に、`OnModelCreating()` メソッドに以下の設定を追加します：

   ```csharp
   protected override void OnModelCreating(ModelBuilder modelBuilder)
   {
       base.OnModelCreating(modelBuilder);

       modelBuilder.ConfigurePermissionManagement();
       modelBuilder.ConfigureSettingManagement();
       modelBuilder.ConfigureAuditLogging();
       modelBuilder.ConfigureIdentity();
       modelBuilder.ConfigureFeatureManagement();
       modelBuilder.ConfigureTenantManagement();
       modelBuilder.ConfigureNotificationCenter(); // この行を追加して NotificationCenter モジュールを構成します
   }
   ```

5. `Dignite.Abp.Notifications.Identity` NuGet パッケージを Domain プロジェクトにインストールします。

   同様に、[モジュールクラス](https://docs.abp.io/en/abp/latest/Module-Development-Basics) に `AbpNotificationsIdentityModule` を追加します。

6. `Dignite.Abp.NotificationCenter.Application.Contracts` NuGet パッケージを Application.Contracts プロジェクトにインストールします。

   `[DependsOn(...)]`属性リストに `AbpNotificationCenterApplicationContractsModule` を追加します。

7. `Dignite.Abp.NotificationCenter.Application` NuGet パッケージを Application プロジェクトにインストールします。

   `[DependsOn(...)]`属性リストに `AbpNotificationCenterApplicationModule` を追加します。

8. `Dignite.Abp.NotificationCenter.HttpApi` NuGet パッケージを HttpApi プロジェクトにインストールします。

   `[DependsOn(...)]`属性リストに `AbpNotificationCenterHttpApiModule` を追加します。
