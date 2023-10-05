# ダイナミックフォーム

`Dignite.Abp.DynamicForms` は、実行時にオブジェクトの拡張データをダイナミックに管理するためのフォームのセットを定義します。これは、カスタムフィールドを持つ製品SKU、オンラインアンケート、CMSなどのシステムで共通に使用され、拡張データを取得、保存、管理するための共通仕様を提供します。

`Dignite.Abp.DynamicForms` と `[Volo.Abp.ObjectExtending](https://docs.abp.io/en/abp/latest/Object-Extensions)` の違い：

- `[Volo.Abp.ObjectExtending]` は、開発中にプログラム的にオブジェクトを拡張するために開発者が使用します。
- `Dignite.Abp.DynamicForms` は管理者がランタイム中にオブジェクトのフィールドを定義できるようにします。

`Dignite.Abp.DynamicForms` は次の2つの主要なパーツで構成されています。

1. フォームエンジン：
   `Dignite.Abp.DynamicForms` はテキストボックス、ドロップダウン、スイッチ、日付ピッカー、ファイルアップロード、数値入力などのシンプルなフォームコントロールと、テーブルや行列などの複雑なフォームを提供します。

2. カスタムフィールド：
   カスタムフィールドを使用して、ダイナミックフォームのデータを取得または設定できます。

> ダイナミックフォームは通常、カスタムフィールドを持つビジネスオブジェクトと組み合わせて使用されます。ビジネスオブジェクトは [カスタムフィールド基本モジュール](Field-Customizing.md) をベースに構築することをお勧めします。

## インストール

`Dignite.Abp.DynamicForms` を使用するには、次の手順を実行してください。

- Blazor プロジェクトに `Dignite.Abp.DynamicForms` NuGet パッケージをインストールします。

- [モジュールクラス](https://docs.abp.io/en/abp/latest/Module-Development-Basics) の `[DependsOn(...)]` 属性リストに `DigniteAbpDynamicFormsModule` を追加します。

## フォームのリストを取得

`IEnumerable<IForm>` を注入することで、ダイナミックフォームのリストを取得できます。以下はその例です。

```csharp
public class DemoAppService : ApplicationService
{
    private readonly IEnumerable<IForm> _forms;

    public DemoAppService(IEnumerable<IForm> forms)
    {
        _forms = forms;
    }

    public async Task<ListResultDto<FormDto>> GetFormsAsync()
    {
        return await Task.FromResult(
            new ListResultDto<FormDto>(
                _forms.Select(f => new FormDto(f.Name, f.DisplayName, f.FormType))
                    .ToList()
            )
        );
    }
}
```

## 単一のダイナミックフォームを取得

名前で指定したダイナミックフォームを取得するには、`IFormSelector` インターフェースを注入します。以下はその例です。

```csharp
public class DemoAppService : ApplicationService
{
    private readonly IFormSelector _formSelector;

    public DemoAppService(IFormSelector formSelector)
    {
        _formSelector = formSelector;
    }

    public IForm Get(string formName)
    {
        var form = _formSelector.Get(formName);
        return form;
    }
}
```

## 新しいダイナミックフォームを作成

新しいダイナミックフォームを作成するには、`FormBase` 抽象クラスを継承し、必要なメンバーを実装します。

- **Name**: ダイナミックフォームのユニークな名前です。英字または英字と数字を使用し、大文字と小文字を区別します。

- **DisplayName**: UI に表示されるダイナミックフォームのローカライズされた名前です。

- **FormType**: 列挙型 (`FormType.Simple` または `FormType.Complex`) で、単純なフォームか複雑なフォームかを指定します。

- **GetConfiguration**: ダイナミックフォームの構成を返すメソッドです。`FormConfigurationBase` を継承した構成クラスを作成する必要があります。

- **Validate**: フォームデータを検証するメソッドです。

以下は、シンプルなテキストボックスのダイナミックフォームの例です。

```csharp
public class SimpleTextboxForm : FormBase
{
    public const string ProviderName = "SimpleTextbox";

    public override string Name => ProviderName;

    public override string DisplayName => L["DisplayName:SimpleTextbox"];

    public override FormType FormType => FormType.Simple;

    public override void Validate(FormValidateArgs args)
    {
        var configuration = new SimpleTextboxConfiguration(args.Field.FormConfiguration);

        if (configuration.Required && (args.Value == null || args.Value.ToString().Length == 0))
        {
            args.ValidationErrors.Add(
                new System.ComponentModel.DataAnnotations.ValidationResult(
                    L["ValidateValue:Required", args.Field.DisplayName],
                    new[] { args.Field.Name }
                )
            );
        }

        if (args.Value != null && configuration.CharLimit < args.Value.ToString().Length)
        {
            args.ValidationErrors.Add(
                new System.ComponentModel.DataAnnotations.ValidationResult(
                    L["ValidateValue:CharacterCountExceedsLimit", args.Field.DisplayName, configuration.CharLimit],
                    new[] { args.Field.Name }
                )
            );
        }
    }



    public override FormConfigurationBase GetConfiguration(FormConfigurationData fieldConfiguration)
    {
        return new SimpleTextboxConfiguration(fieldConfiguration);
    }
}
```

シンプルなテキストボックスのダイナミックフォーム構成クラスの例です。

```csharp
public class SimpleTextboxConfiguration : FormConfigurationBase
{
    public string Placeholder
    {
        get => ConfigurationDictionary.GetConfigurationOrDefault<string>("Placeholder", null);
        set => ConfigurationDictionary.SetConfiguration("Placeholder", value);
    }

    public int CharLimit
    {
        get => ConfigurationDictionary.GetConfigurationOrDefault("CharLimit", 64);
        set => ConfigurationDictionary.SetConfiguration("CharLimit", value);
    }

    public SimpleTextboxConfiguration(FormConfigurationDictionary fieldConfiguration)
        : base(fieldConfiguration)
    {
    }

    public SimpleTextboxConfiguration()
        : base()
    {
    }
}
```

## フィールド情報の定義

カスタムフィールドクラスのデータ構造を作成するには、`ICustomizeFieldInfo` インターフェースを使用します。このインターフェースを継承してカスタムフィールドクラスを作成できます。以下はその例です。

- **Name**: カスタムフィールドのユニークな名前です。英字または英字と数字を使用し、大文字と小文字を区別します。

- **DisplayName**: カスタムフィールドの表示名です。

- **DefaultValue**: カスタムフィールドのデフォルト値です。

- **FormName**: カスタムフィールドが関連付けられているダイナミックフォームの名前です。

- **FormConfiguration**: カスタムフィールドが関連付けられているダイナミックフォームの構成です。

以下はカスタムフィールドクラスの例です。

```csharp
public class CustomizeFieldDemo : Entity<Guid>, ICustomizeFieldInfo
{
    public string Name { get; set; }

    public string DisplayName { get; set; }

    public string DefaultValue { get; set; }

    public string FormName { get; set; }

    public FormConfigurationDictionary FormConfiguration { get; set; }
}
```

## カスタムフィールドを持つビジネスオブジェクト

カスタムフィールドを持つビジネスオブジェクトクラスを作成するには、`IHasCustomFields` インターフェースを使用します。このインターフェースを継承してビジネスオブジェクトクラスを作成します。このインターフェースには、ビジネスオブジェクト内のさまざまなカスタムフィールドの値を取得または保存するための `CustomFields` プロパティが含まれています。

以下はカスタムフィールドを持つ製品クラスの例です。

```csharp
public class Product : FullAuditedAggregateRoot<Guid>, IHasCustomFields
{
    public Guid CategoryId { get; set; }

    public string Name { get; set; }

    public CustomFieldDictionary CustomFields { get; set; }
}
```

### `IHasCustomFields` の拡張メソッド

- **SetField**: ビジネスオブジェクト内のカスタムフィールドの値を設定します。

  ```csharp
  entry.SetField("title", "My Title");
  entry.SetField("content", "<p>Text content</p>");
  ```

- **GetField**: ビジネスオブジェクト内のカスタムフィールドの値を取得します。

  ```csharp
  var title = entry.GetField<string>("title");
  var content = entry.GetField<string>("content");
  ```

- **HasField**: ビジネスオブジェクト内のカスタムフィールドに指定した名前のフィールドが存在するかどうかをチェックします。

  ```csharp
  var isExists = entry.HasField("title");
  ```

- **RemoveField**: ビジネスオブジェクト内のカスタムフィールドから指定した名前のフィールドを削除します。

  ```csharp
  var isExists = entry.RemoveField("title");
  ```

- **SetDefaultsForCustomizeFields**: ビジネスオブジェクト内のカスタムフィールドディクショナリをチェックし、カスタムフィールドに値が設定されていない場合、`ICustomizeFieldInfo` から `DefaultValue` の値を取得してカスタムフィールドに値を設定します。

- **SetCustomizeFieldsToRegularProperties**: カスタムフィールドの値を、ビジネスオブジェクト内の同名のプロパティにマッピングします。

> [AutoMapper](https://automapper.org/) ライブラリを使用していて、マッピングプロファイルで `MapCustomizeFields()` メソッドを使用し、`mapToRegularProperties` パラメータを `true` に設定した場合、ビジネスオブジェクトの `SetCustomizeFieldsToRegularProperties` 拡張メソッドが自動的に呼び出されます。

### カスタムフィールドの値を検証

ビジネスオブジェクトがカスタムフィールドを持つ場合、ビジネスオブジェクトの DTO は `CustomizableObject` 抽象クラスを継承し、データ検証を行うために `GetFieldDefinitions` メソッドを実装します。このメソッドは、各プロジェクトの実際の状況に基づいてフィールドオブジェクトのリストを返す必要があります。

```csharp
public class Product

EditDto : CustomizableObject<CustomizeFieldDemo>
{
    public override IReadOnlyList<CustomizeFieldDemo> GetFieldDefinitions(ValidationContext validationContext)
    {
        // プロジェクトの実際の状況に基づいてフィールドオブジェクトのリストを取得します。
    }
}
```

### オブジェクト間のマッピング

- **MapCustomizeFieldsTo メソッド**: カスタムフィールドを制御された方法でオブジェクトからオブジェクトにコピーするための拡張メソッドです。以下はその例です。

  ```csharp
  entry.MapCustomizeFieldsTo(entryDto);
  ```

  `MapCustomizeFieldsTo` メソッドは両方の側面（この例では `Entry` と `EntryDto`）で `IHasCustomFields` インターフェースを実装する必要があります。

- カスタムフィールドのマッピングを指定する: デフォルトではすべてのカスタムフィールドが自動的にマッピングされます。`MapCustomizeFieldsTo` メソッドの `fields` パラメータを使用して、特定のカスタムフィールドをマッピングできます。

  ```csharp
  entry.MapCustomizeFieldsTo(
      entryDto,
      fields: new string[] { "FieldName1", "FieldName2" }
  );
  ```

  この例では、`entry` オブジェクトから `entryDto` オブジェクトに "FieldName1" と "FieldName2" のカスタムフィールドのみがマッピングされます。

- フィールドの無視: デフォルトではマッピングにカスタムフィールドを無視しません。`MapCustomizeFieldsTo` メソッドの `ignoredFields` パラメータを使用して、マッピング操作で特定のフィールドを無視できます。

  ```csharp
  identityUser.MapCustomizeFieldsTo(
      identityUserDto,
      ignoredFields: new[] { "FieldName2" }
  );
  ```

  無視されたカスタムフィールドは、対象のオブジェクトにコピーされません。

- AutoMapper との統合: [AutoMapper](https://automapper.org/) ライブラリを使用している場合、`Dignite.Abp.DynamicForms` は `MapCustomizeFieldsTo` メソッドを活用する拡張メソッドを提供します。この拡張メソッドは、マッピングプロファイル内で `MapCustomizeFields()` メソッドを使用します。

  ```csharp
  public class MyProfile : Profile
  {
      public MyProfile()
      {
          CreateMap<Entry, EntryDto>()
              .MapCustomizeFields();
      }
  }
  ```

  このメソッドは、`MapCustomizeFieldsTo()` メソッドと同じパラメータを持っています。

## 次のステップ

- [カスタムフィールド基本モジュール](Field-Customizing.md)

  このモジュールでは、カスタムフィールドを持つビジネスオブジェクトを素早く構築するための方法を提供します。

- [Blazor ダイナミックフォームコンポーネント](Blazor-Dynamic-Form-Components.md)

  Blazor ダイナミックフォームコンポーネントの開発方法について学びます。
  