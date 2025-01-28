using Dignite.Abp.Data;
using Volo.Abp.Data;

namespace Dignite.Abp.DynamicForms.Matrix;

/// <summary>
///
/// </summary>
public class MatrixBlock : IHasCustomFields
{
    public MatrixBlock()
    {
        this.ExtraProperties = new ExtraPropertyDictionary();
    }
    public MatrixBlock(string matrixBlockTypeName):this()
    {
        MatrixBlockTypeName = matrixBlockTypeName;
    }

    public string MatrixBlockTypeName { get; set; }

    public ExtraPropertyDictionary ExtraProperties { get; set; }
}