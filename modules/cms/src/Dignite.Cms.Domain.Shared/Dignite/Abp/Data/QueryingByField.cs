namespace Dignite.Abp.Data;
public class QueryingByField
{
    public QueryingByField()
    {
    }

    public QueryingByField(string name, string value)
    {
        Name = name;
        Value = value;
    }

    /// <summary>
    /// <see cref="DynamicForms.FormField.Name"/>
    /// </summary>
    public string Name { get; set; }

    public string Value { get; set; }
}
