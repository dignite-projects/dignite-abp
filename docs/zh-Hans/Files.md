# Dignite Abp Files

Dignite Abp Files 基于[ABP BlobStoring](https://docs.abp.io/zh-Hans/abp/latest/Blob-Storing)开发，为文件上传过程提供文件类型验证、文件大小验证的处理，开发人员还可以基于处理接口开发更多的处理事件。

本模块还提供了文件信息持久化的基础方案，开发人员可以方便的在数据库中查询文件信息。

> Dignite  Abp Files 在管理复杂的小文件信息项目提供了可靠的方案，尚未提供上传大文件的方案。

> 学习使用本模块使用方法前，请详细阅读 [ABP BlobStoring](https://docs.abp.io/zh-Hans/abp/latest/Blob-Storing) 文档。

## 安装

在Domain项目中安装 [Dignite.Abp.Files.Domain](https://www.nuget.org/packages/Dignite.Abp.Files.Domain) NuGet包，然后添加`[DependsOn(typeof(AbpFilesDomainModule))]`模块依赖。

## 文件存储配置

除 [ABP BlobStoring](https://docs.abp.io/zh-Hans/abp/latest/Blob-Storing) 的配置外，本模块提供了两个文件处理器配置，分别是文件类型的许可检查、文件大小的许可检查：

### FileTypeCheckHandler

配置设定允许用户上传的文件类型：
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

### FileSizeLimitHandler

配置设定上传文件大小：
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


### 创建自定义文件处理

通过实现下面的接口，可以构建符合你的业务需要的处理器。

第一步：创建一个实现`IFileHandler`接口类：

````csharp
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp;

namespace Dignite.Demo
{
    public class MyCustomFileHandler : IFileHandler, ITransientDependency
    {
        public Task ExecuteAsync(FileHandlerContext context)
        {
            //TODO...
            return Task.CompletedTask;
        }
    }
}
````

第二步：创建`BlobContainerConfiguration`的扩展方法：

````csharp
using System;
using Volo.Abp.BlobStoring;
using Volo.Abp.Collections;

namespace Dignite.Demo
{
    public static class MyCustomHandlerBlobContainerConfigurationExtensions
    {
        public static void AddMyCustomHandler(
            this BlobContainerConfiguration containerConfiguration)
        {
            var blobProcessHandlers = containerConfiguration.GetConfigurationOrDefault(
                BlobContainerConfigurationNames.FileHandlers,
                new TypeList<IFileHandler>());

            if (blobProcessHandlers.TryAdd<MyCustomFileHandler>())
            {
                //Configure handler parameters here
            }
        }
    }
}
````

如果需要一个灵活的配置，可创建一个`MyCustomFileHandlerConfiguration`类

````csharp
using Volo.Abp.BlobStoring;

namespace Dignite.Abp.BlobStoring
{
    public class MyCustomFileHandlerConfiguration
    {
        public string Option1
        {
            get => _containerConfiguration.GetConfigurationOrDefault<string>("MyCustomFileHandlerOptionName", null);
            set => _containerConfiguration.SetConfiguration("MyCustomFileHandlerOptionName", value);
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

## 文件信息持久化

Dignite Abp Files 提供了一部分接口，规范文件信息的持久化功能开发。

### IFile

`IFile` 是文件信息的接口类，提供以下基础字段：

**ContainerName**
存储文件的容器名称。 
参见 [Typed IBlobContainer](https://docs.abp.io/en/abp/latest/Blob-Storing#about-naming-the-blobs)

**BlobName**
Blob Storing System 中 BlobName。

**Size**
文件二进制大小。

**Name**
文件的名称。

**MimeType**
文件 Mime type。
例如图片的 Mime type有"image/png","image/jpeg","image/gif"等。

**Md5**
文件的 Md5 值。
用于验证用户上传的文件在容器中是否已存在。

**ReferBlobName**
文件引用已存在的BlobName。
在同一容器中，当上传了相同的文件时，系统不会再次存储文件，而是引用已存在文件的 BlobName，这种机制能够节省重复文件的存储空间。

### FileBase 

`FileBase` 是 `IFile` 接口的实现，开发者可使用`FileBase`作为文件信息的基类，减少代码量。

### IFileStore<TFile>

`IFileStore`是一个泛型接口，它的一系列方法用于`IFile`的CURD等操作。

### FileManager<TFile, TFileStore>

`FileManager`是一个泛型接口，包装了`IFileStore`的系列方法。
> 关于 `FileManager` 的使用，参见[FileExplorer Module](File-Explorer.md)。

### Configure Ef Core Entity

`Dignite.Abp.Files.EntityFrameworkCore`提供了一个实用的扩展方法，配置从`IFile`接口继承的属性。

在EntityFrameworkCore项目中安装[Dignite.Abp.Files.EntityFrameworkCore](https://www.nuget.org/packages/Dignite.Abp.Files.EntityFrameworkCore)Nuget包，然后添加`[DependsOn(typeof(AbpFilesEntityFrameworkCoreModule))]`模块依赖。

`ConfigureAbpFiles<TFile>` 配置了`IFile`中的字段。在 `DbContext` 重写 `OnModelCreating` 方法并且做以下配置:

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
        b.ConfigureAbpFiles();
    });
}
````

> `FileDescriptor` 继承自 `IFile` 或 `FileBase`.

## See Also

- [FileExplorer Module](File-Explorer.md)
是一个文件管理模块，提供文件目录和文件管理功能。