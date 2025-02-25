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
    /// The field name
    /// </summary>
    public string Name { get; set; }

    public string Value { get; set; }
}
