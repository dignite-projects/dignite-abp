# 动态表单

`Dignite.Abp.DynamicForms` 定义了一组在程序运行时可动态管理对象扩展数据的表单，常应用于商品SKU、在线调研、CMS等含有自定义字段的系统。

`Dignite.Abp.DynamicForms` 提供了文本框、下拉菜单、富文本编辑、日期选择、文件上传、数字框等类型表单。

`Dignite.Abp.DynamicForms` 与 [Volo.Abp.ObjectExtending](https://docs.abp.io/zh-Hans/abp/latest/Object-Extensions) 的区别：

- `Volo.Abp.ObjectExtending` 在开发系统阶段，由软件开发人员以编程方式扩展对象额外属性；
- `Dignite.Abp.DynamicForms` 在系统运行期间，由后台管理员自定义对象的字段；

## 安装

- 将 `Dignite.Abp.DynamicForms` Nuget 包安装到 Abp项目的 `Domain.Shared` 项目中

- 添加 `DigniteAbpDynamicFormsModule` 到 [模块类](https://docs.abp.io/en/abp/latest/Module-Development-Basics) `[DependsOn(...)]` 属性列表中。

## 获取指定名称的动态表单

通过注入 `IFormControlSelector` 接口，获取指定表单名称的 `IFormControl` 实例。

````csharp
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
````

## 开发一个新动态表单

继承 `FormControlBase` 抽象类，创建一个动态表单类：

- Name

    动态表单的唯一名称，使用字母或字母+数字方式命名，注意区分大小写。

- DisplayName

    动态表单的本地化字符，用于在UI上显示。

- GetConfiguration 方法

    返回获取动态表单配置的方法。
    以下代码为获取 `TextEdit` 动态表单配置的参考代码：

    ```csharp
    public override FormConfigurationBase GetConfiguration(FormConfigurationDictionary fieldConfiguration)
    {
        return new TextEditConfiguration(fieldConfiguration);
    }
    ```

- Validate 方法

    验证表单数据有效性的方法。

### 创建动态表单示例

`SimpleTextboxFormControl` 动态表单类：

> 动态表单类名必须是以`FormControl`结尾。

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

`SimpleTextboxConfiguration` 动态表单配置类：

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

## 后续阅读

- [Blazor 动态表单组件](Blazor-Dynamic-Form-Components.md)

    教你如何开发 Blazor 动态表单组件。

## 动态表单的最佳范例

- [Dignite Cms](https://dignite.com/dignite-cms)
