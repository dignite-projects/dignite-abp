namespace Dignite.Abp.FieldCustomizing;
public class QueryingByFieldParameter
{
    public QueryingByFieldParameter()
    {
    }

    public QueryingByFieldParameter( string fieldName, string value)
    {
        FieldName = fieldName;
        Value = value;
    }


    public string FieldName { get; set; }

    public string Value { get; set; }
}
