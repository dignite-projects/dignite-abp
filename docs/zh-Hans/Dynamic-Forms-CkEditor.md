# CkEditor 动态表单

CkEditor 动态表单组件是 [Dignite.Abp.DynamicForms](Dynamic-Forms.md) 的组件，关于动态表单组件的开发知识请参阅[Blazor 动态表单组件](Blazor-Dynamic-Form-Components.md)。

## 安装

* 将 `Dignite.Abp.DynamicForms.CkEditor` Nuget 包安装到 `Application Layer` 的 `Contracts`项目中。

* 添加 `DigniteAbpDynamicFormsCkEditorModule` 到 [模块类](https://docs.abp.io/en/abp/latest/Module-Development-Basics) `[DependsOn(...)]`属性列表中。

* 将 `Dignite.Abp.DynamicForms.Components.CkEditor` Nuget 包安装到 `Blazor` 项目中。

* 添加 `DigniteAbpDynamicFormsComponentsCkEditorModule` 到 [模块类](https://docs.abp.io/en/abp/latest/Module-Development-Basics) `[DependsOn(...)]`属性列表中。

下图是CkEditor 动态表单在[Dignite CMS](https://dignite.com/dignite-cms)后台配置的截图：

![Cms-Dynamic-Forms-CkEditor](images/Cms-Dynamic-Forms-CkEditor.jpg)
