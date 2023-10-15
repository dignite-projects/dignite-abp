using System.Threading.Tasks;
using RulesEngine.Models;

namespace Dignite.Abp.Points;

/// <summary>
/// Points Manager
/// </summary>
public interface IPointsManager
{
    /// <summary>
    /// Calculate points according to specified points workflow rules
    /// </summary>
    /// <param name="pointsDefinitionName"></param>
    /// <param name="workflowName"></param>
    /// <param name="reSettings"></param>
    /// <param name="input"></param>
    /// <returns>Returns the result of calculating the points</returns>
    Task<int> PointsCalculationAsync(string pointsDefinitionName, string workflowName, ReSettings reSettings = null, object input = null);

    /// <summary>
    /// Calculate points according to specified points workflow rules
    /// </summary>
    /// <param name="pointsDefinitionName"></param>
    /// <param name="workflowName"></param>
    /// <param name="reSettings"></param>
    /// <param name="ruleParams">A variable member of rule parameters</param>
    /// <returns>Returns the result of calculating the points</returns>
    Task<int> PointsCalculationAsync(string pointsDefinitionName, string workflowName, ReSettings reSettings = null, params RuleParameter[] ruleParams);
}