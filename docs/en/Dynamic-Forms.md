# Dynamic Forms

`Dignite.Abp.DynamicForms` defines a set of forms for managing extension data of objects dynamically at runtime. It provides a common specification for retrieving, storing, and managing extension data, which is commonly used in systems with custom fields such as product SKUs, online surveys, CMS, and more.

`Dignite.Abp.DynamicForms` vs. [Volo.Abp.ObjectExtending](https://docs.abp.io/en/abp/latest/Object-Extensions):

- `[Volo.Abp.ObjectExtending]` extends objects with additional properties programmatically during development.
- `Dignite.Abp.DynamicForms` allows administrators to define fields for objects dynamically during runtime.

`Dignite.Abp.DynamicForms` consists of two main parts:

1. Form Engine:
   `Dignite.Abp.DynamicForms` provides various simple form controls like textboxes, dropdowns, switches, date pickers, file uploads, numeric inputs, as well as complex forms like tables and matrices.

2. Custom Fields:
   You can use custom fields to fetch or set data for dynamic forms.

> Dynamic forms are typically used in conjunction with business objects that have custom fields. We recommend building business objects based on the [Custom Field Basic Module](Field-Customizing.md).

## Installation

To get started with `Dignite.Abp.DynamicForms`, follow these steps:

- Install the `Dignite.Abp.DynamicForms` Nuget package into the `Domain.Shared` project of the Abp project.

- Add `DigniteAbpDynamicFormsModule` to the `[DependsOn(...)]` attribute list in your [module class](https://docs.abp.io/en/abp/latest/Module-Development-Basics).

## Getting a List of Forms

You can retrieve a list of dynamic forms by injecting `IEnumerable<IForm>`. Here's an example:

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

## Getting a Single Dynamic Form

To get a specific dynamic form by its name, inject the `IFormSelector` interface. Here's an example:

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

## Creating a New Dynamic Form

To create a new dynamic form, inherit from the `FormBase` abstract class and implement the required members:

- **Name**: The unique name of the dynamic form, using letters or letters with numbers, and it is case-sensitive.
  
- **DisplayName**: The localized name of the dynamic form, which is displayed in the UI.

- **FormType**: An enumeration (`FormType.Simple` or `FormType.Complex`) specifying whether it's a simple or complex type form.

- **GetConfiguration**: A method that returns the configuration for the dynamic form. You need to create a configuration class that derives from `FormConfigurationBase`.

- **Validate**: A method to validate the form data.

Here's an example of creating a simple textbox dynamic form:

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

Here's an example of the `SimpleTextboxConfiguration` class for the configuration:

```csharp
public class SimpleTextboxConfiguration : FormConfigurationBase
{
    public

 string Placeholder
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

## Defining Field Information

Use the `ICustomizeFieldInfo` interface to create data structures for custom field classes. You can create custom field classes by inheriting from this interface. It includes the following properties:

- **Name**: The unique name of the custom field, using letters or letters with numbers, and it is case-sensitive.
  
- **DisplayName**: The display name of the custom field.

- **DefaultValue**: The default value for the custom field.

- **FormName**: The name of the dynamic form associated with the custom field.

- **FormConfiguration**: The configuration for the dynamic form associated with the custom field.

Here's an example of a custom field class:

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

## Business Objects with Custom Fields

Use the `IHasCustomFields` interface to create business object classes that have custom fields. Inherit from this interface to create your business object classes. This interface includes a `CustomFields` property that allows you to retrieve or store values for various custom fields in the business object.

Here's an example of a product class with custom fields:

```csharp
public class Product : FullAuditedAggregateRoot<Guid>, IHasCustomFields
{
    public Guid CategoryId { get; set; }

    public string Name { get; set; }

    public CustomFieldDictionary CustomFields { get; set; }
}
```

### Extension Methods for `IHasCustomFields`

- **SetField**: Sets the value of a custom field in the business object.

  ```csharp
  entry.SetField("title", "My Title");
  entry.SetField("content", "<p>Text content</p>");
  ```

- **GetField**: Retrieves the value of a custom field in the business object.

  ```csharp
  var title = entry.GetField<string>("title");
  var content = entry.GetField<string>("content");
  ```

- **HasField**: Checks if a custom field with the specified name exists in the business object's custom fields.

  ```csharp
  var isExists = entry.HasField("title");
  ```

- **RemoveField**: Removes a custom field with the specified name from the business object's custom fields.

  ```csharp
  var isExists = entry.RemoveField("title");
  ```

- **SetDefaultsForCustomizeFields**: Checks the custom fields dictionary of the business object and sets default values from `ICustomizeFieldInfo` for custom fields that don't have values.

- **SetCustomizeFieldsToRegularProperties**: Maps the custom field values to properties with matching names in the business object.

> If you are using the [AutoMapper](https://automapper.org/) library and have configured mappings with the `MapCustomizeFields()` method in your mapping profile with the `mapToRegularProperties` parameter set to `true`, it will automatically call the `SetCustomizeFieldsToRegularProperties` extension method for your business objects.

### Validating Custom Field Values

When creating DTOs for business objects that have custom fields, inherit from the `CustomizableObject` abstract class and implement the `GetFieldDefinitions` method to perform data validation. This method should return a list of field objects based on the actual status of each project.

```csharp
public class ProductEditDto : CustomizableObject<CustomizeFieldDemo>
{
    public override IReadOnlyList<CustomizeFieldDemo> GetFieldDefinitions(ValidationContext validationContext)
    {
        // Obtain a list of field objects based on the actual status of each project.
    }
}
```

### Object-to-Object Mapping

- **MapCustomizeFieldsTo Method**: This extension method for `IHasCustomFields` allows you to copy custom fields from one object to another in a controlled manner. For example:

  ```csharp
  entry.MapCustomizeFieldsTo(entryDto);
  ```

  Both the source and target objects (in this example, `Entry` and `EntryDto`) must implement the `IHasCustomFields` interface.

- **Specifying Custom Field Mapping**: By default, all custom fields are mapped automatically. However, you can specify which custom fields to map by using the `fields` parameter in the `MapCustomizeFieldsTo` method:

  ```csharp
  entry.MapCustomizeFieldsTo(
      entryDto,
      fields: new[] {"FieldName1", "FieldName2"}
  );
  ```

  In the above code, only "FieldName1" and "FieldName2" custom fields from the `entry` object will be mapped to the `entryDto` object.

- **Ignoring Fields**: By default, no custom fields are ignored during mapping. You can ignore specific custom fields during mapping using the `ignoredFields` parameter in the `MapCustomizeFieldsTo` method:

  ```csharp
  identityUser.MapCustomizeFieldsTo(
      identityUserDto,
      ignoredFields: new[] {"FieldName2"}
  );
  ```

  Ignored custom fields will not be copied to the target object.

- **AutoMapper Integration**: If you are using the [AutoMapper](https://automapper.org/) library, `Dignite.Abp.DynamicForms` also provides an extension method to leverage the `MapCustomizeFieldsTo` method defined above. You can use the `MapCustomizeFields()` method in your mapping configuration:

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

  This method has the same parameters as the `MapCustomizeFieldsTo()` method.

## Further Reading

- [Custom Field Basic Module](Field-Customizing.md)

  This module provides a set of methods to help you quickly build business objects with custom fields.

- [Blazor Dynamic Form Components](Blazor-Dynamic-Form-Components.md)

  Learn how to develop Blazor dynamic form components.
