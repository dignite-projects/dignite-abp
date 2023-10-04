# Blazor 文件管理器

````json
//[doc-params]
{
    "UI": ["Blazor", "BlazorServer"]
}
````

## 安装

- 将 `Dignite.FileExplorer.Blazor` Nuget 包安装到 Blazor 项目中

    添加 `FileExplorerBlazorModule` 到 [模块类](https://docs.abp.io/en/abp/latest/Module-Development-Basics) `[DependsOn(...)]`属性列表中。

{{if UI == "Blazor"}}

- 将 `Dignite.FileExplorer.HttpApi.Client` Nuget 包安装到 HttpApi.Client 项目中

    添加 `FileExplorerHttpApiClientModule` 到 [模块类](https://docs.abp.io/en/abp/latest/Module-Development-Basics) `[DependsOn(...)]`属性列表中。

- 将 `Dignite.FileExplorer.Blazor.WebAssembly` Nuget 包安装到 Blazor.WebAssembly 项目中

    添加 `FileExplorerBlazorWebAssemblyModule` 到 [模块类](https://docs.abp.io/en/abp/latest/Module-Development-Basics) `[DependsOn(...)]`属性列表中。

- 在Blazor项目中执行一次 [abp bundle](https://docs.abp.io/en/abp/latest/CLI#bundle)

{{end}}

{{if UI == "BlazorServer"}}

- 将 `Dignite.FileExplorer.Blazor.Server` Nuget 包安装到 Blazor.Server 项目中

    添加 `FileExplorerBlazorServerModule` 到 [模块类](https://docs.abp.io/en/abp/latest/Module-Development-Basics) `[DependsOn(...)]`属性列表中。

{{end}}

## 示例

请前往 [Github](https://github.com/dignite-projects/dignite-abp/tree/main/samples/FileExplorerSample)下载示例，并按说明运行体验 Blazor 版文件管理器。
