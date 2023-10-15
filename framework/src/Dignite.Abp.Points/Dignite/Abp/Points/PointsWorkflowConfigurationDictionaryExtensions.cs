using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Dignite.Abp.Points;

public static class PointsWorkflowConfigurationDictionaryExtensions
{
    public static bool HasConfiguration(this PointsWorkflowConfigurationDictionary source, string name)
    {
        return source.ContainsKey(name);
    }

    public static TConfiguration GetConfigurationOrDefault<TConfiguration>(this PointsWorkflowConfigurationDictionary source, string name, TConfiguration defaultValue = default)
    {
        if (!source.HasConfiguration(name))
        {
            return defaultValue;
        }
        var configurationAsJson = source[name];
        var options = new JsonSerializerOptions();
        options.Converters.Add(new JsonStringEnumConverter());
        return JsonSerializer.Deserialize<TConfiguration>(configurationAsJson, options);
    }

    public static void SetConfiguration<TConfiguration>(
        this PointsWorkflowConfigurationDictionary source,
        string name,
        TConfiguration value)
    {
        var options = new JsonSerializerOptions
        {
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            WriteIndented = true
        };
        var configurationAsJson = JsonSerializer.Serialize(value, options);
        source[name] = configurationAsJson;
    }
}