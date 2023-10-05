# BlazoriseUI 组件

## 简介

BlazoriseUI 组件是一套基于 [Blazorise](https://blazorise.com) 开发的 Blazor 组件集合，包括树状组件、增强型 DataGrid 等。

## 安装

要在 ABP 项目中使用 BlazoriseUI 组件，您需要执行以下步骤：

1. **安装 NuGet 包**：将 `Dignite.Abp.BlazoriseUI` NuGet 包添加到您的 Blazor 项目中。这个包包含了所有必要的组件和资源，以便您可以在应用程序中使用它们。

2. **模块集成**：将 `[DependsOn(typeof(DigniteAbpBlazoriseUiModule))]` 添加到项目的 ABP 模块依赖列表中。这将确保组件在应用程序中正确注册并可用。

## AutoHeight 组件

`AutoHeight` 组件生成一个 `<div>` 元素，根据当前视窗高度充满剩余的屏幕空间。如果内容高度超过 `<AutoHeight>` 组件的高度，将显示滚动条。

### 基本用法

```html
<AutoHeight>
    <div>这是内部元素</div>
</AutoHeight>
```

### 带有 ExtraHeight 的 AutoHeight

为 `<AutoHeight>` 组件设置 `ExtraHeight` 属性，可以减少 `<AutoHeight>` 组件的高度。

```html
<AutoHeight ExtraHeight="45">
    <div>这是内部元素</div>
</AutoHeight>
```

## ExtensibleDataGrid 组件

`ExtensibleDataGrid` 组件简化了使用 `[Blazorise DataGrid](https://blazorise.com/docs/extensions/datagrid/getting-started)` 组件的过程，同时增加了自适应高度和编程式 `TableColumn` 等新特性。

### 基本用法

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

`SelectionMode` 属性支持两种方式：`Multiple?DataGridSelectionMode.Single` 和 `Multiple?DataGridSelectionMode.Multiple`，默认值为 `Multiple?DataGridSelectionMode.Single`。

如果将 `SelectionMode` 设置为 `Multiple?DataGridSelectionMode.Multiple`，行记录前面将显示复选框。

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
<Button Color="Color.Primary" Clicked="SelectSectionsAsync">选择记录</Button>
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

以下是 `ExtensibleDataGrid` 组件的所有属性：

- `TItem`: 指定数据类型。
- `Data`: 指定数据列表。
- `ReadData`: 读取数据列表的异步方法。如果页面继承自 `AbpCrudPageBase` 类，则可以直接使用 `OnDataGridReadAsync` 方法。
- `TotalItems`: 指定数据总数。
- `ShowPager`: 指定是否显示分页导航。
- `CurrentPage`: 指定数据分页中的当前页。
- `Columns`: 指定 `[TableColumn](https://github.com/abpframework/abp/blob/dev/framework/src/Volo.Abp.AspNetCore.Components.Web/Volo/Abp/AspNetCore/Components/Web/Extensibility/TableColumns/TableColumn.cs)` 列表。
- `SelectionMode`: 指定选择数据的方式，支持 `Multiple?DataGridSelectionMode.Single` 和 `Multiple?DataGridSelectionMode.Multiple` 两种方式，默认值为 `Multiple?DataGridSelectionMode.Single`。
- `ExtraHeight`: 指定 `ExtensibleDataGrid` 的额外高度，用于开发者控制 `ExtensibleDataGrid` 的自适应高度。

## PasswordEdit 组件

`PasswordEdit` 组件是 `<TextEdit Role="TextRole.Password"></TextEdit>` 的封装，增加了可显示密码明文的切换功能以及随机生成新密码的功能。

### 基本用法

```html
<PasswordEdit @bind-Password="NewPassword"></PasswordEdit>
```

### API

以下是 `PasswordEdit` 组件的所有属性：

- `Password`: 获取或设置输入框内的密码。
- `PasswordChanged`: 密码更改后触发事件回调。
- `Length`: 设置随机生成密码的长度（默认值：6）。
- `Characters`: 设置随机生成的密码字符集（默认值：ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghizklmnopqrstuvwxyz_-{}[]!@#$%^&*();'=+<>,.|?0123456789）。
- `Validator`: 用于验证选中值的验证处理程序。
- `AsyncValidator`: 异步验证所选值。

## ExtensibleTreeView 组件

`ExtensibleTreeView` 组件是 `[Blazorise TreeView](https://blazorise.com/docs/extensions/treeview)` 组件

的封装，保留其原有功能，增加了拖拽功能。

### 基本用法

```html
<ExtensibleTreeView TNode="Item" Nodes="Items" Draggable
          GetChildNodes="@(item => item.Children)"
          HasChildNodes="@(item => item.Children?.Any() == true)"
          @bind-SelectedNode="selectedNode"
          @bind-ExpandedNodes="expandedNodes"
          NodeDropped="@OnDropped">
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
        new Item { Text = "Item 1" },
        new Item
        {
            Text = "Item 2",
            Children = new []
            {
                new Item { Text = "Item 2.1" },
                new Item
                {
                    Text = "Item 2.2",
                    Children = new []
                    {
                        new Item { Text = "Item 2.2.1" },
                        new Item { Text = "Item 2.2.2" },
                        new Item { Text = "Item 2.2.3" },
                        new Item { Text = "Item 2.2.4" }
                    }
                },
                new Item { Text = "Item 2.3" },
                new Item { Text = "Item 2.4" }
            }
        },
        new Item { Text = "Item 3" },
    };

    IList<Item> expandedNodes = new List<Item>();
    Item selectedNode;

    
    protected virtual async void OnDropped(DropNode<Item> dropNode)
    {
        if (dropNode.Node.Id == dropNode.Target.Id)
            return;

        if(dropNode.Area==DragEnterNodePosition.Inside)
        {
            // 在目标节点内拖拽
        }
        else if (dropNode.Area == DragEnterNodePosition.Bottom)
        {
            // 在目标节点下方拖拽
        }

        await this.InvokeAsync(() => this.StateHasChanged());
    }
}
```

### API

以下是 `ExtensibleTreeView` 组件的属性：

- `TNode`: 指定节点数据类型。
- `Nodes`: 绑定到组件的节点列表。
- `NodeContent`: 用于显示节点内容的模板。
- `SelectedNode`: 当前选中的节点。
- `SelectedNodeChanged`: 选中节点发生变化时触发的事件回调。
- `ExpandedNodes`: 当前展开的子节点列表。
- `ExpandedNodesChanged`: 扩展节点集合发生变化时触发的事件回调。
- `GetChildNodes`: 获取节点的子节点列表。
- `HasChildNodes`: 判断节点是否有子元素。
- `Draggable`: 是否启用拖拽功能（默认值：false）。
- `NodeDropped`: 拖拽节点后触发的事件回调。

## TreeSelect 组件

TreeSelect 组件是一个下拉式树形选择组件。

### 基本用法

```html
<TreeSelect TNode="Item" TValue="string"
            Nodes="@Items"
            SelectedNodeChanged="OnSelectNodeChanged"
            GetChildNodes="@(item => item.Children)"
            HasChildNodes="@(item => item.Children?.Any() == true)"
            NodeText="x => x.Text">
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
        new Item { Text = "Item 1" },
        new Item
        {
            Text = "Item 2",
            Children = new []
            {
                new Item { Text = "Item 2.1" },
                new Item
                {
                    Text = "Item 2.2",
                    Children = new []
                    {
                        new Item { Text = "Item 2.2.1" },
                        new Item { Text = "Item 2.2.2" },
                        new Item { Text = "Item 2.2.3" },
                        new Item { Text = "Item 2.2.4" }
                    }
                },
                new Item { Text = "Item 2.3" },
                new Item { Text = "Item 2.4" }
            }
        },
        new Item { Text = "Item 3" },
    };

    
    protected virtual Task OnSelectNodeChanged(Item args)
    {        
        Console.WriteLine(args.Text);
        return Task.CompletedTask;
    }
}
```

### API

以下是 `TreeSelect` 组件的属性：

- `Nodes`: 绑定到组件的节点列表。
- `SelectedNode`: 当前选中的节点。
- `SelectedNodeChanged`: 选中节点发生变化时触发的事件回调。
- `ExpandedNodes`: 当前展开的子节点列表。
- `ExpandedNodesChanged`: 扩展节点集合发生变化时触发的事件回调。
- `ClearNode`: 当清除选择的节点时触发的事件回调。
- `GetChildNodes`: 获取节点的子节点列表。
- `HasChildNodes`: 判断节点是否有子元素。
- `NodeText`: 设置显示节点的文本。
- `Validator`: 用于验证选中值的验证处理程序。
- `AsyncValidator`: 异步验证所选值的验证处理程序。