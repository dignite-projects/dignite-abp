using AutoMapper;
using Dignite.FileExplorer.Directories;

namespace Dignite.FileExplorer.Blazor;

public class FileExplorerBlazorAutoMapperProfile : Profile
{
    public FileExplorerBlazorAutoMapperProfile()
    {
        CreateMap<DirectoryDescriptorDto, UpdateDirectoryInput>();
    }
}
