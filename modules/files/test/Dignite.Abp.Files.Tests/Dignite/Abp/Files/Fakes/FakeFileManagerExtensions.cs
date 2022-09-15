using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Dignite.Abp.Files.Fakes;

namespace Dignite.Abp.Files;
public static class FakeFileManagerExtensions
{
    public static async Task<FakeFile> CreateAsync(
        this FakeFileManager fakeFileManager,
            FakeFile file,
            byte[] bytes,
            bool overrideExisting = false,
            CancellationToken cancellationToken = default)
    {
        using (var memoryStream = new MemoryStream(bytes))
        {
            return await fakeFileManager.CreateAsync(
                file,
                memoryStream,
                overrideExisting,
                cancellationToken
            );
        }
    }
}
