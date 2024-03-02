using Dignite.Abp.DynamicForms.TextEdit;
using Shouldly;
using Xunit;

namespace Dignite.Abp.DynamicForms;

public class FormControlSelector_Tests : DynamicFormsTestBase
{
    private readonly IFormControlSelector _selector;

    public FormControlSelector_Tests()
    {
        _selector = GetRequiredService<IFormControlSelector>();
    }

    [Fact]
    public void Should_Select_TextEdit_Field_Provider()
    {
        _selector.Get(TextEditFormControl.ControlName).ShouldBeAssignableTo<TextEditFormControl>();
    }
}