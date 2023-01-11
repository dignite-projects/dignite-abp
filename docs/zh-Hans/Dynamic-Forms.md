# Dynamic Forms

## 介绍

`Dignite.Abp.DynamicForms`用于动态定义实体的字段表单，主要应用于商城系统中商品的SKU、问卷系统中的问题、CMS中的字段等系统。

`Dignite.Abp.DynamicForms`由两部分组成：

1. 表单引擎
    `Dignite.Abp.DynamicForms`提供了文本框、下拉菜单、富文本编辑、日期选择、文件上传、数字框等简单类型表单，以及表格、矩阵等复杂类型表单。
    此外，您可以基于接口创造更多的表单，满足更多的业务需求。详见[创建表单](#创建表单)

2. 自定义字段
    结合表单引擎和数据库持久化，在线定义业务对象的字段。
    `Dignite.Abp.DynamicForms`与`[Volo.Abp.ObjectExtending](https://docs.abp.io/zh-Hans/abp/latest/Object-Extensions)`的区别：
    - `Volo.Abp.ObjectExtending`是在程序开发阶段，由开发人员以编程方式扩展对象额外属性；
    - `Dignite.Abp.DynamicForms`自定义字段是在程序运行期间，由用户在线定义对象的属性；

## 安装

[Dignite.Abp.DynamicForms](https://www.nuget.org/packages/Dignite.Abp.DynamicForms)是 Dynamic Forms 的主包.

在 Abp 项目中安装 `Dignite.Abp.DynamicForms`NuGet包，然后添加`[DependsOn(typeof(AbpDynamicFormsModule))]`模块依赖。

## 表单引擎

### 创建表单

继承`FormBase`抽象类，创造表单，重写属性及方法如下：

#### Name

表单的唯一名称，使用字母或字母+数字方式命名，注意区分大小写。

#### DisplayName

表单的本地化字符，用于在UI上显示名称。

#### FormType

FormType的枚举值，FormType.Simple和FormType.Complex。
文本框、下拉菜单为FormType.Simple类型，表格、矩阵是FormType.Complex类型。

#### GetConfiguration方法

用于获取表单的配置类。

#### Validate方法

用于验证提交数据。

#### New Form Demo：

````csharp
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
````

如上，创造了一个新表单，`Dignite.Abp.DynamicForms`会自动发现并添加它，在你的程序中添加如下代码即可获取所有表单集合（虽然一般情况不需要这么做）：

````csharp
public class DemoAppService : ApplicationService
{
    protected IEnumerable<IForm> Forms { get; }
    public DemoAppService(IEnumerable<IForm> _forms)
    {
        Forms=_forms;
    }
}
````

#### 表单配置

继承 `FormConfigurationBase` 抽象类，构建表单配置，该抽象类具有以下两个属性：

- Required：表单值是否必填；

- Description：表单的说明文本；

> 以上两个属性是表单的通用属性，新建的表单不需要重写。

下面写一个简单文本框表单的配置：

````csharp
public class SimpleTextboxConfiguration:FormConfigurationBase
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

        public SimpleTextboxConfiguration() : base()
        {
        }
    }
````

### IFormSelector 

通过表单名称获取 `IForm` 实例。

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

## 自定义字段

### ICustomizeFieldInfo 

继承该接口定义字段实体。

#### Name

自定义字段的唯一名称，使用字母或字母+数字方式命名，注意区分大小写。

#### DisplayName

自定义字段的本地化字符，用于在UI上显示名称。

#### DefaultValue

自定义字段的默认值。

#### FormName

自定义字段应用表单的名称。

#### FormConfiguration

自定义字段应用表单的配置项。

````csharp
public class CustomizeFieldDefinitionDemo : Entity<Guid>, ICustomizeFieldInfo, IMultiTenant
{
    public Guid? TenantId { get; set; }

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

### IHasCustomFields

继承该接口定义自定义字段值的集合。含有 `CustomFields` 属性:

````csharp
CustomFieldDictionary CustomFields { get; }
````

`IHasCustomFields` 有一些实用的扩展方法：

#### SetField

用于设置自定义字段的值:

````csharp
entry.SetField("title", "My Title");
entry.SetField("content", "<p>Text content</p>");
````

#### GetField

用于读取自定义字段的值:

````csharp
var title = entry.GetField<string>("title");
var content = entry.GetField<string>("content");
````

- `GetField` 是一个泛型方法，对象类型做为泛型参数.
- 如果未设置过字段值，则返回默认值 (int 的默认值为 0 , bool 的默认值是 false ... 等).

> 如果自定义字段值不是原始类型(int,bool,枚举,字符串等),可以使用 GetField 的非泛型版本,它会返回 object.

#### HasField

用于检查对象是否含有字段。

#### RemoveField

用于从实体对象中删除字段。

#### SetDefaultsForCustomizeFields

当尚未设置字段值时，该方法将字段的值设置为`ICustomizeFieldInfo`中设定的`DefaultValue`。

#### SetCustomizeFieldsToRegularProperties

用于自动映射自定义字段的值。
> 如果你使用的是[AutoMapper](https://automapper.org/)库，同时在映射配置文件中使用 MapCustomizeFields() 方法，参数`mapToRegularProperties`值设定为`true`，程序将自动调用对象的`SetCustomizeFieldsToRegularProperties`扩展方法。


### 表单数据的验证

继承`CustomizableObject`抽象类实现表单数据验证，子类需要实现抽象类中`GetFieldDefinitions`方法。

````csharp
public class EntryEditDto: CustomizableObject<CustomizeFieldDefinitionDemoDto>
    {
        public EntryEditDto()
        {
            this.CustomizedFields = new CustomizeFieldDictionary();
        }

        ///<summary>
        /// Get the definition information of the field for data validation
        ///</summary>
        public override IReadOnlyList<BasicCustomizeFieldDefinition> GetFieldDefinitions(ValidationContext validationContext)
        {
            ......
        }
    }
````

### 对象到对象映射

#### MapCustomizeFieldsTo

`MapCustomizeFieldsTo` 是`IHasCustomFields`的扩展方法，用于以受控方式将自定义字段从一个对象复制到另一个对象. 示例:

````csharp
entry.MapCustomizeFieldsTo(entryDto);
````

`MapCustomizeFieldsTo` 需要在**两侧**(本例中是`Entry` 和 `EntryDto`)实现`IHasCustomFields`接口。

#### 指定映射自定义字段

默认情况下自动映射全部自定义字段，通过`MapCustomizeFieldsTo`中`fields`参数，可以映射指定的自定义字段，如下所示：

````csharp
entry.MapCustomizeFieldsTo(
    entryDto,
    fields: string[]{"FieldName1","FieldName2"}
);
````

以上代码示例`entry`对象仅向`entryDto`对象映射"FieldName1"、"FieldName1"两个自定义字段。

#### 忽略字段

默认情况下不会忽略任何自定义字段的映射，通过`MapCustomizeFieldsTo`中`ignoredFields`参数，可以在映射操作中忽略某些字段:

````csharp
identityUser.MapCustomizeFieldsTo(
    identityUserDto,
    ignoredFields: new[] {"FieldName2"}
);
````

忽略的自定义字段不会复制到目标对象。

#### AutoMapper集成

如果你使用的是[AutoMapper](https://automapper.org/)库，`Dignite.Abp.DynamicForms`还提供了一种扩展方法来利用上面定义的 `MapCustomizeFieldsTo` 方法.

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

它与 `MapCustomizeFieldsTo()` 方法具有相同的参数.


## See Also

- [FieldCustomizing Module](Field-Customizing.md)

提供一系列方法，帮助您快速的构建自定义字段。

- [Dignite.Cms Module](Cms.md)

基于[FieldCustomizing Module](Field-Customizing.md)开发的一套无头CMS。

-  [Dynamic Form Blazor Component](Dynamic-Form-Components.md)

教你如何开发 Dynamic Form Blazor Component。

