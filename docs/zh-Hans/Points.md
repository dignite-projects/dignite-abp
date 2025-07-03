# 积分核心

积分是一种用于奖励用户在特定活动中的积极参与和贡献的系统,这些积分能够增强用户参与度、建立忠诚度、激励用户积极参与和贡献，可广泛应用于电子商务、社交媒体、游戏、教育和健康等系统中。

Dignite Abp Points 积分规则使用 Microsoft [RulesEngine](https://github.com/microsoft/RulesEngine) 开源项目，关于 Microsoft RulesEngine 的使用方法，除了参考官方文档外，还可以参考我写的示例项目，地址是[RulesEngineSample](https://github.com/duguankui/code-samples/tree/main/RulesEngineSample)。

## 安装

要开始使用Dignite Abp Points，首先需要执行以下步骤：

1. 在您的 Domain 项目中安装 `Dignite.Abp.Points` NuGet 包。
2. 在您的模块类的 `[DependsOn(...)]` 属性列表中添加 `AbpPointsModule`。

如果您已经安装了[用户积分模块](User-Points.md)，则无需单独安装 `Dignite.Abp.Points` 模块。

## 定义积分

使用积分之前需要定义积分，不同的Abp模块可以拥有不同的积分工作流,每个积分工作流下可多写多个积分规则。

积分定义包括以下信息：

- Name：积分的唯一名称。
- DisplayName（可选）：用于显示的积分名称。
- Description（可选）：积分的友好说明。
- PointsWorkflow：积分工作流，参见[Microsoft RulesEngine文档](https://github.com/microsoft/RulesEngine/blob/main/docs/index.md)
  - Name：积分工作流的唯一名称。
  - DisplayName（可选）：用于显示的积分工作流名称。
  - Description（可选）：积分工作流的友好说明。
  - GlobalParams（可选）：参见[Microsoft RulesEngine GlobalParams](https://github.com/microsoft/RulesEngine/blob/main/docs/index.md#globalparams)。
  - Rules : 积分的规则，支持多个积分串联，定义方法参见[Microsoft RulesEngine Basic Usage](https://github.com/microsoft/RulesEngine/blob/main/docs/index.md#basic-usage)
  - Configuration： 积分工作流的其他自定义配置表，有多个扩展方法帮忙开发者定义配置。

### 积分定义示例代码

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

ABP会自动发现并注册积分的定义。

## 管理积分定义

`IPointsDefinitionManager` 接口提供了一系列方法积分定义的操作，包括：

- 获取指定名称的积分定义:

  ```csharp
  PointsDefinition Get(string name)
  ```

- 获取所有的积分定义:

  ```csharp
  IReadOnlyList<PointsDefinition> GetAll()
  ```

## 计算积分

`IPointsManager` 接口提供了两个计算积分的方法：

- 使用`RuleParameter`参数计算积分:

  ```csharp
  Task<int> CalculatePointsAsync(string pointsDefinitionName, string workflowName, ReSettings reSettings = null, params RuleParameter[] ruleParams)
  ```
  
  - pointsDefinitionName：订阅通知的用户ID。
  - workflowName：订阅通知的名称。
  - reSettings：使用说明参见[ReSettings](https://github.com/microsoft/RulesEngine/wiki/Getting-Started#resettings)。
  - ruleParams：关于RuleParameter的说明参见[RuleParameter](https://github.com/microsoft/RulesEngine/wiki/Getting-Started#ruleparameter)。

  当有多个规则时，我们推荐使用这个方法。

- 使用`object`参数计算积分:

  ```csharp
  Task<int> CalculatePointsAsync(string pointsDefinitionName, string workflowName, ReSettings reSettings = null, object input = null)
  ```
  
  - pointsDefinitionName：订阅通知的用户ID。
  - workflowName：订阅通知的名称。
  - reSettings：使用说明参见[ReSettings](https://github.com/microsoft/RulesEngine/wiki/Getting-Started#resettings)。
  - input：积分规则中使用的数据对象。

  当使用比较简单的规则逻辑时，我们推荐使用这个方法。

### 计算积分示例代码

```csharp
public async Task TestCalculatePointsAsync()
{
    var input1 = new RuleParameter("input1",new {
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

## 积分工作流存储

开发者除了可以在`PointsDefinitionProvider`中定义积分流规则以外，可以将积分流规则以`JSON`的格式存储在任意的媒介中。

`IPointsWorkflowStore`接口提供了读取积分流规则的方法，本模块提供了默认的`IPointsWorkflowStore`实现`NullPointsWorkflowStore`,它为所有积分流规则返回null，即使用开发者定义的积分流。
