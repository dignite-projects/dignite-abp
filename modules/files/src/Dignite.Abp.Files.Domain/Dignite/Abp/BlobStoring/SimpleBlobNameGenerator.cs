using System;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Dignite.Abp.BlobStoring;

/// <summary>
/// Implements <see cref="IBlobNameGenerator"/> by using <see cref="Guid.NewGuid"/>.
/// </summary>
public class SimpleBlobNameGenerator : IBlobNameGenerator, ITransientDependency
{
    public static SimpleBlobNameGenerator Instance { get; } = new SimpleBlobNameGenerator();

    public virtual Task<string> Create()
    {
        return Task.FromResult(
            Guid.NewGuid().ToString("N")
            );
    }
}