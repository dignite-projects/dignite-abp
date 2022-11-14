using Dignite.Abp.FieldCustomizing.Forms;
using Dignite.Abp.FieldCustomizing.Forms.Textbox;
using Shouldly;
using Xunit;

namespace Dignite.Abp.FieldCustomizing;

public class FieldProviderSelector_Tests : FieldCustomizingTestBase
{
    private readonly IFormProviderSelector _selector;

    public FieldProviderSelector_Tests()
    {
        _selector = GetRequiredService<IFormProviderSelector>();
    }

    [Fact]
    public void Should_Select_Textbox_Field_Provider()
    {
        _selector.Get(TextboxFormProvider.ProviderName).ShouldBeAssignableTo<TextboxFormProvider>();
    }
}