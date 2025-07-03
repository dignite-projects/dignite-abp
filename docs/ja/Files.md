# ファイルストレージ管理

ファイル管理は、[ABP BlobStoring](https://docs.abp.io/zh-Hans/abp/latest/Blob-Storing)をベースにしたアプリケーションモジュールで、ファイルのアップロードに関する機能を提供します。ファイルタイプの検証やファイルサイズの検証などが行え、開発者はカスタムな処理ロジックを追加することもできます。

> このモジュールの使用方法を探る前に、[ABP BlobStoring](https://docs.abp.io/zh-Hans/abp/latest/Blob-Storing)のドキュメンテーションを十分に読むことが重要です。

## インストール

ファイル管理モジュールをインストールする手順は次のとおりです。

1. `Dignite.Abp.Files.Domain.Shared` NuGet パッケージを `Domain.Shared` プロジェクトにインストールします。

2. `AbpFilesDomainSharedModule` を [モジュールクラス](https://docs.abp.io/en/abp/latest/Module-Development-Basics) の `[DependsOn(...)]` 属性リストに追加します。

3. `Dignite.Abp.Files.Domain` NuGet パッケージを Domain プロジェクトにインストールします。

4. `AbpFilesDomainModule` を [モジュールクラス](https://docs.abp.io/en/abp/latest/Module-Development-Basics) の `[DependsOn(...)]` 属性リストに追加します。

5. `Dignite.Abp.Files.EntityFrameworkCore` NuGet パッケージを EntityFrameworkCore プロジェクトにインストールします。

6. `AbpFilesEntityFrameworkCoreModule` を [モジュールクラス](https://docs.abp.io/en/abp/latest/Module-Development-Basics) の `[DependsOn(...)]` 属性リストに追加します。

## ファイルストレージハンドラ

[ABP BlobStoring](https://docs.abp.io/zh-Hans/abp/latest/Blob-Storing)を設定するだけでなく、このモジュールでは2つのファイルハンドラも紹介されています。

- ファイルアップロードタイプの設定

    `ConfigureServices` メソッドで許可されるファイルタイプを設定します。

    ```csharp
    Configure<AbpBlobStoringOptions>(options =>
    {
        options.Containers.Configure<ProfilePictureContainer>(container =>
        {
            container.AddFileTypeCheckHandler(config =>
                config.AllowedFileTypeNames = new string[] { ".jpeg",".jpg",".png" }
            );
        });
    });
    ```

- ファイルアップロードサイズの設定

    `ConfigureServices` メソッドで最大ファイルアップロードサイズを設定します。

    ```csharp
    Configure<AbpBlobStoringOptions>(options =>
    {
        options.Containers.Configure<ProfilePictureContainer>(container =>
        {
            container.AddFileSizeLimitHandler(config =>
                config.MaxFileSize = 1024    // ファイルサイズ制限（KB）
            );
        });
    });
    ```

## ファイルハンドラの作成

1. ハンドラ固有の設定を保存する設定クラスを作成します。

    ```csharp
    using Volo.Abp.BlobStoring;

    namespace Dignite.Abp.BlobStoring
    {
        public class MyCustomFileHandlerConfiguration
        {
            public string Option1
            {
                get => _containerConfiguration.GetConfigurationOrDefault<string>("MyCustomFileHandlerOption", null);
                set => _containerConfiguration.SetConfiguration("MyCustomFileHandlerOption", value);
            }

            private readonly BlobContainerConfiguration _containerConfiguration;

            public MyCustomFileHandlerConfiguration(BlobContainerConfiguration containerConfiguration)
            {
                _containerConfiguration = containerConfiguration;
            }
        }
    }
    ```

    次に、`MyCustomHandlerBlobContainerConfigurationExtensions` クラスに次の拡張メソッドを追加します。

    ```csharp
    public static MyCustomFileHandlerConfiguration GetMyCustomFileHandlerConfiguration(
        this BlobContainerConfiguration containerConfiguration)
    {
        return new MyCustomFileHandlerConfiguration(containerConfiguration);
    }
    ```

2. `BlobContainerConfiguration` の拡張メソッドを作成します。

    ```csharp
    using System;
    using Volo.Abp.BlobStoring;
    using Volo.Abp.Collections;

    namespace Dignite.Demo
    {
        public static class MyCustomHandlerBlobContainerConfigurationExtensions
        {
            public static void AddMyCustomHandler(
                this BlobContainerConfiguration containerConfiguration,
                Action<MyCustomFileHandlerConfiguration> configureAction)
            {
                var fileProcessHandlers = containerConfiguration.GetConfigurationOrDefault(
                    BlobContainerConfigurationNames.FileHandlers,
                    new TypeList<IFileHandler>());

                if (fileProcessHandlers.TryAdd<MyCustomFileHandler>())
                {
                    configureAction(new MyCustomFileHandlerConfiguration(containerConfiguration));

                    containerConfiguration.SetConfiguration(
                        BlobContainerConfigurationNames.FileHandlers,
                        fileProcessHandlers);
                }
            }
        }
    }
    ```

3. `IFileHandler` インターフェースを実装したクラスを作成します。

    `IFileHandler` インターフェースを実装して、ビジネスニーズに合ったカスタムハンドラを構築します。

    ```csharp
    using System.Threading.Tasks;
    using Volo.Abp;
    using Volo.Abp.DependencyInjection;

    namespace Dignite.Demo
    {
        public class MyCustomFileHandler : IFileHandler, ITransientDependency
        {
            public Task ExecuteAsync(FileHandlerContext context)
            {
                var configuration = context.ContainerConfiguration.GetMyCustomFileHandlerConfiguration();

                // ここでファイルストリームを処理
                return Task.CompletedTask;
            }
        }
    }
    ```

4. `ConfigureServices` メソッドでハンドラを設定します。

    ```csharp
    Configure<AbpBlobStoringOptions>(options =>
    {


        options.Containers.Configure<MyCustomFileHandlerConfiguration>(container =>
        {
            container.AddMyCustomHandler(config =>
                config.Option1 = "abc"
            );
        });
    });
    ```

## ファイル情報の永続化

ファイルストレージ管理では、ファイル情報の永続化を行うためのインターフェースが提供されています。

### `IFile` インターフェース

`IFile` はファイル情報のインターフェースで、次の基本フィールドを提供します。

- `ContainerName`
    ファイルを格納するコンテナの名前です。
    [Typed IBlobContainer](https://docs.abp.io/en/abp/latest/Blob-Storing#typed-iblobcontainer)を参照してください。

- `BlobName`
    BLOB ストレージ内の BlobName に対応します。

- `Size`
    ファイルのバイナリサイズです。

- `Name`
    ファイルの名前です。

- `MimeType`
    ファイルの MIME タイプです。
    たとえば、画像の MIME タイプは`image/png`、`image/jpeg`、`image/gif`のいずれかです。

- `Md5`
    ファイルの MD5 値です。
    ユーザーがアップロードしたファイルがコンテナ内に既に存在するかどうかを検証するために使用されます。

- `ReferBlobName`
    同じコンテナ内で、同じファイルをアップロードした場合、システムはファイルを再度保存するのではなく、既存のファイルの BlobName を参照します。このメカニズムにより、重複するファイルのストレージスペースを節約できます。

### `FileBase` 抽象クラス

`FileBase` は `IFile` インターフェースの実装であり、開発者はファイル情報の基本クラスとして `FileBase` を使用できます。

### `IFileStore` インターフェース

`IFileStore` はジェネリックなインターフェースで、`IFile` の CRUD 操作に関連する一連のメソッドを提供します。

### `FileManager<TFile, TFileStore>` 抽象クラス

`FileManager` はジェネリックな抽象クラスで、`IFileStore` のメソッドをカプセル化します。

> 完全な実装例は、[ファイル管理モジュール](File-Explorer.md)で提供されています。

### Ef Core エンティティの設定

`Dignite.Abp.Files.EntityFrameworkCore` は、`IFile` インターフェースから継承したプロパティを設定する便利な拡張メソッドを提供しています。

`ConfigureAbpFiles<TFile>` を使用して、`IFile` のフィールドを設定します。これらの設定は、`DbContext` の `OnModelCreating` メソッドに追加します：

```csharp
protected override void OnModelCreating(ModelBuilder builder)
{
    // 必ずベースメソッドを呼び出す
    base.OnModelCreating(builder);

    builder.Entity<FileDescriptor>(b =>
    {
        // テーブルを設定
        b.ToTable("FileDescriptors");

        b.ConfigureByConvention();

        // ABP ファイルプロパティを設定
        b.ConfigureAbpFiles();
    });
}
```

> `FileDescriptor` オブジェクトに関する情報は、[ファイル管理モジュールのソースコード](https://github.com/dignite-projects/dignite-abp/blob/main/modules/file-explorer/src/Dignite.FileExplorer.Domain/Dignite/FileExplorer/Files/FileDescriptor.cs)を参照してください。

## 続きを読む

- [ファイル管理モジュール](File-Explorer.md)
    ディレクトリとファイルの管理機能を提供する包括的なファイル管理モジュール。
