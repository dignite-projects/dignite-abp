using AutoMapper;
using Dignite.FileExplorer.Directories;
using Dignite.FileExplorer.Files;

namespace Dignite.FileExplorer;

public class FileExplorerApplicationAutoMapperProfile : Profile
{
    public FileExplorerApplicationAutoMapperProfile()
    {
        CreateMap<FileDescriptor, FileDescriptorDto>();
        CreateMap<DirectoryDescriptor, DirectoryDescriptorDto>();
        CreateMap<DirectoryDescriptor, DirectoryDescriptorInfoDto>();
    }
}