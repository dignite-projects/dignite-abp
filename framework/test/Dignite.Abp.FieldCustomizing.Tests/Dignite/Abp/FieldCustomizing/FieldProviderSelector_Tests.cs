using Dignite.Abp.FieldCustomizing.Fields;
using Dignite.Abp.FieldCustomizing.Fields.Textbox;
using Shouldly;
using Xunit;

namespace Dignite.Abp.FieldCustomizing;

public class FieldProviderSelector_Tests : FieldCustomizingTestBase
{
    private readonly IFieldProviderSelector _selector;

    public FieldProviderSelector_Tests()
    {
        _selector = GetRequiredService<IFieldProviderSelector>();
    }

    [Fact]
    public void Should_Select_Textbox_Field_Provider()
    {
        _selector.Get(TextboxFieldProvider.ProviderName).ShouldBeAssignableTo<TextboxFieldProvider>();
    }
}