namespace Dignite.Cms.Admin.DynamicForms
{
    /// <summary>
    /// 
    /// </summary>
    public class FormControlDto
    {
        public FormControlDto(string name, string displayName)
        {
            Name = name;
            DisplayName = displayName;
        }

        protected FormControlDto()
        {
        }


        public string Name { get; protected set; }

        public string DisplayName { get; protected set; }
    }
}
