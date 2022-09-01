

namespace Dignite.Abp.FieldCustomizing.Fields
{
    public class SelectListItem
    {
        public SelectListItem()
        {
        }

        public SelectListItem(string text, string value, bool selected)
        {
            Text = text;
            Value = value;
            Selected = selected;
        }

        public string Text { get; set; }

        public string Value { get; set; }

        public bool Selected { get; set; }
    }
}
