# Blazor ファイルマネージャ

```json
//[doc-params]
{
    "UI": ["Blazor", "BlazorServer"]
}
```

Blazor ファイルマネージャは、ファイルとフォルダを管理するための強力なツールで、簡単にBlazorアプリケーションに統合できます。この記事では、Blazorファイルマネージャのインストールと使用方法について説明します。

## インストール

Blazor ファイルマネージャをインストールするには、いくつか簡単なステップが必要です。

- `Dignite.FileExplorer.Blazor` NuGet パッケージを Blazor プロジェクトにインストールします。
  
  [モジュールクラス](https://docs.abp.io/en/abp/latest/Module-Development-Basics) の `[DependsOn(...)]` プロパティリストに `FileExplorerBlazorModule` を追加します。

{{if UI == "Blazor"}}

- `Dignite.FileExplorer.HttpApi.Client` NuGet パッケージを HttpApi.Client プロジェクトにインストールします。
  
  [モジュールクラス](https://docs.abp.io/en/abp/latest/Module-Development-Basics) の `[DependsOn(...)]` プロパティリストに `FileExplorerHttpApiClientModule` を追加します。

- `Dignite.FileExplorer.Blazor.WebAssembly` NuGet パッケージを Blazor.WebAssembly プロジェクトにインストールします。
  
  [モジュールクラス](https://docs.abp.io/en/abp/latest/Module-Development-Basics) の `[DependsOn(...)]` プロパティリストに `FileExplorerBlazorWebAssemblyModule` を追加します。

- Blazor プロジェクトで [abp bundle](https://docs.abp.io/en/abp/latest/CLI#bundle) を一度実行します。

{{end}}

{{if UI == "BlazorServer"}}

- `Dignite.FileExplorer.Blazor.Server` NuGet パッケージを Blazor.Server プロジェクトにインストールします。
  
  [モジュールクラス](https://docs.abp.io/en/abp/latest/Module-Development-Basics) の `[DependsOn(...)]` プロパティリストに `FileExplorerBlazorServerModule` を追加します。

{{end}}

## サンプル

Blazor ファイルマネージャをすぐに試してみたい場合は、[GitHub](https://github.com/dignite-projects/dignite-abp/tree/main/samples/FileExplorerSample) からサンプルコードをダウンロードし、指示に従って実行できます。このサンプルは、Blazorアプリケーションにファイルマネージャを統合して使用する方法を素早く理解するのに役立ちます。
