# Blazor 动态表单组件

本文将介绍如何开发与用户数据交互的 Blazor 动态表单组件。

动态表单组件分为三个部分：表单配置组件、表单组件、表单数据组件，下面我们依次介绍。

## 安装

* 将 `Dignite.Abp.DynamicForms.Components` Nuget 包安装到即将开发 Blazor 动态表单组件的项目中。

* 添加 `DigniteAbpDynamicFormsComponentsModule` 到 [模块类](https://docs.abp.io/en/abp/latest/Module-Development-Basics) `[DependsOn(...)]`属性列表中。

## 表单配置组件

表单配置组件用于配置动态表单的参数。

继承 `ConfigurationComponentBase` 类创建一个动态表单组件，代码示例：

```csharp
@using Dignite.Abp.DynamicForms.CkEditor
@inherits ConfigurationComponentBase<CkEditorForm,CkEditorConfiguration>

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

### IConfigurationComponentSelector 接口

本接口提供了 `IConfigurationComponent Get(string formName)` 方法，可通过该方法获取动态表单的配置组件。

通过注入 `IConfigurationComponentSelector` 接口，获取指定动态表单名称的 `IConfigurationComponent` 实例，示例：

```csharp
@inject IConfigurationComponentSelector ConfigurationComponentSelector
@code{
    var component = configurationComponentSelector.Get("CkEditor");
}

```

## 表单组件

表单组件用于系统与用户之间的数据交互。

继承 `FormComponentBase` 类创建一个动态表单组件，代码示例：

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

### IFormComponentSelector 接口

本接口提供了 `IFormComponent Get(string formName)` 方法，可通过该方法获取动态表单的组件。

通过注入 `IFormComponentSelector` 接口，获取指定动态表单名称的 `IFormComponent` 实例，示例：

```csharp
@inject IFormComponentSelector FormComponentSelector
@code{
    var component = FormComponentSelector.Get("CkEditor");
}
```

## 表单数据组件

表单数据组件用于在UI上呈现表单数据。

继承 `FieldComponentBase` 类创建一个动态表单组件，代码示例：

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

### IFieldComponentSelector 接口

本接口提供了 `IFieldComponent Get(string formName)` 方法，可通过该方法获取动态表单的组件。

通过注入 `IFieldComponentSelector` 接口，获取指定动态表单名称的 `IFieldComponent` 实例，示例：

```csharp
@inject IFieldComponentSelector FieldComponentSelector
@code{
    var component = FieldComponentSelector.Get("CkEditor");
}
```

> 以上内容的介绍请参照 [CkEditor 动态表单](https://github.com/dignite-projects/dignite-abp/tree/main/modules/ckeditor-component/Dignite.Abp.DynamicForms.Components.CkEditor)源码
