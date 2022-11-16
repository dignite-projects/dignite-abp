using Dignite.Abp.DynamicForms.Textbox;
using Shouldly;
using Xunit;

namespace Dignite.Abp.DynamicForms;

public class FormProviderSelector_Tests : DynamicFormsTestBase
{
    private readonly IFormSelector _selector;

    public FormProviderSelector_Tests()
    {
        _selector = GetRequiredService<IFormSelector>();
    }

    [Fact]
    public void Should_Select_Textbox_Field_Provider()
    {
        _selector.Get(TextboxForm.TextboxFormName).ShouldBeAssignableTo<TextboxForm>();
    }
}