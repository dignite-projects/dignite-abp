using AutoMapper;
using Dignite.FileExplorer.Directories;
using Dignite.FileExplorer.Files;
using Volo.Abp.AutoMapper;

namespace Dignite.FileExplorer;

public class FileExplorerApplicationAutoMapperProfile : Profile
{
    public FileExplorerApplicationAutoMapperProfile()
    {
        CreateMap<FileDescriptor, FileDescriptorDto>()
            .Ignore(fd=>fd.Url);
        CreateMap<DirectoryDescriptor, DirectoryDescriptorDto>();
        CreateMap<DirectoryDescriptor, DirectoryDescriptorInfoDto>()
            .Ignore(x => x.HasChildren)
            .Ignore(x => x.Children);
    }
}