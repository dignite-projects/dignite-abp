using System.Collections.Generic;

namespace Dignite.Abp.DynamicForms.Matrix;
public class MatrixBlockType
{
    public MatrixBlockType()
    {
        this.FieldDefinitions = new List<MatrixBlockFieldDefinition>();
    }

    public string DisplayName { get; set; }

    public string Name { get; set; }

    public IList<MatrixBlockFieldDefinition> FieldDefinitions { get; set; }
}
