using Dignite.Abp.BlobStoring;
using Volo.Abp.DependencyInjection;

namespace FileExplorerSample.BlobStoring;

public class YearMonthBlobNameGenerator : IBlobNameGenerator, ITransientDependency
{
    public Task<string> Create()
    {
        return Task.FromResult(
            DateTime.Now.Year.ToString() + "/" + DateTime.Now.Month.ToString() + "/" + Guid.NewGuid().ToString("N")
            );
    }
}
