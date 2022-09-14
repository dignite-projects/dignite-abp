using System.Threading.Tasks;

namespace Dignite.Abp.BlobStoring;

/// <summary>
/// Generate new blob names.
/// </summary>
public interface IBlobNameGenerator
{
    /// <summary>
    /// Create a new blob name.
    /// </summary>
    Task<string> Create();
}