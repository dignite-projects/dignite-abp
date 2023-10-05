# カスタムフィールド基本モジュール

動的フォームでは、カスタムフィールドとビジネスオブジェクトのカスタムフィールド値を管理するためのデータベース永続化技術が必要です。このモジュールでは、カスタムフィールドを含むビジネスオブジェクトを迅速に構築するための一連のメソッドを提供します。

## インストール

- `Domain.Shared` プロジェクトに `Dignite.Abp.FieldCustomizing.Domain.Shared` NuGet パッケージをインストールします。

- `Domain.Shared` [モジュールクラス](https://docs.abp.io/en/abp/latest/Module-Development-Basics) の `[DependsOn(...)]` 属性リストに `DigniteAbpFieldCustomizingDomainSharedModule` を追加します。

- `Domain` プロジェクトに `Dignite.Abp.FieldCustomizing.Domain` NuGet パッケージをインストールします。

- `Domain` [モジュールクラス](https://docs.abp.io/en/abp/latest/Module-Development-Basics) の `[DependsOn(...)]` 属性リストに `AbpFieldCustomizingDomainModule` を追加します。

- `EntityFrameworkCore` プロジェクトに `Dignite.Abp.FieldCustomizing.EntityFrameworkCore` NuGet パッケージをインストールします。

- `EntityFrameworkCore` [モジュールクラス](https://docs.abp.io/en/abp/latest/Module-Development-Basics) の `[DependsOn(...)]` 属性リストに `DigniteAbpFieldCustomizingEntityFrameworkCoreModule` を追加します。

## フィールドエンティティクラスの作成

`CustomizeFieldDefinitionBase` はフィールドエンティティクラスを定義するための抽象クラスで、[ICustomizeFieldInfo](Dynamic-Forms.md#フィールド情報の定義) インターフェースを実装しています。

> `ICustomizeFieldInfo` からフィールドエンティティクラスを作成することもできますが、`CustomizeFieldDefinitionBase` から作成することでコードを簡素化できます。

例:

```csharp
public class ProductFieldDefinition : CustomizeFieldDefinitionBase
{    
    /// <summary>
    /// フィールドのグループ
    /// </summary>
    public string Group { get; set; }
}
```

## ビジネスオブジェクトエンティティクラスの作成

[IHasCustomFields](Dynamic-Forms.md#カスタムフィールドを持つビジネスオブジェクト) はビジネスオブジェクトエンティティクラスのカスタムフィールド値を管理するために使用されるインターフェースです。

例:

```csharp
public class Product : AuditedAggregateRoot<Guid>, IHasCustomFields
{    
    /// <summary>
    /// 製品カテゴリ ID
    /// </summary>
    public Guid CategoryId { get; set; }

    /// <summary>
    /// 製品名
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 製品 SKU 情報
    /// </summary>
    public CustomFieldDictionary CustomFields { get; set; }
}
```

## Ef Core エンティティの構成

`Dignite.Abp.FieldCustomizing.EntityFrameworkCore` は `ICustomizeFieldInfo` および `IHasCustomFields` インターフェースの実装を構成するための便利な拡張メソッドを提供します。

- `ConfigureCustomizableFieldDefinitions()` メソッド

    このメソッドは、フィールド定義エンティティのプロパティと規約を構成するためのものです。 `DbContext` の `OnModelCreating` メソッドで次のように構成します。

    ```csharp
    protected override void OnModelCreating(ModelBuilder builder)
    {
        // 常にベースメソッドを呼び出します
        base.OnModelCreating(builder);

        builder.Entity<ProductFieldDefinition>(b =>
        {
            // テーブルを構成
            b.ToTable("ProductFieldDefinitions");

            b.ConfigureByConvention();

            // フィールドを構成
            b.ConfigureCustomizableFieldDefinitions();

            // プロパティ
            b.Property(q => q.Group).HasMaxLength(64);
        });
    }
    ```

- `ConfigureObjectCustomizedFields()` メソッド

    このメソッドは、カスタムフィールド値を持つビジネスオブジェクトエンティティのプロパティと規約を構成するためのものです。 `DbContext` の `OnModelCreating` メソッドで次のように構成します。

    ```csharp
    protected override void OnModelCreating(ModelBuilder builder)
    {
        // 常にベースメソッドを呼び出します
        base.OnModelCreating(builder);    

        builder.Entity<Product>(b =>
        {
            // テーブルを構成
            b.ToTable("Products");

            b.ConfigureByConvention();

            // オブジェクトのカスタマイズフィールドを構成
            b.ConfigureObjectCustomizedFields();

            // プロパティ
            b.Property(q => q.Name).HasMaxLength(128);
        });
    }
    ```

## 推奨読書

- [Dignite.Cms](https://dignite.com/dignite-cms)

    フレキシブルなフォーム定義を持つ [カスタムフィールド基本モジュール](Field-Customizing.md) に基づいて開発されたコンテンツ管理システム。
