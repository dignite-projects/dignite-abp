# Blazor File Manager

```json
//[doc-params]
{
    "UI": ["Blazor", "BlazorServer"]
}
```

The Blazor File Manager is a powerful tool for managing files and folders, and it can be easily integrated into your Blazor applications. This article explains the installation and usage of the Blazor File Manager.

## Installation

Installing the Blazor File Manager involves a few simple steps.

- Install the `Dignite.FileExplorer.Blazor` NuGet package in your Blazor project.

  Add `FileExplorerBlazorModule` to the `[DependsOn(...)]` property list in your [module class](https://docs.abp.io/en/abp/latest/Module-Development-Basics).

{{if UI == "Blazor"}}

- Install the `Dignite.FileExplorer.HttpApi.Client` NuGet package in your HttpApi.Client project.

  Add `FileExplorerHttpApiClientModule` to the `[DependsOn(...)]` property list in your [module class](https://docs.abp.io/en/abp/latest/Module-Development-Basics).

- Install the `Dignite.FileExplorer.Blazor.WebAssembly` NuGet package in your Blazor WebAssembly project.

  Add `FileExplorerBlazorWebAssemblyModule` to the `[DependsOn(...)]` property list in your [module class](https://docs.abp.io/en/abp/latest/Module-Development-Basics).

- Run [abp bundle](https://docs.abp.io/en/abp/latest/CLI#bundle) once in your Blazor project.

{{end}}

{{if UI == "BlazorServer"}}

- Install the `Dignite.FileExplorer.Blazor.Server` NuGet package in your Blazor Server project.

  Add `FileExplorerBlazorServerModule` to the `[DependsOn(...)]` property list in your [module class](https://docs.abp.io/en/abp/latest/Module-Development-Basics).

{{end}}

## Example

If you want to try out the Blazor File Manager quickly, you can download the sample code from [GitHub](https://github.com/dignite-projects/dignite-abp/tree/main/samples/FileExplorerSample) and follow the instructions to run it. This sample will help you quickly understand how to integrate and use the File Manager in your Blazor application.
