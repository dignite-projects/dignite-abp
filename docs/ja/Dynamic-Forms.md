# ダイナミックフォーム

`Dignite.Abp.DynamicForms` は、実行時に動的に管理可能なオブジェクト拡張データフォームのセットを定義し、通常は商品SKU、オンライン調査、CMSなど、カスタムフィールドを持つシステムに適用されます。

`Dignite.Abp.DynamicForms` は、テキストボックス、ドロップダウンメニュー、リッチテキストエディタ、日付選択、ファイルのアップロード、数値ボックスなどの種類のフォームを提供します。

`Dignite.Abp.DynamicForms` と [Volo.Abp.ObjectExtending](https://docs.abp.io/zh-Hans/abp/latest/Object-Extensions) の違い：

- `Volo.Abp.ObjectExtending` は、開発システムの段階で、ソフトウェア開発者がプログラムでオブジェクトを追加のプロパティで拡張するものです。
- `Dignite.Abp.DynamicForms` は、システムが実行されている期間中に、バックエンドの管理者がオブジェクトのフィールドをカスタマイズします。

## インストール

- `Dignite.Abp.DynamicForms` NuGet パッケージを Abp プロジェクトの `Domain.Shared` プロジェクトにインストールします。

- [モジュールクラス](https://docs.abp.io/en/abp/latest/Module-Development-Basics)の `[DependsOn(...)]` プロパティリストに `AbpDynamicFormsModule` を追加します。

## 特定の名前のダイナミックフォームを取得する

`IFormControlSelector` インターフェースを注入して、指定されたフォーム名の `IFormControl` インスタンスを取得します。

```csharp
public class DemoAppService : ApplicationService
{
    private readonly IFormControlSelector _formControlSelector;
    public DemoAppService(IFormControlSelector formControlSelector)
    {
        _formControlSelector = formControlSelector;
    }

    public IFormControl Get(string formControlName)
    {
        var formControl = _formControlSelector.Get(formControlName);
        return formControl;
    }
}
```

## 新しいダイナミックフォームを開発する

`FormControlBase` 抽象クラスを継承して、ダイナミックフォームクラスを作成します：

- Name

    ダイナミックフォームのユニークな名前。文字または文字+数字の形式で命名し、大文字と小文字を区別します。

- DisplayName

    UIに表示するためのダイナミックフォームのローカライズされた文字列。

- GetConfiguration メソッド

    ダイナミックフォームの構成を取得するメソッドを返します。
    以下のコードは `TextEdit` ダイナミックフォーム構成を取得するための参考コードです：

    ```csharp
    public override FormConfigurationBase GetConfiguration(FormConfigurationDictionary fieldConfiguration)
    {
        return new TextEditConfiguration(fieldConfiguration);
    }
    ```

- Validate メソッド

    フォームデータの妥当性を検証するメソッド。

### ダイナミックフォームの例

`SimpleTextboxFormControl` ダイナミックフォームクラス：

> ダイナミックフォームクラス名は `FormControl` で終わる必要があります。

```csharp
public class SimpleTextboxFormControl : FormControlBase
{
    public const string ControlName = "SimpleTextbox";

    public override string Name => ControlName;

    public override string DisplayName => L["DisplayName:SimpleTextbox"];

    public override void Validate(FormControlValidateArgs args)
    {
        var configuration = new SimpleTextboxConfiguration(args.Field.FormConfiguration);

        if (args.Field.Value != null && !args.Field.Value.ToString().IsNullOrWhiteSpace())
        {
            if (configuration.CharLimit < args.Field.Value.ToString().Length)
            {
                args.ValidationErrors.Add(
                    new System.ComponentModel.DataAnnotations.ValidationResult(
                        L["Validate:CharacterCountExceedsLimit", args.Field.DisplayName, configuration.CharLimit],
                        new[] { args.Field.Name }
                        ));
            }
        }
        else
        {
            if (args.Field.Required)
            {
                args.ValidationErrors.Add(
                    new System.ComponentModel.DataAnnotations.ValidationResult(
                        L["Validate:Required", args.Field.DisplayName],
                        new[] { args.Field.Name }
                        ));
            }
        }
    }

    public override FormConfigurationBase GetConfiguration(FormConfigurationDictionary fieldConfiguration)
    {
        return new SimpleTextboxConfiguration(fieldConfiguration);
    }
}
```

`SimpleTextboxConfiguration` ダイナミックフォーム構成クラス：

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

## 追加のリーディング

- [Blazor ダイナミックフォームコンポーネント](Blazor-Dynamic-Form-Components.md)

    Blazorダイナミックフォームコンポーネントの開発方法を教えます。

## 最良の例

- [Dignite Cms](https://dignite.com/dignite-cms)
