using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Dignite.Abp.Files.Fakes;
public class FakeFileManager:FileManager<FakeFile,FakeFileStore>, ITransientDependency
{
}
