using Dignite.Abp.DynamicForms.Textbox;
using Shouldly;
using Xunit;

namespace Dignite.Abp.DynamicForms;

public class FormProviderSelector_Tests : DynamicFormsTestBase
{
    private readonly IFormProviderSelector _selector;

    public FormProviderSelector_Tests()
    {
        _selector = GetRequiredService<IFormProviderSelector>();
    }

    [Fact]
    public void Should_Select_Textbox_Field_Provider()
    {
        _selector.Get(TextboxFormProvider.ProviderName).ShouldBeAssignableTo<TextboxFormProvider>();
    }
}