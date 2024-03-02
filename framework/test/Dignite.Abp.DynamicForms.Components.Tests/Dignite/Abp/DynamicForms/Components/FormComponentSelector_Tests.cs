using Dignite.Abp.DynamicForms.Components.BlazoriseUI.Components.TextEdit;
using Dignite.Abp.DynamicForms.TextEdit;
using Shouldly;
using Xunit;

namespace Dignite.Abp.DynamicForms.Components;

public class FormComponentSelector_Tests : FieldComponentsTestBase
{
    private readonly IFormControlComponentSelector _selector;

    public FormComponentSelector_Tests()
    {
        _selector = GetRequiredService<IFormControlComponentSelector>();
    }

    [Fact]
    public void Should_Select_TextEdit_Component_Provider()
    {
        _selector.Get(TextEditFormControl.ControlName).ShouldBeAssignableTo<TextEditFormControlComponent>();
    }
}