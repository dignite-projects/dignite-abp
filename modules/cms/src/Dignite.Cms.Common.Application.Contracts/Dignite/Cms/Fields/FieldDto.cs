using Dignite.Abp.DynamicForms;
using System;
using Volo.Abp.Application.Dtos;

namespace Dignite.Cms.Fields
{
    [Serializable]
    public class FieldDto: EntityDto<Guid>
    {
        public FieldDto()
        {
            FormConfiguration = new();
        }

        /// <summary>
        /// Field Unique Name
        /// </summary>
        public string Name { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Field <see cref="IFormControl.Name"/>
        /// </summary>
        public string FormControlName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public FormConfigurationDictionary FormConfiguration { get; set; }
        
    }
}
