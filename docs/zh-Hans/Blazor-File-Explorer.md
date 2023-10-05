# Blazor 文件管理器

```json
//[doc-params]
{
    "UI": ["Blazor", "BlazorServer"]
}
```

Blazor 文件管理器是一个用于管理文件和文件夹的强大工具，可以轻松集成到你的 Blazor 应用程序中。本文将介绍如何安装和使用 Blazor 文件管理器。

## 安装

安装 Blazor 文件管理器只需几个简单的步骤。

- 将 `Dignite.FileExplorer.Blazor` NuGet 包安装到你的 Blazor 项目中。
  
  添加 `FileExplorerBlazorModule` 到 [模块类](https://docs.abp.io/en/abp/latest/Module-Development-Basics) `[DependsOn(...)]` 属性列表中。

{{if UI == "Blazor"}}

- 将 `Dignite.FileExplorer.HttpApi.Client` NuGet 包安装到你的 HttpApi.Client 项目中。
  
  添加 `FileExplorerHttpApiClientModule` 到 [模块类](https://docs.abp.io/en/abp/latest/Module-Development-Basics) `[DependsOn(...)]` 属性列表中。
  
- 将 `Dignite.FileExplorer.Blazor.WebAssembly` NuGet 包安装到你的 Blazor.WebAssembly 项目中。
  
  添加 `FileExplorerBlazorWebAssemblyModule` 到 [模块类](https://docs.abp.io/en/abp/latest/Module-Development-Basics) `[DependsOn(...)]` 属性列表中。

- 在Blazor项目中执行一次 [abp bundle](https://docs.abp.io/en/abp/latest/CLI#bundle)

{{end}}

{{if UI == "BlazorServer"}}

- 将 `Dignite.FileExplorer.Blazor.Server` NuGet 包安装到你的 Blazor.Server 项目中。
  
  添加 `FileExplorerBlazorServerModule` 到 [模块类](https://docs.abp.io/en/abp/latest/Module-Development-Basics) `[DependsOn(...)]` 属性列表中。

{{end}}

## 示例

如果你想立即体验 Blazor 文件管理器，可以前往 [GitHub](https://github.com/dignite-projects/dignite-abp/tree/main/samples/FileExplorerSample) 下载示例代码，并按照说明运行它。这个示例将帮助你快速了解如何在你的 Blazor 应用程序中集成和使用文件管理器。
