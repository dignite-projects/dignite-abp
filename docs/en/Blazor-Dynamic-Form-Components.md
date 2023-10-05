# Blazor Dynamic Form Component

This article will introduce how to develop a Blazor dynamic form component that interacts with user data.

The dynamic form component consists of three parts: the form configuration component, the form component, and the form data component. Below, we will introduce them one by one.

## Installation

* Install the `Dignite.Abp.DynamicForms.Components` NuGet package in the project where you will be developing the Blazor dynamic form component.

* Add `DigniteAbpDynamicFormsComponentsModule` to the `[DependsOn(...)]` attribute list of your [module class](https://docs.abp.io/en/abp/latest/Module-Development-Basics).

## Form Configuration Component

The form configuration component is used to configure the parameters of the dynamic form.

Create a dynamic form component by inheriting the `ConfigurationComponentBase` class, as shown in the code example below:

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

### IConfigurationComponentSelector Interface

This interface provides the `IConfigurationComponent Get(string formName)` method, which allows you to obtain an instance of the configuration component for a specific dynamic form.

You can inject the `IConfigurationComponentSelector` interface and use it to get an `IConfigurationComponent` instance for a specified dynamic form name, as shown in the example below:

```csharp
@inject IConfigurationComponentSelector ConfigurationComponentSelector
@code{
    var component = configurationComponentSelector.Get("CkEditor");
}
```

## Form Component

The form component is used for data interaction between the system and users.

Create a dynamic form component by inheriting the `FormComponentBase` class, as shown in the code example below:

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

### IFormComponentSelector Interface

This interface provides the `IFormComponent Get(string formName)` method, which allows you to obtain an instance of the form component for a specific dynamic form.

You can inject the `IFormComponentSelector` interface and use it to get an `IFormComponent` instance for a specified dynamic form name, as shown in the example below:

```csharp
@inject IFormComponentSelector FormComponentSelector
@code{
    var component = FormComponentSelector.Get("CkEditor");
}
```

## Form Data Component

The form data component is used to display form data on the UI.

Create a dynamic form component by inheriting the `FieldComponentBase` class, as shown in the code example below:

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

### IFieldComponentSelector Interface

This interface provides the `IFieldComponent Get(string formName)` method, which allows you to obtain an instance of the field component for a specific dynamic form.

You can inject the `IFieldComponentSelector` interface and use it to get an `IFieldComponent` instance for a specified dynamic form name, as shown in the example below:

```csharp
@inject IFieldComponentSelector FieldComponentSelector
@code{
    var component = FieldComponentSelector.Get("CkEditor");
}
```

> For more information, please refer to the [CkEditor Dynamic Form](https://github.com/dignite-projects/dignite-abp/tree/main/modules/ckeditor-component/Dignite.Abp.DynamicForms.Components.CkEditor) source code.
