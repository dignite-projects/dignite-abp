using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using JetBrains.Annotations;
using RulesEngine.Models;
using Volo.Abp.Localization;

namespace Dignite.Abp.Points;

[Serializable]
public class PointsWorkflow
{
    public PointsWorkflow(string name, ILocalizableString displayName, ILocalizableString description = null, IList<ScopedParam> globalParams = null, params Rule[] rules)
    {
        Name = name;
        DisplayName = displayName;
        Description = description;
        GlobalParams = globalParams;
        Rules = rules;
        Configuration = new PointsWorkflowConfigurationDictionary();
    }

    /// <summary>
    /// Unique name of the points.
    /// </summary>
    public string Name { get; private set; }

    /// <summary>
    /// Display name of the points.
    /// Optional.
    /// </summary>
    [JsonIgnore]
    public ILocalizableString DisplayName { get; set; }

    /// <summary>
    /// Description for the points.
    /// Optional.
    /// </summary>
    [JsonIgnore]
    public ILocalizableString Description { get; set; }

    /// <summary>
    /// Gets or Sets the global params which will be applicable to all rules
    /// </summary>
    public IEnumerable<ScopedParam> GlobalParams { get; set; }

    /// <summary>
    /// list of rules.
    /// </summary>
    public IEnumerable<Rule> Rules { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [NotNull]
    public PointsWorkflowConfigurationDictionary Configuration { get; set; }
}
