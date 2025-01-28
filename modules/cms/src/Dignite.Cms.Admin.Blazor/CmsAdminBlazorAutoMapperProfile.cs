using AutoMapper;
using Dignite.Cms.Admin.Entries;
using Dignite.Cms.Admin.Fields;
using Dignite.Cms.Admin.Sections;
using Dignite.Cms.Sections;
using Volo.Abp.AutoMapper;

namespace Dignite.Cms.Admin.Blazor
{
    public class CmsAdminBlazorAutoMapperProfile : Profile
    {
        public CmsAdminBlazorAutoMapperProfile()
        {
            CreateMap<FieldGroupDto, CreateOrUpdateFieldGroupInput>();
            CreateMap<FieldDto, UpdateFieldInput>();
            CreateMap<SectionDto, UpdateSectionInput>()
                .MapExtraProperties();
            CreateMap<EntryTypeDto, UpdateEntryTypeInput>();
            CreateMap<EntryFieldTabDto, EntryFieldTabInput>();
            CreateMap<EntryFieldDto, EntryFieldInput>();
            CreateMap<EntryDto, CreateEntryInput>()
                .Ignore(e=>e.Draft);
            CreateMap<EntryDto, UpdateEntryInput>()
                .Ignore(e => e.Draft);
        }
    }
}