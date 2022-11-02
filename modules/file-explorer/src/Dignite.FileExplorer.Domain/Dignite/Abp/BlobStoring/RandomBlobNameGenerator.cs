using System;
using System.IO;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Dignite.Abp.BlobStoring;

/// <summary>
/// Implements <see cref="IBlobNameGenerator"/> by using <see cref="Guid.NewGuid"/>.
/// </summary>
public class RandomBlobNameGenerator : IBlobNameGenerator, ITransientDependency
{
    public static RandomBlobNameGenerator Instance { get; } = new RandomBlobNameGenerator();

    public virtual Task<string> Create()
    {
        return Task.FromResult(
            Path.GetRandomFileName().Replace(".", "")
            );
    }
}