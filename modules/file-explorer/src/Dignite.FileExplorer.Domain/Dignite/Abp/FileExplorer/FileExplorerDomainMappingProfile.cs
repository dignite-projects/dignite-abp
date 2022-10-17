using AutoMapper;
using Dignite.Abp.FileExplorer.Directories;
using Dignite.FileExplorer.Files;

namespace Dignite.FileExplorer;

public class FileExplorerDomainMappingProfile : Profile
{
    public FileExplorerDomainMappingProfile()
    {
        CreateMap<DirectoryDescriptor, DirectoryDescriptorEto>();
        CreateMap<FileDescriptor, FileDescriptorEto>();
    }
}