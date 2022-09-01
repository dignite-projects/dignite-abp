using JetBrains.Annotations;

namespace Dignite.Abp.BlobStoring.InfoPersistent;

public class BasicBlobInfo : IBlobInfo
{
    public BasicBlobInfo()
    {
    }

    public BasicBlobInfo(
        [NotNull] string containerName,
        [NotNull] string blobName
        )
    {
        ContainerName = containerName;
        BlobName = blobName;
    }

    /*
    public BasicBlobInfo(string containerName, string blobName, long binarySize, string hash, string referBlobName) : this(containerName, blobName)
    {
        BinarySize = binarySize;
        Hash = hash;
        ReferBlobName = referBlobName;
    }
    */

    [NotNull]
    public string ContainerName { get; private set; }

    [NotNull]
    public string BlobName { get; private set; }

    [NotNull]
    public long Size { get; set; }

    [CanBeNull]
    public string Hash { get; set; }

    [CanBeNull]
    public string ReferBlobName { get; set; }
}
