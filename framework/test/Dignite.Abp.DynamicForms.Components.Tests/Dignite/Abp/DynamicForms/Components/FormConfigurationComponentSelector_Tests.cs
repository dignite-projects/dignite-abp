using Dignite.Abp.DynamicForms.Components.BlazoriseUI.Components.TextEdit;
using Dignite.Abp.DynamicForms.TextEdit;
using Shouldly;
using Xunit;

namespace Dignite.Abp.DynamicForms.Components;

public class FormConfigurationComponentSelector_Tests : FieldComponentsTestBase
{
    private readonly IFormConfigurationComponentSelector _selector;

    public FormConfigurationComponentSelector_Tests()
    {
        _selector = GetRequiredService<IFormConfigurationComponentSelector>();
    }

    [Fact]
    public void Should_Select_TextEdit_Component_Provider()
    {
        _selector.Get(TextEditFormControl.ControlName).ShouldBeAssignableTo<TextEditFormConfigurationComponent>();
    }
}