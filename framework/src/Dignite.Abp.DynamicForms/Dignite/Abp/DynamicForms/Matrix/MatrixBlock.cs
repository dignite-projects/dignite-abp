namespace Dignite.Abp.DynamicForms.Matrix;

/// <summary>
///
/// </summary>
public class MatrixBlock : IHasCustomFields
{
    public MatrixBlock()
    {
        this.CustomFields = new CustomFieldDictionary();
    }
    public MatrixBlock(string matrixBlockTypeName):this()
    {
        MatrixBlockTypeName = matrixBlockTypeName;
    }

    public string MatrixBlockTypeName { get; set; }

    public CustomFieldDictionary CustomFields { get; set; }
}