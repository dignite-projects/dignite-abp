# Dynamic Forms

`Dignite.Abp.DynamicForms` defines a set of forms that can dynamically manage extended data objects at runtime, commonly used in systems with custom fields such as product SKUs, online surveys, CMS, etc.

`Dignite.Abp.DynamicForms` provides various types of forms, including text boxes, dropdown menus, rich text editors, date pickers, file uploads, numeric boxes, etc.

The difference between `Dignite.Abp.DynamicForms` and [Volo.Abp.ObjectExtending](https://docs.abp.io/zh-Hans/abp/latest/Object-Extensions) is as follows:

- `Volo.Abp.ObjectExtending`: During the development phase of the system, software developers programmatically extend objects with additional properties.
- `Dignite.Abp.DynamicForms`: During the runtime of the system, backend administrators customize the fields of objects.

## Installation

- Install the `Dignite.Abp.DynamicForms` NuGet package into the `Domain.Shared` project of the Abp project.

- Add `DigniteAbpDynamicFormsModule` to the `[DependsOn(...)]` property list of the [module class](https://docs.abp.io/en/abp/latest/Module-Development-Basics).

## Get a Dynamic Form with a Specific Name

Inject the `IFormControlSelector` interface to get an instance of `IFormControl` for a specific form name.

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

## Developing a New Dynamic Form

Inherit from the `FormControlBase` abstract class to create a dynamic form class:

- Name

    The unique name of the dynamic form, named using letters or letters+numbers, and case-sensitive.

- DisplayName

    The localized string for the dynamic form, used for display on the UI.

- GetConfiguration Method

    Returns the method to retrieve the configuration of the dynamic form.
    The following code is a reference code for obtaining the configuration of the `TextEdit` dynamic form:

    ```csharp
    public override FormConfigurationBase GetConfiguration(FormConfigurationDictionary fieldConfiguration)
    {
        return new TextEditConfiguration(fieldConfiguration);
    }
    ```

- Validate Method

    Method to validate the validity of form data.

### Creating a Dynamic Form Example

`SimpleTextboxFormControl` dynamic form class:

> The dynamic form class name must end with `FormControl`.

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

`SimpleTextboxConfiguration` dynamic form configuration class:

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

## Further Reading

- [Blazor Dynamic Form Components](Blazor-Dynamic-Form-Components.md)

    Teaches how to develop Blazor dynamic form components.
