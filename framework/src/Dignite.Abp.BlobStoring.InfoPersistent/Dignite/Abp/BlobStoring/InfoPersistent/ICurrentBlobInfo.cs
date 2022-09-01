using System;

namespace Dignite.Abp.BlobStoring.InfoPersistent;

public interface ICurrentBlobInfo
{
    bool IsAvailable { get; }

    IBlobInfo BlobInfo { get; }

    IDisposable Current(IBlobInfo blobInfo);
}
