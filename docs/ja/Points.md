# ポイントコア

ポイントは、特定のアクティビティに積極的に参加し貢献するユーザーに報酬を与えるシステムとして機能します。これらのポイントシステムは、ユーザーのエンゲージメントを高め、ロイヤルティを醸成し、ユーザーに積極的な参加と貢献を奨励します。ポイントシステムは、電子商取引、ソーシャルメディア、ゲーム、教育、ヘルスケアシステムなど、さまざまな領域で活用されています。

Dignite Abp Pointsは、ポイントをカスタムルールに基づいて定義および計算するために、Microsoft [RulesEngine](https://github.com/microsoft/RulesEngine)のオープンソースプロジェクトを活用しています。Microsoft RulesEngineの詳細な使用方法については、公式Microsoftドキュメンテーションだけでなく、私が作成したサンプルプロジェクトも参照できます。[RulesEngineSample](https://github.com/duguankui/code-samples/tree/main/RulesEngineSample)でご利用いただけます。

## インストール

Dignite Abp Pointsを使用し始めるには、次の手順に従ってください：

1. ドメインプロジェクトに`Dignite.Abp.Points` NuGetパッケージをインストールします。
2. モジュールクラスの`[DependsOn(...)]`属性リストに`AbpPointsModule`を追加します。

すでに[ユーザーポイントモジュール](User-Points.md)をインストールしている場合、`Dignite.Abp.Points`モジュールを個別にインストールする必要はありません。

## ポイントの定義

ポイントを利用する前に、それらを定義することが重要です。異なるAbpモジュールでは異なるポイントワークフローを持つことができ、各ワークフローは複数のポイントルールを包括することができます。

ポイントの定義には、以下の詳細情報が含まれます：

- **名前:** ポイントのユニークな識別子。
- **表示名（オプション):** ポイントのユーザーフレンドリーな名前。
- **説明（オプション):** ポイントのフレンドリーな説明。
- **ポイントワークフロー:** これは[Microsoft RulesEngineドキュメンテーション](https://github.com/microsoft/RulesEngine)に従って定義されます。
  - **名前:** ポイントワークフローのユニークな名前。
  - **表示名（オプション):** ユーザーに表示される名前。
  - **説明（オプション):** ポイントワークフローのフレンドリーな説明。
  - **GlobalParams（オプション):** [Microsoft RulesEngine GlobalParams](https://github.com/microsoft/RulesEngine/blob/main/docs/index.md#globalparams)を参照してください。
  - **ルール:** 複数のルールをサポートし、ルールは[Microsoft RulesEngine Basic Usage](https://github.com/microsoft/RulesEngine/blob/main/docs/index.md#basic-usage)で説明されている方法を使用して定義できます。
  - **設定:** ポイントワークフローのためのカスタム設定設定です。開発者が設定を定義するのを支援するいくつかの拡張メソッドがあります。

### ポイントの定義のサンプルコード

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

ABPはポイントの定義を自動的に検出および登録します。

## ポイントの定義の管理

`IPointsDefinitionManager`インターフェースは、ポイントの定義の操作に関する一連のメソッドを提供しており、以下を含みます：

- 名前を指定してポイントの定義を取得する：

  ```csharp
  PointsDefinition Get(string name)
  ```

- すべてのポイントの定義のリストを取得する：

  ```csharp
  IReadOnlyList<PointsDefinition> GetAll()
  ```

## ポイントの計算

`IPointsManager`インターフェースは、ポイントを計算するための2つのメソッドを提供して

います：

- `RuleParameter`パラメータを使用してポイントを計算：

  ```csharp
  Task<int> CalculatePointsAsync(string pointsDefinitionName, string workflowName, ReSettings reSettings = null, params RuleParameter[] ruleParams)
  ```
  
  - **pointsDefinitionName:** ポイントの名前。
  - **workflowName:** ポイントのワークフローの名前。
  - **reSettings:** 使用に関しては[ReSettings](https://github.com/microsoft/RulesEngine/wiki/Getting-Started#resettings)を参照してください。
  - **ruleParams:** RuleParameterに関する情報は[RuleParameter](https://github.com/microsoft/RulesEngine/wiki/Getting-Started#ruleparameter)で確認できます。

複数のルールが含まれている場合、この方法をお勧めします。

- `object`パラメータを使用してポイントを計算：

  ```csharp
  Task<int> CalculatePointsAsync(string pointsDefinitionName, string workflowName, ReSettings reSettings = null, object input = null)
  ```
  
  - **pointsDefinitionName:** ポイントの名前。
  - **workflowName:** ポイントのワークフローの名前。
  - **reSettings:** 使用に関しては[ReSettings](https://github.com/microsoft/RulesEngine/wiki/Getting-Started#resettings)を参照してください。
  - **input:** ポイントのルールで使用されるデータオブジェクト。

この方法は、より単純なルールロジックにおすすめです。

### ポイントの計算のサンプルコード

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

    // ポイントの値は10です
}

```

## ポイントワークフローストレージ

`PointsDefinitionProvider`でポイントワークフローを定義するだけでなく、ポイントワークフローを`JSON`形式で任意の媒体に保存することもできます。

`IPointsWorkflowStore`インターフェースは、ポイントワークフローを読み取るためのメソッドを提供しており、このモジュールには`NullPointsWorkflowStore`というデフォルトの`IPointsWorkflowStore`実装が含まれています。`NullPointsWorkflowStore`はすべてのポイントワークフローに対してnullを返し、開発者が定義したポイントワークフローを使用することを可能にします。
