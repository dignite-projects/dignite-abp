# 文件存储管理

文件管理是基于[ABP BlobStoring](https://docs.abp.io/zh-Hans/abp/latest/Blob-Storing)开发，为文件上传提供文件类型验证、文件大小验证的处理，开发人员还可以提供更多的处理逻辑。

> 学习使用本模块使用方法前，请务必详细阅读 [ABP BlobStoring](https://docs.abp.io/zh-Hans/abp/latest/Blob-Storing) 文档。

## 安装

- 将 `Dignite.Abp.Files.Domain.Shared` Nuget 包安装到 `Domain.Shared` 项目中

- 添加 `DigniteAbpFilesDomainSharedModule` 到 [模块类](https://docs.abp.io/en/abp/latest/Module-Development-Basics) `[DependsOn(...)]`属性列表中。

- 将 `Dignite.Abp.Files.Domain` Nuget 包安装到 Domain 项目中

- 添加 `DigniteAbpFilesDomainModule` 到 [模块类](https://docs.abp.io/en/abp/latest/Module-Development-Basics) `[DependsOn(...)]`属性列表中。

- 将 `Dignite.Abp.Files.EntityFrameworkCore` Nuget 包安装到 EntityFrameworkCore 项目中

- 添加 `DigniteAbpFilesEntityFrameworkCoreModule` 到 [模块类](https://docs.abp.io/en/abp/latest/Module-Development-Basics) `[DependsOn(...)]`属性列表中。

## 文件存储处理器

除 [ABP BlobStoring](https://docs.abp.io/zh-Hans/abp/latest/Blob-Storing) 的配置外，本模块新增两个文件处理器配置：

- 文件上传类型配置

    在`ConfigureServices` 方法中配置允许上传的文件类型：

    ````csharp
    Configure<AbpBlobStoringOptions>(options =>
    {
        options.Containers.Configure<ProfilePictureContainer>(container =>
        {
            container.AddFileTypeCheckHandler(config =>
                config.AllowedFileTypeNames = new string[] { ".jpeg",".jpg",".png" }
            );
        });
    });
    ````

- 文件上传大小配置

    在`ConfigureServices` 方法中配置上传文件大小：

    ````csharp
    Configure<AbpBlobStoringOptions>(options =>
    {
        options.Containers.Configure<ProfilePictureContainer>(container =>
        {
            container.AddFileSizeLimitHandler(config =>
                config.MaxFileSize = 1024    //Limit file size(KB)
            );
        });
    });
    ````

## 创建文件处理器

1. 创建一个配置类，用于存储处理器的配置项

    ````csharp
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
    ````

    然后，在`MyCustomHandlerBlobContainerConfigurationExtensions`类中增加下扩展方法：

    ````csharp
    public static MyCustomFileHandlerConfiguration GetMyCustomFileHandlerConfiguration(
        this BlobContainerConfiguration containerConfiguration)
    {
        return new MyCustomFileHandlerConfiguration(containerConfiguration);
    }
    ````

2. 创建`BlobContainerConfiguration`的扩展方法：

    ````csharp
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
    ````

3. 创建一个继承自`IFileHandler`接口的类：

    通过实现`IFileHandler`的接口，可以构建符合你的业务需要的处理器。

    ````csharp
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

                //在这里处理文件流
                return Task.CompletedTask;
            }
        }
    }
    ````

4. 在`ConfigureServices` 方法中配置

    ````csharp
    Configure<AbpBlobStoringOptions>(options =>
    {
        options.Containers.Configure<MyCustomFileHandlerConfiguration>(container =>
        {
            container.AddMyCustomHandler(config =>
                config.Option1 = "abc"
            );
        });
    });
    ````

## 文件信息持久化

文件存储管理提供了一些接口，规范文件信息的持久化开发。

### IFile 接口

`IFile` 是文件信息的接口类，提供以下基础字段：

- ContainerName
    存储文件的容器名称。
    参见 [Typed IBlobContainer](https://docs.abp.io/en/abp/latest/Blob-Storing#typed-iblobcontainer)

- BlobName
    对应BLOB 存储中的 BlobName。

- Size
    文件二进制大小。

- Name
    文件的名称。

- MimeType
    文件 Mime type。
    例如图片的 Mime type 可能是`image/png`、`image/jpeg`、`image/gif`其中的一个。

- Md5
    文件的 Md5 值。
    用于验证用户上传的文件在容器中是否已存在。

- ReferBlobName
    在同一容器中，当上传了相同的文件时，系统不会再次存储文件，而是引用已存在文件的 BlobName，这种机制能够节省重复文件的存储空间。

### FileBase 抽象类

`FileBase` 是 `IFile` 接口的实现，开发者可使用`FileBase`作为文件信息的基类。

### IFileStore 接口

`IFileStore`是一个泛型接口，提供了一系列方法用于`IFile`的CURD操作。

### FileManager<TFile, TFileStore> 抽象类

`FileManager`是一个泛型抽象类，包装了`IFileStore`的系列方法。

> 在[文件管理器模块](File-Explorer.md)中有一个完整的实现。

### Configure Ef Core Entity

`Dignite.Abp.Files.EntityFrameworkCore`提供了一个实用的扩展方法，配置从`IFile`接口继承的属性。

`ConfigureAbpFiles<TFile>` 配置了`IFile`中的字段。在 `DbContext` 的 `OnModelCreating` 方法中做以下配置:

````csharp
protected override void OnModelCreating(ModelBuilder builder)
{
    //Always call the base method
    base.OnModelCreating(builder);

    builder.Entity<FileDescriptor>(b =>
    {
        //Configure table
        b.ToTable("FileDescriptors");

        b.ConfigureByConvention();

        //
        b.ConfigureAbpFiles();
    });
}
````

> `FileDescriptor` 对象信息请参阅[文件管理器模块源码](https://github.com/dignite-projects/dignite-abp/blob/main/modules/file-explorer/src/Dignite.FileExplorer.Domain/Dignite/FileExplorer/Files/FileDescriptor.cs)。

## 继续阅读

- [文件管理器模块](File-Explorer.md)
    一套完整的文件管理模块，提供文件目录管理和文件管理功能。
