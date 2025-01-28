using System;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;


namespace Dignite.Abp.BlobStoring
{
    public class YearMonthBlobNameGenerator : IBlobNameGenerator, ITransientDependency
    {
        public Task<string> Create()
        {
            return Task.FromResult(
                DateTime.Now.Year.ToString() + "/" + DateTime.Now.Month.ToString() + "/" + Guid.NewGuid().ToString("N")
                );
        }
    }
}
