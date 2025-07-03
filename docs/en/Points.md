# Points Core

Points serve as a system to reward users for their active participation and contributions in specific activities. These points systems can enhance user engagement, foster loyalty, and motivate users to actively participate and contribute. Points systems find applications across various domains such as e-commerce, social media, gaming, education, and healthcare systems.

Dignite Abp Points leverages the Microsoft [RulesEngine](https://github.com/microsoft/RulesEngine) open-source project to define and calculate points based on custom rules. For a detailed understanding of Microsoft RulesEngine usage, you can refer to both the official Microsoft documentation and the sample project that I have created, available at [RulesEngineSample](https://github.com/duguankui/code-samples/tree/main/RulesEngineSample).

## Installation

To start using Dignite Abp Points, follow these steps:

1. Install the `Dignite.Abp.Points` NuGet package in your Domain project.
2. Add `AbpPointsModule` to the `[DependsOn(...)]` attribute list in your module class.

If you have already installed the [User Points module](User-Points.md), you do not need to install the `Dignite.Abp.Points` module separately.

## Defining Points

Before utilizing points, it is essential to define them. Different Abp modules can have distinct points workflows, and each workflow can encompass multiple points rules.

A points definition includes the following details:

- **Name:** The unique identifier for the points.
- **DisplayName (Optional):** A user-friendly name for the points.
- **Description (Optional):** A friendly description of the points.
- **PointsWorkflow:** This is defined according to the [Microsoft RulesEngine documentation](https://github.com/microsoft/RulesEngine).
  - **Name:** A unique name for the points workflow.
  - **DisplayName (Optional):** A name displayed to users.
  - **Description (Optional):** A friendly description of the points workflow.
  - **GlobalParams (Optional):** Refer to [Microsoft RulesEngine GlobalParams](https://github.com/microsoft/RulesEngine/blob/main/docs/index.md#globalparams).
  - **Rules:** Points rules, supporting multiple rules linked together. Rules can be defined using the method outlined in [Microsoft RulesEngine Basic Usage](https://github.com/microsoft/RulesEngine/blob/main/docs/index.md#basic-usage).
  - **Configuration:** Custom configuration settings for the points workflow, with several extension methods to assist developers in defining the configuration.

### Points Definition Sample Code

```csharp
public class TestPointsDefinitionProvider : PointsDefinitionProvider
{
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
        ageRule.Actions.OnSuccess = an ActionInfo
        {
            Name = "OutputExpression",
            Context = new System.Collections.Generic.Dictionary<string, object>
            {
                { "Expression","5"}
            }
        };

        context.Add(
            new PointsDefinition(
                "test-points-definition", 
                null, null, 
                new PointsWorkflow(
                    "test-points-workflow", null, null, null, 
                    authRule, 
                    ageRule)
                ));
    }
}
```

ABP will automatically discover and register points definitions.

## Managing Points Definitions

The `IPointsDefinitionManager` interface offers a set of methods for managing points definitions, including:

- Retrieving a points definition by name:

  ```csharp
  PointsDefinition Get(string name)
  ```

- Obtaining a list of all points definitions:

  ```csharp
  IReadOnlyList<PointsDefinition> GetAll()
  ```

## Calculating Points

The `IPointsManager` interface provides two methods for calculating points:

- Calculating points using the `RuleParameter` parameters:

  ```csharp
  Task<int> CalculatePointsAsync(string pointsDefinitionName, string workflowName, ReSettings reSettings = null, params RuleParameter[] ruleParams)
  ```
  
  - **pointsDefinitionName:** The name of the points definition.
  - **workflowName:** The name of the points workflow.
  - **reSettings:** Refer to [ReSettings](https://github.com/microsoft/RulesEngine/wiki/Getting-Started#resettings) for usage details.
  - **ruleParams:** Information about RuleParameter can be found in [RuleParameter](https://github.com/microsoft/RulesEngine/wiki/Getting-Started#ruleparameter).

This approach is recommended when multiple rules are involved.

- Calculating points using `object` parameters:

  ```csharp
  Task<int> CalculatePointsAsync(string pointsDefinitionName, string workflowName, ReSettings reSettings = null, object input = null)
  ```
  
  - **pointsDefinitionName:** The name of the points definition.
  - **workflowName:** The name of the points workflow.
  - **reSettings:** Refer to [ReSettings](https://github.com/microsoft/RulesEngine/wiki/Getting-Started#resettings) for usage details.
  - **input:** The data object used in the points rules.

This method is recommended for simpler rule logic.

### Calculating Points Sample Code

```csharp
public async Task TestCalculatePointsAsync()
{
    var input1 = new RuleParameter("input1", new {
        Authenticated = true
    });
    var input2 = new RuleParameter("input2", new {
        Age = 16
    });

    var points = await _pointsManager.CalculatePointsAsync(
        "test-points-definition", 
        "test-points-workflow", 
        null,
        input1, input2
        );

    // The value of points is 10
}

```

## Points Workflow Storage

In addition to defining points workflows in the `PointsDefinitionProvider`, you can store points workflows in `JSON` format in any medium.

The `IPointsWorkflowStore` interface provides methods for reading points workflows. This module includes a default `IPointsWorkflowStore` implementation named `NullPointsWorkflowStore`, which returns null for all points workflows, allowing you to utilize the points workflows defined by developers.
