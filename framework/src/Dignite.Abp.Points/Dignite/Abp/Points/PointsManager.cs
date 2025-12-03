using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RulesEngine.Models;
using Volo.Abp;
using Volo.Abp.DependencyInjection;

namespace Dignite.Abp.Points;

/// <summary>
/// Points Manager
/// </summary>
public class PointsManager : IPointsManager, ITransientDependency
{
    private readonly IPointsWorkflowStore _pointsWorkflowStore;

    private readonly IPointsDefinitionManager _pointsDefinitionManager;

    public PointsManager(IPointsWorkflowStore pointsWorkflowStore, IPointsDefinitionManager pointsDefinitionManager)
    {
        _pointsWorkflowStore = pointsWorkflowStore;
        _pointsDefinitionManager = pointsDefinitionManager;
    }

    /// <summary>
    /// Calculate points according to specified points workflow rules
    /// </summary>
    /// <param name="pointsDefinitionName"></param>
    /// <param name="workflowName"></param>
    /// <param name="input"></param>
    /// <param name="reSettings"></param>
    /// <returns>Returns the result of calculating the points</returns>
    public async Task<int> CalculatePointsAsync(string pointsDefinitionName, string workflowName, ReSettings reSettings = null, object input = null)
    {
        var re = await InstantiatedRulesEngineAsync(pointsDefinitionName, workflowName, reSettings);
        List<RuleResultTree> resultList = await re.ExecuteAllRulesAsync(workflowName, input);

        //Start calculating points
        return CalculatePointsWithRules(resultList);
    }

    /// <summary>
    /// Calculate points according to specified points workflow rules
    /// </summary>
    /// <param name="pointsDefinitionName"></param>
    /// <param name="workflowName"></param>
    /// <param name="reSettings"></param>
    /// <param name="ruleParams">A variable member of rule parameters</param>
    /// <returns>Returns the result of calculating the points</returns>
    public async Task<int> CalculatePointsAsync(string pointsDefinitionName, string workflowName, ReSettings reSettings = null, params RuleParameter[] ruleParams)
    {
        var re = await InstantiatedRulesEngineAsync(pointsDefinitionName, workflowName, reSettings);
        List<RuleResultTree> resultList = await re.ExecuteAllRulesAsync(workflowName, ruleParams);

        //Start calculating points
        return CalculatePointsWithRules(resultList);
    }

    private async Task<RulesEngine.RulesEngine> InstantiatedRulesEngineAsync(string pointsDefinitionName, string workflowName, ReSettings reSettings = null)
    {
        var pointsWorkflow = await _pointsWorkflowStore.GetOrNullAsync(pointsDefinitionName, workflowName);
        if (pointsWorkflow == null)
        {
            var pointsDefinition = _pointsDefinitionManager.Get(pointsDefinitionName);

            if (pointsDefinition == null)
            {
                throw new AbpException(
                    $"Could not find the points definition with the name ({pointsDefinitionName}) ."
                );
            }

            pointsWorkflow = pointsDefinition.Workflows.FirstOrDefault(wf => wf.Name == workflowName);

            if (pointsWorkflow == null)
            {
                throw new AbpException(
                    $"Could not find the workflow with the name ({workflowName}) ."
                );
            }
        }


        //Building a rule engine for calculating points
        var re = new RulesEngine.RulesEngine(reSettings);
        re.AddWorkflow(new Workflow
        {
            WorkflowName = pointsWorkflow.Name,
            GlobalParams = pointsWorkflow.GlobalParams,
            Rules = pointsWorkflow.Rules,
        });

        return re;
    }

    protected virtual int CalculatePointsWithRules(List<RuleResultTree> resultList)
    {
        var points = 0;
        foreach (var item in resultList)
        {
            if (item.IsSuccess && item.ActionResult != null && item.ActionResult.Output != null)
            {
                points += (int)item.ActionResult.Output;
            }
        }

        return points;
    }
}