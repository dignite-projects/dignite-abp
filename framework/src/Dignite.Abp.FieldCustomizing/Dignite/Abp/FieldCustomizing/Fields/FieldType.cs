

namespace Dignite.Abp.FieldCustomizing.Fields
{
    public enum FieldType
    {
        /// <summary>
        /// Simple type field control may not include other types of field controls
        /// </summary>
        Simple,

        /// <summary>
        /// Complex type field control can include simple type field controls
        /// </summary>
        Complex
    }
}
