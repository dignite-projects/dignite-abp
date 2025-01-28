using System;
using System.Collections.Generic;

namespace Dignite.Abp.DynamicForms.Matrix;
public class MatrixBlockType
{
    public MatrixBlockType()
    {
        this.Fields = new List<FormField>();
    }

    public string DisplayName { get; set; }

    public string Name { get; set; }

    public IList<FormField> Fields { get; set; }
}