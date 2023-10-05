# File Storage Management

File management is an application module built on top of [ABP BlobStoring](https://docs.abp.io/zh-Hans/abp/latest/Blob-Storing). It provides features for file uploads, including file type validation and file size validation. Additionally, developers can add custom processing logic.

> Before delving into the usage of this module, it is essential to thoroughly read the [ABP BlobStoring](https://docs.abp.io/zh-Hans/abp/latest/Blob-Storing) documentation.

## Installation

Here are the steps to install the File Management module:

1. Install the `Dignite.Abp.Files.Domain.Shared` NuGet package into your `Domain.Shared` project.

2. Add `DigniteAbpFilesDomainSharedModule` to the `[DependsOn(...)]` attribute list of your [module class](https://docs.abp.io/en/abp/latest/Module-Development-Basics).

3. Install the `Dignite.Abp.Files.Domain` NuGet package into your Domain project.

4. Add `DigniteAbpFilesDomainModule` to the `[DependsOn(...)]` attribute list of your [module class](https://docs.abp.io/en/abp/latest/Module-Development-Basics).

5. Install the `Dignite.Abp.Files.EntityFrameworkCore` NuGet package into your EntityFrameworkCore project.

6. Add `DigniteAbpFilesEntityFrameworkCoreModule` to the `[DependsOn(...)]` attribute list of your [module class](https://docs.abp.io/en/abp/latest/Module-Development-Basics).

## File Storage Handlers

In addition to configuring [ABP BlobStoring](https://docs.abp.io/zh-Hans/abp/latest/Blob-Storing), this module introduces two file handlers:

- File Upload Type Configuration

    Configure allowed file types in the `ConfigureServices` method:

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

- File Upload Size Configuration

    Configure maximum file upload size in the `ConfigureServices` method:

    ```csharp
    Configure<AbpBlobStoringOptions>(options =>
    {
        options.Containers.Configure<ProfilePictureContainer>(container =>
        {
            container.AddFileSizeLimitHandler(config =>
                config.MaxFileSize = 1024    // Limit file size (KB)
            );
        });
    });
    ```

## Creating File Handlers

1. Create a configuration class to store handler-specific settings:

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

    Then, add the following extension method in the `MyCustomHandlerBlobContainerConfigurationExtensions` class:

    ```csharp
    public static MyCustomFileHandlerConfiguration GetMyCustomFileHandlerConfiguration(
        this BlobContainerConfiguration containerConfiguration)
    {
        return new MyCustomFileHandlerConfiguration(containerConfiguration);
    }
    ```

2. Create an extension method for `BlobContainerConfiguration`:

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

3. Create a class that implements the `IFileHandler` interface:

    Implement the `IFileHandler` interface to build a custom handler that suits your business needs:

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

                // Handle file stream here
                return Task.CompletedTask;
            }
        }
    }
    ```

4. Configure the handler in the `ConfigureServices` method:

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

## File Information Persistence

The File Storage Management module provides interfaces to standardize the development of file information persistence.

### IFile Interface

`IFile` is an interface that represents file information and provides the following basic properties:

- ContainerName: The name of the storage container where the file is stored. See [Typed IBlobContainer](https://docs.abp.io/en/abp/latest/Blob-Storing#typed-iblobcontainer).

- BlobName: Corresponds to the BlobName in BLOB storage.

- Size: The size of the file in bytes.

- Name: The name of the file.

- MimeType: The MIME type of the file. For example, the MIME type for an image may be `.jpeg`, `.jpg`, or `.png`.

- Md5: The MD5 hash of the file. This is used to verify whether a user-uploaded file already exists in the container.

- ReferBlobName: In the same container, if the same file is uploaded again, the system won't store the file again but will instead reference the BlobName of the existing file. This mechanism helps save storage space for duplicate files.

### FileBase Abstract Class

`FileBase` is an implementation of the `IFile` interface. Developers can use `FileBase` as the base class for file information.

### IFileStore Interface

`IFileStore` is a generic interface that provides a series of methods for CRUD operations on `IFile` objects.

### FileManager<TFile, TFileStore> Abstract Class

`FileManager` is a generic abstract class that encapsulates a series of methods from `IFileStore`.

> A complete implementation of these concepts can be found in the

 [File Explorer module](File-Explorer.md).

### Configuring EF Core Entity

`Dignite.Abp.Files.EntityFrameworkCore` provides a convenient extension method to configure properties inherited from the `IFile` interface.

Use `ConfigureAbpFiles<TFile>` to configure the fields of `IFile`. Add this configuration in the `OnModelCreating` method of your `DbContext`:

```csharp
protected override void OnModelCreating(ModelBuilder builder)
{
    // Always call the base method
    base.OnModelCreating(builder);

    builder.Entity<FileDescriptor>(b =>
    {
        // Configure the table
        b.ToTable("FileDescriptors");

        b.ConfigureByConvention();

        // Configure ABP File properties
        b.ConfigureAbpFiles();
    });
}
```

> For information about the `FileDescriptor` object, please refer to the [source code of the File Explorer module](https://github.com/dignite-projects/dignite-abp/blob/main/modules/file-explorer/src/Dignite.FileExplorer.Domain/Dignite/FileExplorer/Files/FileDescriptor.cs).

## Continue Reading

- [File Explorer Module](File-Explorer.md)
    A comprehensive file management module that provides features for managing directories and files.
