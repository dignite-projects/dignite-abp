using AutoMapper;
using Dignite.FileExplorer.Files;

namespace Dignite.FileExplorer;

public class FileExplorerDomainMappingProfile : Profile
{
    public FileExplorerDomainMappingProfile()
    {
        CreateMap<FileDescriptor, FileDescriptorEto>();
    }
}