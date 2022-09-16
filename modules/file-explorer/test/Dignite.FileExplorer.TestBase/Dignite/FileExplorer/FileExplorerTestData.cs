using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Dignite.FileExplorer;
public class FileExplorerTestData : ISingletonDependency
{
    public string EntityTypeFullName { get; } = "EntityTypeFullName";
    public string EntityId { get; } = "123456";
    public string ContainerName1 { get; } = "testContainer1";
    public string BlobName1 { get; } = "testBlobName1";
    public string ContainerName2 { get; } = "testContainer2";
    public string BlobName2 { get; } = "testBlobName2";
    public string ContainerName3 { get; } = "testContainer3";
    public string BlobName3 { get; } = "testBlobName2";
}
