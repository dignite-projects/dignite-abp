using System.Collections.Generic;
using Volo.Abp.BlobStoring;

namespace Dignite.Abp.BlobStoring;

public class FileGridConfiguration
{
    public IReadOnlyList<FileCell> FileCells {
        get => _containerConfiguration.GetConfigurationOrDefault<IReadOnlyList<FileCell>>(FileGridConfigurationNames.FileCells);
        set => _containerConfiguration.SetConfiguration(FileGridConfigurationNames.FileCells, value);
    }

    private readonly BlobContainerConfiguration _containerConfiguration;

    public FileGridConfiguration(BlobContainerConfiguration containerConfiguration)
    {
        _containerConfiguration = containerConfiguration;
    }
}