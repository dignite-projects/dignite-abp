using AutoMapper;
using Dignite.FileExplorer.Files;

namespace Dignite.FileExplorer;

public class FileExplorerApplicationAutoMapperProfile : Profile
{
    public FileExplorerApplicationAutoMapperProfile()
    {
        CreateMap<FileDescriptor, FileDescriptorDto>();
    }
}