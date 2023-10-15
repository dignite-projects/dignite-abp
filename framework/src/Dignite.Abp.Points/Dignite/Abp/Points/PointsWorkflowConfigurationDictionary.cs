using System;
using System.Collections.Generic;

namespace Dignite.Abp.Points;

/// <summary>
/// Configuration Item Dictionary for Dynamic Forms
/// </summary>
[Serializable]
public class PointsWorkflowConfigurationDictionary : Dictionary<string, string>
{
    public PointsWorkflowConfigurationDictionary()
    {
    }

    public PointsWorkflowConfigurationDictionary(IDictionary<string, string> dictionary)
        : base(dictionary)
    {
    }
}