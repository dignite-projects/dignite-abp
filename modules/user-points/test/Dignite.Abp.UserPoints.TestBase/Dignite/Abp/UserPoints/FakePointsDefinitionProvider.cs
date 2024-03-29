﻿using Dignite.Abp.Points;
using RulesEngine.Models;

namespace Dignite.Abp.UserPoints;
public class FakePointsDefinitionProvider : PointsDefinitionProvider
{
    private readonly UserPointsTestData _testData;

    public FakePointsDefinitionProvider(UserPointsTestData testData)
    {
        _testData = testData;
    }

    public override void Define(IPointsDefinitionContext context)
    {
        Rule authRule = new Rule();
        authRule.RuleName = "CheckAuthenticated";
        authRule.Expression = "input1.Authenticated == true";
        authRule.Actions = new RuleActions();
        authRule.Actions.OnSuccess = new ActionInfo
        {
            Name = "OutputExpression",
            Context = new System.Collections.Generic.Dictionary<string, object>
            {
                { "Expression","10"}
            }
        };

        Rule ageRule = new Rule();
        ageRule.RuleName = "CheckAge";
        ageRule.Expression = "input2.Age >= 18";
        ageRule.Actions = new RuleActions();
        ageRule.Actions.OnSuccess = new ActionInfo
        {
            Name = "OutputExpression",
            Context = new System.Collections.Generic.Dictionary<string, object>
            {
                { "Expression","5"}
            }
        };

        context.Add(
            new PointsDefinition(
                _testData.PointsDefinitionName,
                null, null,
                new PointsWorkflow(
                    _testData.PointsWorkflow1Name, null, null, null,
                    authRule,
                    ageRule)
                ));
    }
}