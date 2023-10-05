# Blazor ダイナミックフォームコンポーネント

この記事では、ユーザーデータと対話するBlazorダイナミックフォームコンポーネントの開発方法について説明します。

ダイナミックフォームコンポーネントは、フォーム構成コンポーネント、フォームコンポーネント、フォームデータコンポーネントの3つの部分に分かれています。以下、それぞれを順番に紹介します。

## インストール

* Blazorダイナミックフォームコンポーネントを開発するプロジェクトに `Dignite.Abp.DynamicForms.Components` NuGet パッケージをインストールします。

* [モジュールクラス](https://docs.abp.io/en/abp/latest/Module-Development-Basics) の `[DependsOn(...)]` 属性リストに `DigniteAbpDynamicFormsComponentsModule` を追加します。

## フォーム構成コンポーネント

フォーム構成コンポーネントは、ダイナミックフォームのパラメータを構成するために使用されます。

`ConfigurationComponentBase` クラスを継承して、ダイナミックフォームコンポーネントを作成します。以下にコード例を示します：

```csharp
@using Dignite.Abp.DynamicForms.CkEditor
@inherits ConfigurationComponentBase<CkEditorForm, CkEditorConfiguration>

<Validation>
    <Field>
        <FieldLabel>@L["FieldDisplayName"]</FieldLabel>
        <TextEdit MaxLength="CustomizeFieldInfoConsts.MaxDisplayNameLength" @bind-Text="@Field.DisplayName" />
    </Field>
</Validation>
<Validation>
    <Field>
        <FieldLabel>@L["FieldName"]</FieldLabel>
        <TextEdit Pattern="@CustomizeFieldInfoConsts.NameRegularExpression" MaxLength="CustomizeFieldInfoConsts.MaxNameLength" @bind-Text="@Field.Name">
            <FieldHelp>@L["FieldNameHelpText"]</FieldHelp>
        </TextEdit>
    </Field>
</Validation>
<Validation>
    <Field>
        <FieldLabel>@L["Description"]</FieldLabel>
        <TextEdit @bind-Text="@FormConfiguration.Description" />
    </Field>
</Validation>
<Field>
    <Check TValue="bool" @bind-Checked="@FormConfiguration.Required">@L["IsRequired"]</Check>
</Field>
<Validation Validator="@ValidationRule.IsNotEmpty">
    <Field>
        <FieldLabel>@L["ImagesContainerName"]</FieldLabel>
        <TextEdit @bind-Text="@FormConfiguration.ImagesContainerName" />
    </Field>
</Validation>
<Validation>
    <Field>
        <FieldLabel>@L["InitialContent"]</FieldLabel>
        <TextEdit @bind-Text="@FormConfiguration.InitialContent" />
    </Field>
</Validation>
```

### IConfigurationComponentSelector インターフェース

このインターフェースは、指定されたダイナミックフォームの設定コンポーネントのインスタンスを取得できる `IConfigurationComponent Get(string formName)` メソッドを提供し、指定されたダイナミックフォーム名の `IConfigurationComponent` インスタンスを取得するために `IConfigurationComponentSelector` インターフェースをインジェクトして使用できます。以下は例です：

```csharp
@inject IConfigurationComponentSelector ConfigurationComponentSelector
@code{
    var component = configurationComponentSelector.Get("CkEditor");
}
```

## フォームコンポーネント

フォームコンポーネントは、システムとユーザー間のデータのやり取りに使用されます。

`FormComponentBase` クラスを継承してダイナミックフォームコンポーネントを作成します。以下にコード例を示します：

```csharp
@using Dignite.Abp.DynamicForms.CkEditor
@using Dignite.Abp.AspNetCore.Components.CkEditor
@inherits FormComponentBase<CkEditorForm,CkEditorConfiguration>

<Field Horizontal="@(!IsChild)">
    <FieldLabel ColumnSize="ColumnSize.Is2.OnDesktop">@Field.DisplayName</FieldLabel>
    <FieldBody ColumnSize="ColumnSize.Is10.OnDesktop">
        <CkEditor @bind-Content="Content" Options="Options" ImagesContainerName="@ImagesContainerName">
        </CkEditor>
        <FieldHelp>@FormConfiguration.Description</FieldHelp>
    </FieldBody>
</Field>

@code {
    private string _content;
    protected string Content
    {
        get
        {
            return _content;
        }
        set
        {
            _content = value;
            CustomizableObject.SetField(Field.Name, value);
        }
    }

   

 protected string ImagesContainerName
    {
        get
        {
            return FormConfiguration.ImagesContainerName;
        }
    } 

    protected CkEditorOptions Options { get; } = CkEditorOptions.Default;

    protected override void OnInitialized()
    {
        base.OnInitialized();
    }

    protected override void OnParametersSet()
    {
        base.OnParametersSet();
        _content = CustomizableObject.GetField(Field.Name, FormConfiguration.InitialContent)?.ToString();
    }
}
```

### IFormComponentSelector インターフェース

このインターフェースは、指定されたダイナミックフォームのフォームコンポーネントのインスタンスを取得できる `IFormComponent Get(string formName)` メソッドを提供し、指定されたダイナミックフォーム名の `IFormComponent` インスタンスを取得するために `IFormComponentSelector` インターフェースをインジェクトして使用できます。以下は例です：

```csharp
@inject IFormComponentSelector FormComponentSelector
@code{
    var component = FormComponentSelector.Get("CkEditor");
}
```

## フォームデータコンポーネント

フォームデータコンポーネントは、フォームデータをUI上で表示するために使用されます。

`FieldComponentBase` クラスを継承してダイナミックフォームコンポーネントを作成します。以下にコード例を示します：

```csharp
@using Dignite.Abp.DynamicForms.CkEditor
@inherits FieldComponentBase<CkEditorForm,CkEditorConfiguration>

<Field Horizontal="@(!IsChild)">
    <FieldLabel ColumnSize="ColumnSize.Is2.OnDesktop" hidden="@IsChild">@Field.DisplayName</FieldLabel>
    <FieldBody ColumnSize="ColumnSize.Is10.OnDesktop">
        <div class="content">
            @if (Value != null)
            {
                @((MarkupString)Value)
            }
        </div>
    </FieldBody>
</Field>

@code {
    private string Value { get; set; }

    protected override void OnInitialized()
    {
        base.OnInitialized();
        Value = CustomizableObject.GetField(Field.Name)?.ToString();
    }
}
```

### IFieldComponentSelector インターフェース

このインターフェースは、指定されたダイナミックフォームのフィールドコンポーネントのインスタンスを取得できる `IFieldComponent Get(string formName)` メソッドを提供し、指定されたダイナミックフォーム名の `IFieldComponent` インスタンスを取得するために `IFieldComponentSelector` インターフェースをインジェクトして使用できます。以下は例です：

```csharp
@inject IFieldComponentSelector FieldComponentSelector
@code{
    var component = FieldComponentSelector.Get("CkEditor");
}
```

> 詳細については、[CkEditor Dynamic Form](https://github.com/dignite-projects/dignite-abp/tree/main/modules/ckeditor-component/Dignite.Abp.DynamicForms.Components.CkEditor)のソースコードを参照してください。
