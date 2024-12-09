namespace Dignite.CmsKit.Brand
{
    public class BrandDto
    {
        public BrandDto()
        {
        }

        public BrandDto(string name, string logo, string logoReverse, string icon)
        {
            Name = name;
            Logo = logo;
            LogoReverse = logoReverse;
            Icon = icon;
        }


        /// <summary>
        /// Name of the brand
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Logo of the brand
        /// </summary>
        public string? Logo { get; set; }

        /// <summary>
        /// LogoReverse of the brand
        /// </summary>
        public string? LogoReverse { get; set; }

        public string? Icon { get; set; }
    }
}

