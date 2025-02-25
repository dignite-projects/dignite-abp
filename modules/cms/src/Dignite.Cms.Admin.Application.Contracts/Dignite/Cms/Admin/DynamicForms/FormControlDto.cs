namespace Dignite.Cms.Admin.DynamicForms
{
    /// <summary>
    /// 
    /// </summary>
    public class FormControlDto
    {
        public FormControlDto(string name, string displayName, bool enableSearch)
        {
            Name = name;
            DisplayName = displayName;
            EnableSearch = enableSearch;
        }

        protected FormControlDto()
        {
        }


        public string Name { get; protected set; }

        public string DisplayName { get; protected set; }

        /// <summary>
        /// 
        /// </summary>
        public bool EnableSearch { get; set; }
    }
}
