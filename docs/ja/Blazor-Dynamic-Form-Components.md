# Blazorダイナミックフォームコンポーネント

本記事では、ユーザーデータとの対話を可能にするためのBlazorダイナミックフォームコンポーネントの開発方法について説明します。

ダイナミックフォームコンポーネントは、フォームの構成パラメータを設定するために使用されます。フォーム構成コンポーネント、フォームコントロールコンポーネント、フォームデータ表示コンポーネントの3つのカテゴリに分かれています。以下、それぞれについて説明します。

## インストール

* Blazorダイナミックフォームコンポーネントを開発するプロジェクトに `Dignite.Abp.DynamicForms.Components` NuGet パッケージをインストールします。

* [モジュールクラス](https://docs.abp.io/en/abp/latest/Module-Development-Basics) の `[DependsOn(...)]` プロパティリストに `AbpDynamicFormsComponentsModule` を追加します。

## フォーム構成コンポーネント

フォーム構成コンポーネントは、ダイナミックフォームのパラメータを設定するために使用されます。

`FormConfigurationComponentBase` クラスを継承してダイナミックフォームコンポーネントを作成します。以下はコードの例です：

```csharp
@using Dignite.Abp.DynamicForms.Textbox
@inherits FormConfigurationComponentBase<TextEditFormControl, TextEditConfiguration>

<Validation>
    <Field>
        <FieldLabel>@L["Placeholder"]</FieldLabel>
        <TextEdit @bind-Text="@FormConfiguration.Placeholder" />
    </Field>
</Validation>
<Field>
    <FieldLabel>@L["TextEditMode"]</FieldLabel>
    <RadioGroup TValue="TextEditMode" Name="textboxMode" @bind-CheckedValue="@FormConfiguration.Mode">
        <Radio TValue="TextEditMode" Value="@TextEditMode.SingleLine">@L["SingleLine"]</Radio>
        <Radio TValue="TextEditMode" Value="@TextEditMode.MultipleLine">@L["MultipleLine"]</Radio>
    </RadioGroup>
</Field>
<Field>
    <FieldLabel>@L["CharLimit"]</FieldLabel>
    <NumericEdit @bind-Value="@FormConfiguration.CharLimit" />
</Field>
```

### IFormConfigurationComponentSelector インターフェース

このインターフェースは `IFormConfigurationComponent Get(string formControlName)` メソッドを提供し、このメソッドを使用してダイナミックフォームの構成コンポーネントを取得できます。

`IFormConfigurationComponentSelector` インターフェースを注入することで、指定されたダイナミックフォーム名の `IFormConfigurationComponent` インスタンスを取得できます。例：

```csharp
@inject IFormConfigurationComponentSelector ConfigurationComponentSelector
@code{
    var component = configurationComponentSelector.Get("TextEdit");
}
```

## フォームコントロールコンポーネント

フォームコントロールコンポーネントは、システムとユーザー間のデータ相互作用に使用されます。

`FormControlComponentBase` クラスを継承してダイナミックフォームコンポーネントを作成します。以下はコードの例です：

```csharp
@using Dignite.Abp.DynamicForms.Textbox
@inherits FormControlComponentBase<TextEditFormControl, TextEditConfiguration, string>

<Validation Validator="@ValidateIsRequired">
    <Field>
        <FieldLabel>@Field.DisplayName</FieldLabel>
        <FieldBody>
            @if (FormConfiguration.Mode == TextEditMode.SingleLine)
            {
                <TextEdit Placeholder="@FormConfiguration.Placeholder" MaxLength="@FormConfiguration.CharLimit" Text="@Field.Value?.ToString()" TextChanged="@ChangeValueAsync">
                    <Feedback>
                        <ValidationError />
                    </Feedback>
                </TextEdit>
            }
            else
            {
                <MemoEdit Rows="5" AutoSize Placeholder="@FormConfiguration.Placeholder" MaxLength="@FormConfiguration.CharLimit" Text="@Field.Value?.ToString()" TextChanged="@ChangeValueAsync">
                    <Feedback>
                        <ValidationError />
                    </Feedback>
                </MemoEdit>
            }
            <FieldHelp>@Field.Description</FieldHelp>
        </FieldBody>
    </Field>
</Validation>
@code{
    void ValidateIsRequired(ValidatorEventArgs e)
    {
        if (Field.Required)
        {
            var value = e.Value == null ? string.Empty : Convert.ToString(e.Value);
            e.Status = string.IsNullOrWhiteSpace(value) ? ValidationStatus.Error : ValidationStatus.Success;
        }
    }
}
```

### IFormControlComponentSelector インターフェース

このインターフェースは `IFormControlComponent Get(string formControlName)` メソッドを提供し、このメソッドを使用してダイナミックフォームのコントロールコンポーネントを取得できます。

`IFormControlComponentSelector` インターフェースを注入することで、指定されたダイナミックフォーム名の `IFormControlComponent` インスタンスを取得できます。例：

```csharp
@inject IFormControlComponentSelector FormComponentSelector
@code{
    var component = FormComponentSelector.Get("TextEdit");
}
```

## フォームデータ表示コンポーネント

フォームデータ表示コンポーネントは、UI上でフォームデータを表示するために使用されます。

`FormViewComponentBase` クラスを継承してダイナミックフォームコンポーネントを作成します。以下はコードの例です：

```csharp
@using Dignite.Abp.DynamicForms.Textbox
@inherits FormViewComponentBase<TextEditFormControl, TextEditConfiguration>

<Field>
    <FieldLabel>@Field.DisplayName</FieldLabel>
    <FieldBody>
        @Field.Value?.ToString()
    </FieldBody>
</Field>
```

### IFormViewComponentSelector インターフェース

このインターフェースは `

IFormViewComponent Get(string formControlName)` メソッドを提供し、このメソッドを使用してダイナミックフォームのビューコンポーネントを取得できます。

`IFormViewComponentSelector` インターフェースを注入することで、指定されたダイナミックフォーム名の `IFormViewComponent` インスタンスを取得できます。例：

```csharp
@inject IFormViewComponentSelector FieldComponentSelector
@code{
    var component = FieldComponentSelector.Get("TextEdit");
}
```
