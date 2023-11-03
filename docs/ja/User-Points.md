# ユーザーポイントモジュール

```json
//[doc-params]
{
    "DB": ["EF", "Mongo"]
}
```

ユーザーポイントモジュールは、[ポイントコア](Points.md)モジュールをベースに構築された、ユーザーポイントとポイントの消費を管理するための完全なAbpアプリケーションモジュールです。

## インストール

- `Dignite.Abp.UserPoints.Domain.Shared` NuGet パッケージを `Domain.Shared` プロジェクトにインストールします。[モジュールクラス](https://docs.abp.io/en/abp/latest/Module-Development-Basics)の`[DependsOn(...)]`プロパティリストに`UserPointsDomainSharedModule`を追加します。

- `Dignite.Abp.UserPoints.Domain` NuGet パッケージを Domain プロジェクトにインストールします。[モジュールクラス](https://docs.abp.io/en/abp/latest/Module-Development-Basics)の`[DependsOn(...)]`プロパティリストに`UserPointsDomainModule`を追加します。

{{if DB == "EF"}}

- Entity Framework Core（EF）を使用している場合、`Dignite.Abp.UserPoints.EntityFrameworkCore` NuGet パッケージをインストールします。`builder.ConfigureUserPoints()` を `OnModelCreating()` メソッドに追加します。

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
        modelBuilder.ConfigureUserPoints(); // Abp.UserPointsモジュールを構成するためにこの行を追加
    }
    ```

    Visual Studioのパッケージマネージャーコンソールを開き、デフォルトプロジェクトとして`DbMigrations`を選択し、次のコマンドを実行してユーザーポイントモジュールのマイグレーションを追加します。

    ```csharp
    add-migration Add_AbpUserPoints_Module
    ```

    今すぐデータベースを更新します。

    ```csharp
    update-database
    ```

{{end}}

{{if DB == "Mongo"}}

- MongoDBを使用している場合、`Dignite.Abp.UserPoints.MongoDB` NuGet パッケージをインストールします。`builder.ConfigureUserPoints()` を `OnModelCreating()` メソッドに追加します。

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
        modelBuilder.ConfigureUserPoints(); // Abp.UserPointsモジュールを構成するためにこの行を追加
    }
    ```

{{end}}

- `Dignite.Abp.UserPoints.Application.Contracts` NuGet パッケージを Application.Contracts プロジェクトにインストールします。[モジュールクラス](https://docs.abp.io/en/abp/latest/Module-Development-Basics)の`[DependsOn(...)]`プロパティリストに`UserPointsApplicationContractsModule`を追加します。

- `Dignite.Abp.UserPoints.Application` NuGet パッケージを Application プロジェクトにインストールします。[モジュールクラス](https://docs.abp.io/en/abp/latest/Module-Development-Basics)の`[DependsOn(...)]`プロパティリストに`UserPointsApplicationModule`を追加します。

- `Dignite.Abp.UserPoints.HttpApi` NuGet パッケージを HttpApi プロジェクトにインストールします。[モジュールクラス](https://docs.abp.io/en/abp/latest/Module-Development-Basics)の`[DependsOn(...)]`プロパティリストに`UserPointsHttpApiModule`を追加します。

- `Dignite.Abp.UserPoints.HttpApi.Client` NuGet パッケージを HttpApi プロジェクトにインストールします。[モジュールクラス](https://docs.abp.io/en/abp/latest/Module-Development-Basics)の`[DependsOn(...)]`プロパティリストに`UserPointsHttpApiClientModule`を追加します。

## ユーザーポイントオプション

`DignitePointsBlockOptions`はユーザーポイントのオプションクラスです。

- Factor: ポイントと通貨の交換レートです。通常、1ユニットの通貨に対して何ポイントが等価かを表します。デフォルト値は1です。ユーザーポイントの単位ポイント値は、ポイントファクターの倍数である必要があります。例えば、ポイントファクターを100に設定すると、ユーザーポイントの残高は100、200、300など、100の倍数である必要があります。

サンプルコード：

```csharp
Configure<DignitePointsBlockOptions>(options =>
{
    options.Factor = 100;
});
```

## ユーザーポイントの管理

`UserPointsItemManager`クラスはユーザーポイントを管理するための一連のメソッドを提供します。

- ユーザーポイントの作成

  ```csharp
  Task<UserPointsItem> CreateAsync(
        PointsType pointsType,
        string pointsDefinitionName,
        string pointsWorkflowName,
        int points,
        DateTime expirationDate,
        Guid userId,
        Guid? tenantId)
  ```

  - pointsType: ユーザーポイントのタイプです。2つの値を持つ列挙型です。
    - General: 通常のポイントで、すべての通常のポイントは統合して消費できます。
    - Specialized: 専用ポイントで、ポイントを消費する際にポイントワークフローを指定する必要があります。
  - pointsDefinitionName: ユーザーポイントのポイント定義名です。これは[ポイントの定義](Points.md#定義ポイント)の`Name`です。
  - pointsWorkflowName: ユーザーポイントのポイントワークフロー名です。これは[ポイントの定義](Points.md#定義ポイント)の`PointsWorkflow`の`Name`です。
  - points: ユーザーポイントの値
  - expirationDate: ポイントの有効期限
  - userId: ポイントの所有者のユーザーID
  - tenantId: ポイントの所属テナントID

- ユーザーポイントの削除

  ```csharp
  Task DeleteAsync(UserPointsItem pointsItem)
  ```

## ユーザーポイントオーダーの管理

`UserPointsOrderManager`クラスはユーザーポイントのオーダーを管理するための一連のメソッドを提供します。

- ポイントオーダーの作成

  ```csharp
  Task<UserPointsOrder> CreateAsync(
        int points,
        string businessOrderType,
        string businessOrderNumber, 
        Guid userId,
        PointsType pointsType = PointsType.General, 
        string pointsDefinitionName = null, 
        string pointsWorkflowName = null,
        Guid? tenantId=null)
  ```
  
  - points: 消費するポイントの値
  - businessOrderType: ビジネスオーダーのタイプ
  - businessOrderNumber: ビジネスオーダーの番号
  - userId: ポイント消費者のユーザーID
  - pointsType: 消費するポイントのタイプです。2つの値を持つ列挙型です。
    - General: 通常のポイントを消費します。
    - Specialized: ポイント定義名とポイントワークフロー名を指定する必要があります。
  - pointsDefinitionName: 消費するポイントのポイント定義名です。これは[ポイントの定義](Points.md#ポイントの定義)の`Name`です。
  - pointsWorkflowName: 消費するポイントのポイントワークフロー名です。これは[ポイントの定義](Points.md#ポイントの定義)の`PointsWorkflow`の`Name`です。
  - tenantId: 消費するポイントの所属テナントID

- ポイントオーダーの削除

  ```csharp
  Task DeleteAsync(UserPointsOrder pointsOrder, bool shouldRollbackPoints)
  ```

  - pointsOrder: ポイントオーダーエンティティオブジェクト
  - shouldRollbackPoints: ポイントを払い戻すかどうか

- ビジネスオーダー番号でポイントオーダーを検索

  ```csharp
  Task<UserPointsOrder> FindByBusinessOrderAsync(string businessOrderType, string businessOrderNumber)
  ```

  - businessOrderType: ビジネスオーダーのタイプ
  - businessOrderNumber: ビジネスオーダーの番号

## API

- ユーザーの利用可能なポイントの合計を取得

  ```csharp
  Task<int> GetTotalPointsAsync(GetUserTotalPointsInput input)
  ```

  - ExpirationDate [オプション]: ポイントの有効期限
  - PointsDefinitionName [オプション]: ポイント定義名
  - PointsWorkflowName [オプション]: ポイントワークフロー名

  `PointsDefinitionName`と`PointsWorkflowName`が設定されていない場合、すべての一般ポイントを取得します。

- ユーザーポイントアイテムリストを取得

  ```csharp
  Task<PagedResultDto<UserPointsItemDto>> GetListAsync(GetUserPointsItemsInput input)
  ```

  - StartTime [オプション]: ポイントの作成開始時間
  - EndTime [オプション]: ポイントの作成終了時間
  - PointsDefinitionName [オプション]: ポイント定義名
  - PointsWorkflowName [オプション]: ポイントワークフロー名

  `PointsDefinitionName`と`PointsWorkflowName`が設定されていない場合、すべての一般ポイントを取得します。
