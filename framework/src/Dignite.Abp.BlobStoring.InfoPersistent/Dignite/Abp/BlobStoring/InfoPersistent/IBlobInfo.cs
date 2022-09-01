using JetBrains.Annotations;

namespace Dignite.Abp.BlobStoring.InfoPersistent;

/// <summary>
/// Blob info interface
/// </summary>
public interface IBlobInfo
{
    /// <summary>
    /// Container name of blob
    /// </summary>
    [NotNull] string ContainerName { get; }

    /// <summary>
    /// Blob name
    /// </summary>
    [NotNull] string BlobName { get; }

    /// <summary>
    /// Blob binary size
    /// </summary>
    long Size { get; set; }

    /// <summary>
    /// Hash of blob
    /// </summary>
    string Hash { get; set; }

    /// <summary>
    /// Referencing other blob
    /// </summary>
    string ReferBlobName { get; set; }
}
