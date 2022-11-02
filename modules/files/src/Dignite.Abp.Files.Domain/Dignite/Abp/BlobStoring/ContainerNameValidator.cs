using Volo.Abp.DependencyInjection;

namespace Dignite.Abp.BlobStoring;

public class ContainerNameValidator : ITransientDependency
{
    public void Validate(string name)
    {
        /*
         TODO:Check if container name exists

         */
    }
}