# Blazor 文件动态表单组件

文件动态表单组件是 [Dignite.Abp.DynamicForms](Dynamic-Forms.md) 的组件，关于动态表单组件的开发知识请参阅[Blazor 动态表单组件](Blazor-Dynamic-Form-Components.md)。

## 安装

* 将 `Dignite.Abp.DynamicForms.FileExplorer` Nuget 包安装到 `Application Layer` 的 `Contracts`项目中。

* 添加 `DigniteAbpDynamicFormsFileExplorerModule` 到 [模块类](https://docs.abp.io/en/abp/latest/Module-Development-Basics) `[DependsOn(...)]`属性列表中。

* 将 `Dignite.Abp.DynamicForms.Components.FileExplorer` Nuget 包安装到 `Blazor` 项目中。

* 添加 `DigniteAbpDynamicFormsComponentsFileExplorerModule` 到 [模块类](https://docs.abp.io/en/abp/latest/Module-Development-Basics) `[DependsOn(...)]`属性列表中。

下图是文件动态表单在[Dignite CMS](https://dignite.com/dignite-cms)后台配置的截图：

![Cms-Dynamic-Forms-Files](images/Cms-Dynamic-Forms-Files.png)
