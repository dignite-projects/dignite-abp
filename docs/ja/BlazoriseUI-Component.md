# BlazoriseUI コンポーネント

BlazoriseUI コンポーネントは、[Blazorise](https://blazorise.com) ベースの一連の Blazor コンポーネントで、ツリーコンポーネント、強化型 DataGrid などが含まれています。

## インストール

BlazoriseUI コンポーネントを ABP プロジェクトで使用するには、以下の手順を実行する必要があります：

1. **NuGet パッケージのインストール**: `Dignite.Abp.BlazoriseUI` NuGet パッケージを Blazor プロジェクトに追加します。このパッケージには、コンポーネントとリソースをアプリケーションで使用できるようにするために必要なすべてのものが含まれています。

2. **モジュールの統合**: プロジェクトの ABP モジュール依存リストに `[DependsOn(typeof(DigniteAbpBlazoriseUiModule))]` を追加します。これにより、コンポーネントがアプリケーションで正しく登録され、利用可能になります。

## AutoHeight コンポーネント

`AutoHeight` コンポーネントは、現在のウィンドウの高さに基づいて、画面の残りの領域を満たす `<div>` 要素を生成します。コンテンツの高さが `AutoHeight` コンポーネントの高さを超える場合、スクロールバーが表示されます。

### 基本的な使用法

```html
<AutoHeight>
    <div>これは内部要素です</div>
</AutoHeight>
```

### ExtraHeight を含む AutoHeight コンポーネント

`AutoHeight` コンポーネントに `ExtraHeight` プロパティを設定することで、`AutoHeight` コンポーネントの高さを減少させることができます。

```html
<AutoHeight ExtraHeight="45">
    <div>これは内部要素です</div>
</AutoHeight>
```

## ExtensibleDataGrid コンポーネント

`ExtensibleDataGrid` コンポーネントは、`[Blazorise DataGrid](https://blazorise.com/docs/extensions/datagrid/getting-started)` コンポーネントの使用を簡素化し、自動高さやプログラムによる `TableColumn` などの新しい機能を追加します。

### 基本的な使用法

```csharp
@inject ISiteAdminAppService SiteAdminAppService
@inherits AbpCrudPageBase<ISectionAdminAppService, SectionDto, Guid, GetSectionsInput, CreateSectionInput, UpdateSectionInput>
```

```html
<ExtensibleDataGrid TItem="SectionDto"
                    Data="@Entities"
                    ReadData="@OnDataGridReadAsync"
                    TotalItems="@TotalCount"
                    ShowPager="true"
                    PageSize="@PageSize"
                    CurrentPage="@CurrentPage"
                    Columns="@SectionManagementTableColumns">
</ExtensibleDataGrid>
```

```csharp
public partial class SectionManagement
{
    protected List<TableColumn> SectionManagementTableColumns => TableColumns.Get<SectionManagement>();
    
    protected override ValueTask SetTableColumnsAsync()
    {
        SectionManagementTableColumns
            .AddRange(new TableColumn[]
            {
                new TableColumn
                {
                    Title = L["DisplayName"],
                    Data = nameof(SectionDto.DisplayName)
                },
                new TableColumn
                {
                    Title = L["Name"],
                    Data = nameof(SectionDto.Name)
                },
                new TableColumn
                {
                    Title = L["Route"],
                    Data = nameof(SectionDto.Route)
                },
                new TableColumn
                {
                    Title = L["Template"],
                    Sortable = true,
                    Data = nameof(SectionDto.Template)
                }
            });

        return base.SetTableColumnsAsync();
    }
}
```

### SelectionMode

`SelectionMode` プロパティは `Multiple?DataGridSelectionMode.Single` と `Multiple?DataGridSelectionMode.Multiple` の2つの方法をサポートしており、デフォルト値は `Multiple?DataGridSelectionMode.Single` です。`SelectionMode` を `Multiple?DataGridSelectionMode.Multiple` に設定すると、行の前にチェックボックスが表示されます。

```html
<ExtensibleDataGrid TItem="SectionDto" @ref="DataGridRef"
                    SelectionMode="Multiple?DataGridSelectionMode.Multiple: DataGridSelectionMode.Single"
                    Data="Entities"
                    ReadData="OnDataGridReadAsync"
                    TotalItems="TotalCount"
                    ShowPager="true"
                    PageSize="PageSize"
                    CurrentPage="@CurrentPage"
                    Columns="@SectionManagementTableColumns">
</ExtensibleDataGrid>
<Button Color="Color.Primary" Clicked="SelectSectionsAsync">レコードを選択</Button>
```

```csharp
public partial class SectionManagement
{        
    protected virtual async Task SelectSectionsAsync()
    {
        var items = DataGridRef.SelectedItems;
    }
}
```

### API

以下は `ExtensibleDataGrid` コンポーネントのすべてのプロパティです：

- `TItem`: データ型を指定します。
- `Data`: データリストを指定します。
- `ReadData`: データリストを非同期に読み込むメソッドを指定します。ページが `AbpCrudPageBase` クラスを

継承している場合、`OnDataGridReadAsync` メソッドを直接使用できます。
- `TotalItems`: データの総数を指定します。
- `ShowPager`: ページャーナビゲーションを表示するかどうかを指定します。
- `CurrentPage`: データページネーションの現在のページを指定します。
- `Columns`: `[TableColumn](https://github.com/abpframework/abp/blob/dev/framework/src/Volo.Abp.AspNetCore.Components.Web/Volo/Abp/AspNetCore/Components/Web/Extensibility/TableColumns/TableColumn.cs)` のリストを指定します。
- `SelectionMode`: データの選択方法を指定します。`Multiple?DataGridSelectionMode.Single` と `Multiple?DataGridSelectionMode.Multiple` をサポートしており、デフォルト値は `Multiple?DataGridSelectionMode.Single` です。
- `ExtraHeight`: `ExtensibleDataGrid` の追加の高さを指定し、開発者が `ExtensibleDataGrid` の自動高さを制御できるようにします。

## PasswordEdit コンポーネント

`PasswordEdit` コンポーネントは、`<TextEdit Role="TextRole.Password"></TextEdit>` のラッパーであり、パスワードを平文で表示できる切り替え機能や新しいパスワードのランダム生成機能を追加しています。

### 基本的な使用法

```html
<PasswordEdit @bind-Password="NewPassword"></PasswordEdit>
```

### API

以下は `PasswordEdit` コンポーネントのすべてのプロパティです：

- `Password`: 入力ボックス内のパスワードを取得または設定します。
- `PasswordChanged`: パスワードが変更されたときに発生するイベントコールバックです。
- `Length`: ランダムに生成されるパスワードの長さを設定します（デフォルト値: 6）。
- `Characters`: ランダムに生成されるパスワードの文字セットを設定します（デフォルト値: `ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz_-{}[]!@#$%^&*();'=+<>,.|?0123456789`）。
- `Validator`: 選択された値を検証するための検証ハンドラーです。
- `AsyncValidator`: 選択された値を非同期に検証するための非同期検証ハンドラーです。

## ExtensibleTreeView コンポーネント

`ExtensibleTreeView` コンポーネントは、`[Blazorise TreeView](https://blazorise.com/docs/extensions/treeview)` コンポーネントのラッパーで、元の機能を保持し、ドラッグアンドドロップ機能を追加しています。

### 基本的な使用法

```html
<ExtensibleTreeView TNode="Item" Nodes="Items" Draggable
          GetChildNodes="@(item => item.Children)"
          HasChildNodes="@(item => item.Children?.Any() == true)"
          @bind-SelectedNode="selectedNode"
          @bind-ExpandedNodes="expandedNodes"
          NodeDropped="@OnDroped">
    <NodeContent>
        <Icon Name="IconName.Folder" />
        @context.Text
    </NodeContent>
</ExtensibleTreeView>
```

```csharp
@code{
    public class Item
    {
        public string Text { get; set; }
        public IEnumerable<Item> Children { get; set; }
    }

    IEnumerable<Item> Items = new[]
    {
        new Item { Text = "アイテム 1" },
        new Item
        {
            Text = "アイテム 2",
            Children = new []
            {
                new Item { Text = "アイテム 2.1" },
                new Item
                {
                    Text = "アイテム 2.2",
                    Children = new []
                    {
                        new Item { Text = "アイテム 2.2.1" },
                        new Item { Text = "アイテム 2.2.2" },
                        new Item { Text = "アイテム 2.2.3" },
                        new Item { Text = "アイテム 2.2.4" }
                    }
                },
                new Item { Text = "アイテム 2.3" },
                new Item { Text = "アイテム 2.4" }
            }
        },
        new Item { Text = "アイテム 3" },
    };

    IList<Item> expandedNodes = new List<Item>();
    Item selectedNode;

    
    protected virtual async void OnDroped(DropNode<Item> dropNode)
    {
        if (dropNode.Node.Id == dropNode.Target.Id)
            return;

        if(dropNode.Area==DragEnterNodePosition.Inside)
        {
            // ターゲットノード内でのドラッグアンドドロップ
        }
        else if (dropNode.Area == DragEnterNodePosition.Bottom)
        {
            // ターゲットノードの下にドラッグアンドドロップ
        }

        await this.InvokeAsync(() => this.StateHasChanged());
    }
}
```

### API

以下は `ExtensibleTreeView` コンポーネントのプロパティです：

- `TNode`: ノードデータの型を指定します。
- `Nodes`: コンポーネントにバインドするノードリストを指定します。
- `NodeContent`: ノードの内容を表示するテンプレートです。
- `SelectedNode`: 現在選択されているノードを指定します。
- `SelectedNodeChanged`: ノードが選択されたときに発生するイベントコールバックです。
- `ExpandedNodes`: 現在展開されているサブノードのリストを指定します。
- `ExpandedNodesChanged`: 展開ノードのコレクションが変更されたときに発生するイベントコールバックです。
- `GetChildNodes`: ノードのサブノードリ

ストを取得するためのメソッドを指定します。
- `HasChildNodes`: ノードが子要素を持つかどうかを判断するためのメソッドを指定します。
- `Draggable`: ドラッグアンドドロップ機能を有効にするかどうかを指定します（デフォルト値: false）。
- `NodeDropped`: ノードがドラッグアンドドロップされたときに発生するイベントコールバックです。

## TreeSelect コンポーネント

TreeSelect コンポーネントは、ツリー形式のドロップダウン選択コンポーネントです。

### 基本的な使用法

```html
<TreeSelect TNode="Item" TValue="string"
            Nodes="@Items"
            SelectedNodeChanged="OnSelectNodeChanged"
            GetChildNodes="@(item => item.Children)"
            HasChildNodes="@(item => item.Children?.Any() == true)"
            NodeText="x=>x.Text">
</TreeSelect>
```

```csharp
@code{
    public class Item
    {
        public string Text { get; set; }
        public IEnumerable<Item> Children { get; set; }
    }

    IEnumerable<Item> Items = new[]
    {
        new Item { Text = "アイテム 1" },
        new Item
        {
            Text = "アイテム 2",
            Children = new []
            {
                new Item { Text = "アイテム 2.1" },
                new Item
                {
                    Text = "アイテム 2.2",
                    Children = new []
                    {
                        new Item { Text = "アイテム 2.2.1" },
                        new Item { Text = "アイテム 2.2.2" },
                        new Item { Text = "アイテム 2.2.3" },
                        new Item { Text = "アイテム 2.2.4" }
                    }
                },
                new Item { Text = "アイテム 2.3" },
                new Item { Text = "アイテム 2.4" }
            }
        },
        new Item { Text = "アイテム 3" },
    };

    
    protected virtual Task OnSelectNodeChanged(Item args)
    {        
        Console.WriteLine(args.Text);
        return Task.CompletedTask;
    }
}
```

### API

以下は `TreeSelect` コンポーネントのプロパティです：

- `Nodes`: コンポーネントにバインドするノードリストを指定します。
- `SelectedNode`: 現在選択されているノードを指定します。
- `SelectedNodeChanged`: ノードが選択されたときに発生するイベントコールバックです。
- `ExpandedNodes`: 現在展開されているサブノードのリストを指定します。
- `ExpandedNodesChanged`: 展開ノードのコレクションが変更されたときに発生するイベントコールバックです。
- `ClearNode`: 選択されたノードをクリアするときに発生するイベントコールバックです。
- `GetChildNodes`: ノードのサブノードリストを取得するためのメソッドを指定します。
- `HasChildNodes`: ノードが子要素を持つかどうかを判断するためのメソッドを指定します。
- `NodeText`: ノードのテキストを表示するためのデリゲートを指定します。
- `Validator`: 選択された値を検証するための検証ハンドラーです。
- `AsyncValidator`: 非同期に選択された値を検証するための非同期検証ハンドラーです。
