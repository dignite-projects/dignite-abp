# Blazor动态表单组件

本文将介绍如何开发与用户数据交互的Blazor动态表单组件。

动态表单组件分为三类：表单配置组件、表单控件组件、表单数据展示组件，下面我们依次介绍。

## 安装

* 将 `Dignite.Abp.DynamicForms.Components` NuGet 包安装到即将开发Blazor动态表单组件的项目中。

* 添加 `AbpDynamicFormsComponentsModule` 到 [模块类](https://docs.abp.io/en/abp/latest/Module-Development-Basics) 的`[DependsOn(...)]`属性列表中。

## 表单配置组件

表单配置组件用于配置动态表单的参数。

继承 `FormConfigurationComponentBase` 类创建一个动态表单组件，代码示例：

```csharp
@using Dignite.Abp.DynamicForms.Textbox
@inherits FormConfigurationComponentBase<TextEditFormControl,TextEditConfiguration>

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

### IFormConfigurationComponentSelector 接口

本接口提供了 `IFormConfigurationComponent Get(string formControlName)` 方法，可通过该方法获取动态表单的配置组件。

通过注入 `IFormConfigurationComponentSelector` 接口，获取指定动态表单名称的 `IFormConfigurationComponent` 实例，示例：

```csharp
@inject IFormConfigurationComponentSelector ConfigurationComponentSelector
@code{
    var component = configurationComponentSelector.Get("TextEdit");
}
```

## 表单控件组件

表单控件组件用于系统与用户之间的数据交互。

继承 `FormControlComponentBase` 类创建一个动态表单组件，代码示例：

```csharp
@using Dignite.Abp.DynamicForms.Textbox
@inherits FormControlComponentBase<TextEditFormControl,TextEditConfiguration,string>

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

### IFormControlComponentSelector 接口

本接口提供了 `IFormControlComponent Get(string formControlName)` 方法，可通过该方法获取动态表单的控件组件。

通过注入 `IFormControlComponentSelector` 接口，获取指定动态表单名称的 `IFormControlComponent` 实例，示例：

```csharp
@inject IFormControlComponentSelector FormComponentSelector
@code{
    var component = FormComponentSelector.Get("TextEdit");
}
```

## 表单数据展示组件

表单数据展示组件用于在UI上呈现表单数据。

继承 `FormViewComponentBase` 类创建一个动态表单组件，代码示例：

```csharp
@using Dignite.Abp.DynamicForms.Textbox
@inherits FormViewComponentBase<TextEditFormControl,TextEditConfiguration>

<Field>
    <FieldLabel>@Field.DisplayName</FieldLabel>
    <FieldBody>
        @Field.Value?.ToString()
    </FieldBody>
</Field>
```

### IFormViewComponentSelector 接口

本接口提供了 `IFormViewComponent Get(string formControlName)` 方法，可通过该方法获取动态表单的组件。

通过注入 `IFormViewComponentSelector` 接口，获取指定动态表单名称的 `IFormViewComponent` 实例，示例：

```csharp
@inject IFormViewComponentSelector FieldComponentSelector
@code{
    var component = FieldComponentSelector.Get("TextEdit");
}
```
