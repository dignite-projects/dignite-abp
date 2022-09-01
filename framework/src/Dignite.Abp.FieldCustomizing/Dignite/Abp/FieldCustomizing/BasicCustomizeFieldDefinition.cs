using Dignite.Abp.FieldCustomizing.Fields;
using JetBrains.Annotations;
using System;
using System.ComponentModel.DataAnnotations;

namespace Dignite.Abp.FieldCustomizing
{
    [Serializable]
    public class BasicCustomizeFieldDefinition: ICustomizeFieldDefinition
    {

        public BasicCustomizeFieldDefinition(string name, string displayName, string fieldProviderName, string defaultValue, FieldConfigurationDictionary configuration)
        {
            Name = name;
            DisplayName = displayName;
            FieldProviderName=fieldProviderName;
            DefaultValue = defaultValue;
            Configuration = configuration;
        }

        [Required]
        [NotNull]
        [StringLength(BasicCustomizeFieldDefinitionConsts.MaxNameLength)]
        public string Name { get; set; }

        [Required]
        [NotNull]
        [StringLength(BasicCustomizeFieldDefinitionConsts.MaxDisplayNameLength)]
        public string DisplayName { get; set; }


        /// <summary>
        /// The provider to be used to <see cref="IFieldProvider.Name"/>
        /// </summary>
        [Required]
        [StringLength(BasicCustomizeFieldDefinitionConsts.MaxFieldProviderNameLength)]
        public string FieldProviderName { get; set; }


        /// <summary>
        /// Default value of the field.
        /// </summary>
        [CanBeNull]
        public string DefaultValue { get; set; }

        [NotNull]
        public FieldConfigurationDictionary Configuration { get; set; }
    }
}
