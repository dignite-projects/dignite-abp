# 动态表单

`Dignite.Abp.DynamicForms` 定义了一组在程序运行时可动态管理对象扩展数据的表单，以及获取、存储对象扩展数据的通用规范，常应用于商品SKU、在线调研、CMS等含有自定义字段的系统。

`Dignite.Abp.DynamicForms` 与 [Volo.Abp.ObjectExtending](https://docs.abp.io/zh-Hans/abp/latest/Object-Extensions) 的区别：

- `Volo.Abp.ObjectExtending` 在程序开发阶段，由开发人员以编程方式扩展对象额外属性；
- `Dignite.Abp.DynamicForms` 在程序运行期间，由管理员自定义对象的字段；

`Dignite.Abp.DynamicForms` 由两部分组成：

1. 表单引擎
   `Dignite.Abp.DynamicForms` 提供了文本框、下拉菜单、富文本编辑、日期选择、文件上传、数字框等简单类型表单，以及表格、矩阵等复杂类型表单。

2. 自定义字段
   通过自定义字段获取或设置动态表单的数据。

> 动态表单需要配合带有自定义字段的业务对象一起使用，我们推荐基于 [自定义字段基础模块](Field-Customizing.md) 构建业务对象。

## 安装

- 将 `Dignite.Abp.DynamicForms` Nuget 包安装到 Abp项目的 `Domain.Shared` 项目中

- 添加 `DigniteAbpDynamicFormsModule` 到 [模块类](https://docs.abp.io/en/abp/latest/Module-Development-Basics) `[DependsOn(...)]` 属性列表中。

## 获取表单列表

通过注入 `IEnumerable<IForm>` 可获取动态表单的列表，参考代码如下：

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
            _forms.Select(
                f=>new FormDto(f.Name,f.DisplayName,f.FormType)
                ).ToList()
                ));
    }
}
```

## 获取单个动态表单

通过注入 `IFormSelector` 接口，获取指定表单名称的 `IForm` 实例。

````csharp
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
````

## 开发一个新动态表单

继承 `FormBase` 抽象类，创建一个动态表单类：

- Name

    动态表单的唯一名称，使用字母或字母+数字方式命名，注意区分大小写。

- DisplayName

    动态表单的本地化字符，用于在UI上显示。

- FormType 枚举
  - FormType.Simple ：文本框、下拉菜单、开关、日期选择等动态表单为简单类型表单
  - FormType.Complex ：表格、矩阵等包含 `FormType.Simple` 类型动态表单为复合类型表单

- GetConfiguration 方法

    返回获取动态表单配置的方法，获取 `Textbox` 动态表单配置的参考代码：

    ```csharp
    public override FormConfigurationBase GetConfiguration(FormConfigurationDictionary fieldConfiguration)
    {
        return new TextboxConfiguration(fieldConfiguration);
    }
    ```

- Validate 方法

    验证表单数据有效性的方法。

### 新建动态表单示例

`SimpleTextboxForm` 动态表单类：

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

        if (configuration.Required && (args.Value == null || args.Value.ToString().Length==0))
        {
            args.ValidationErrors.Add(
                new System.ComponentModel.DataAnnotations.ValidationResult(
                    L["ValidateValue:Required", args.Field.DisplayName],
                    new[] { args.Field.Name }
                    ));
        }

        if (args.Value != null && configuration.CharLimit < args.Value.ToString().Length)
        {
            args.ValidationErrors.Add(
                new System.ComponentModel.DataAnnotations.ValidationResult(
                    L["ValidateValue:CharacterCountExceedsLimit",args.Field.DisplayName, configuration.CharLimit],
                    new[] { args.Field.Name }
                    ));
        }
    }

    public override FormConfigurationBase GetConfiguration(FormConfigurationData fieldConfiguration)
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

## 定义字段信息

`ICustomizeFieldInfo` 接口约束自定义字段类的数据结构，开发者通过继承该接口创建自定义字段类。

- Name

    自定义字段的唯一名称，使用字母或字母+数字方式命名，注意区分大小写。

- DisplayName

    自定义字段的显示名称。

- DefaultValue

    自定义字段的默认值。

- FormName

    自定义字段对应的动态表单的名称。

- FormConfiguration

    自定义字段对应动态表单的配置。

### 创建自定义字段类示例

````csharp
public class CustomizeFieldDemo : Entity<Guid>, ICustomizeFieldInfo
{
    /// <summary>
    /// Field Unique Name
    /// </summary>
    public string Name { get; set; }

    public string DisplayName { get; set; }

    public string DefaultValue { get; set; }



    /// <summary>
    /// Field <see cref="IForm.Name"/>
    /// </summary>
    public string FormName { get; set; }

    public FormConfigurationDictionary FormConfiguration { get; set; }
}
````

## 含有自定义字段的业务对象

`IHasCustomFields` 接口约束带有自定义字段的业务对象类，开发者继承该接口创建业务对象类，本接口有一个 `CustomFields` 属性，用于获取或存储该业务对象中各个自定义字段的值。

继承该接口创建一个产品类示例：

```csharp
public class Product : FullAuditedAggregateRoot<Guid>, IHasCustomFields
{
    /// <summary>
    /// Product Cateogry Id
    /// </summary>
    public Guid CategoryId { get; set; }

    /// <summary>
    /// Product Name
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Product Sku Info
    /// </summary>
    public CustomFieldDictionary CustomFields { get; set; }
}
```

### `IHasCustomFields` 接口的扩展方法

- SetField

    设置业务对象的自定义字段值:

    ````csharp
    entry.SetField("title", "My Title");
    entry.SetField("content", "<p>Text content</p>");
    ````

- GetField

    获取业务对象的自定义字段值:

    ````csharp
    var title = entry.GetField<string>("title");
    var content = entry.GetField<string>("content");
    ````

- HasField

    判断业务对象的自定义字段中是否含有指定名称的字段。

    ````csharp
    var isExists = entry.HasField("title");
    ````

- RemoveField

    从业务对象的自定义字段中中删除指定名称的字段。

    ````csharp
    var isExists = entry.RemoveField("title");
    ````

- SetDefaultsForCustomizeFields

    检查业务对象的自定义字段字典表，当未设置业务对象的自定义字段值时，该方法将从 `ICustomizeFieldInfo` 中读取 `DefaultValue` 值，并为自定义字段赋值。

- SetCustomizeFieldsToRegularProperties

    将自定义字段值映射给业务对象中同名的字段。

> 如果使用的是 [AutoMapper](https://automapper.org/) 库，同时在映射配置文件中使用了 `MapCustomizeFields()` 方法，参数 `mapToRegularProperties` 值设定为 `true`，程序将自动调用业务对象的 `SetCustomizeFieldsToRegularProperties` 扩展方法。

### 业务对象自定义字段值的验证

业务对象的 `Dto` 继承自 `CustomizableObject` 抽象类，实现抽象类中 `GetFieldDefinitions` 方法，进行数据验证。

````csharp
public class ProductEditDto : CustomizableObject<CustomizeFieldDemo>
{
    ///<summary>
    /// Get the definition information of the field for data validation
    ///</summary>
    public override IReadOnlyList<CustomizeFieldDemo> GetFieldDefinitions(ValidationContext validationContext)
    {
        // Obtain a list of field objects based on the actual status of each project
    }
}
````

### 对象到对象映射

- MapCustomizeFieldsTo 方法

    `MapCustomizeFieldsTo` 是 `IHasCustomFields` 的扩展方法，用于以受控方式将自定义字段从一个对象复制到另一个对象. 示例:

    ````csharp
    entry.MapCustomizeFieldsTo(entryDto);
    ````

    `MapCustomizeFieldsTo` 需要在**两侧**(本例中是 `Entry` 和 `EntryDto`)实现 `IHasCustomFields` 接口。

- 指定自定义字段映射

    默认情况下自动映射全部自定义字段，通过 `MapCustomizeFieldsTo` 中 `fields` 参数，可以映射指定的自定义字段，如下所示：

    ````csharp
    entry.MapCustomizeFieldsTo(
        entryDto,
        fields: string[]{"FieldName1","FieldName2"}
    );
    ````

    以上代码示例 `entry` 对象仅向 `entryDto` 对象映射 "FieldName1"、"FieldName1" 两个自定义字段。

- 忽略字段

    默认情况下不会忽略任何自定义字段的映射，通过 `MapCustomizeFieldsTo` 中 `ignoredFields` 参数，可以在映射操作中忽略某些字段:

    ````csharp
    identityUser.MapCustomizeFieldsTo(
        identityUserDto,
        ignoredFields: new[] {"FieldName2"}
    );
    ````

    忽略的自定义字段不会复制到目标对象。

- AutoMapper集成

    如果你使用的是 [AutoMapper](https://automapper.org/) 库，`Dignite.Abp.DynamicForms` 还提供了一种扩展方法来利用上面定义的 `MapCustomizeFieldsTo` 方法.

    你可以在映射配置文件中使用 `MapCustomizeFields()` 方法.

    ````csharp
    public class MyProfile : Profile
    {
        public MyProfile()
        {
            CreateMap<Entry, EntryDto>()
                .MapCustomizeFields();
        }
    }
    ````

    它与 `MapCustomizeFieldsTo()` 方法具有相同的参数。

## 后续阅读

- [自定义字段基础模块](Field-Customizing.md)

    本模块提供一系列方法，帮助您快速的构建含有自定义字段的业务对象。

- [Blazor 动态表单组件](Blazor-Dynamic-Form-Components.md)

    教你如何开发 Blazor 动态表单组件。
