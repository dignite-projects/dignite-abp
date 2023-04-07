using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Dignite.Abp.DynamicForms.Matrix;

public class MatrixConfiguration : FormConfigurationBase
{
    [Required]
    public List<MatrixBlockType> MatrixBlockTypes {
        get => ConfigurationDictionary.GetConfigurationOrDefault(MatrixConfigurationNames.MatrixBlockTypes, new List<MatrixBlockType>());
        set => ConfigurationDictionary.SetConfiguration(MatrixConfigurationNames.MatrixBlockTypes, value);
    }

    public MatrixConfiguration(FormConfigurationDictionary fieldConfiguration)
        : base(fieldConfiguration)
    {
    }

    public MatrixConfiguration() : base()
    {
    }
}