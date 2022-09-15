using Volo.Abp.DependencyInjection;

namespace Dignite.Abp.Files.Fakes;
public class FakeFileManager:FileManager<FakeFile,FakeFileStore>, ITransientDependency
{
}
