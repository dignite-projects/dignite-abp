using AutoMapper;
using Dignite.Abp.FileManagement.Files;

namespace Dignite.Abp.FileManagement;

public class FileManagementApplicationAutoMapperProfile : Profile
{
    public FileManagementApplicationAutoMapperProfile()
    {
        CreateMap<FileDescriptor, FileDescriptorDto>();
    }
}
