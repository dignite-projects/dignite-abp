using System.ComponentModel.DataAnnotations;

namespace Dignite.Cms.Admin.Sections
{
    public class SectionRouteExistsInput
    {
        public SectionRouteExistsInput()
        {
        }

        public SectionRouteExistsInput(string route)
        {
            Route = route;
        }

        [Required]
        public string Route { get; set; }
    }
}
