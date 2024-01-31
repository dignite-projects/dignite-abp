# Blazor Dynamic Form Components

This article will introduce how to develop interactive Blazor dynamic form components for user data.

Dynamic form components are divided into three categories: form configuration components, form control components, and form data display components. Let's explore each one.

## Installation

* Install the `Dignite.Abp.DynamicForms.Components` NuGet package into the project where you are developing the Blazor dynamic form components.

* Add `DigniteAbpDynamicFormsComponentsModule` to the `[DependsOn(...)]` property list of the [module class](https://docs.abp.io/en/abp/latest/Module-Development-Basics).

## Form Configuration Components

Form configuration components are used to configure the parameters of dynamic forms.

Inherit from the `FormConfigurationComponentBase` class to create a dynamic form component. Here's an example of the code:

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

### IFormConfigurationComponentSelector Interface

This interface provides the `IFormConfigurationComponent Get(string formControlName)` method, which allows you to obtain the configuration component for a dynamic form.

By injecting the `IFormConfigurationComponentSelector` interface, you can get an instance of `IFormConfigurationComponent` for a specified dynamic form name. Example:

```csharp
@inject IFormConfigurationComponentSelector ConfigurationComponentSelector
@code{
    var component = configurationComponentSelector.Get("TextEdit");
}
```

## Form Control Components

Form control components are used for data interaction between the system and users.

Inherit from the `FormControlComponentBase` class to create a dynamic form component. Here's an example of the code:

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

### IFormControlComponentSelector Interface

This interface provides the `IFormControlComponent Get(string formControlName)` method, which allows you to obtain the control component for a dynamic form.

By injecting the `IFormControlComponentSelector` interface, you can get an instance of `IFormControlComponent` for a specified dynamic form name. Example:

```csharp
@inject IFormControlComponentSelector FormComponentSelector
@code{
    var component = FormComponentSelector.Get("TextEdit");
}
```

## Form Data Display Components

Form data display components are used to present form data on the UI.

Inherit from the `FormViewComponentBase` class to create a dynamic form component. Here's an example of the code:

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

### IFormViewComponentSelector Interface

This interface provides the `IFormViewComponent Get(string formControlName)` method, which allows you to obtain the view component for a dynamic form.

By injecting the `IFormViewComponentSelector` interface, you can get an instance of `IFormViewComponent` for a specified dynamic form name. Example:

```csharp
@inject IFormViewComponentSelector FieldComponentSelector
@code{
    var component = FieldComponentSelector.Get("TextEdit");
}
```
