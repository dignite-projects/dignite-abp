using AutoMapper;
using Dignite.Cms.Entries;
using Dignite.Cms.Fields;
using Dignite.Cms.Public.Entries;
using Dignite.Cms.Public.Sections;
using Dignite.Cms.Sections;
using Volo.Abp.AutoMapper;
using Volo.Abp.ObjectExtending;

namespace Dignite.Cms.Public;

public class CmsPublicApplicationAutoMapperProfile : Profile
{
    public CmsPublicApplicationAutoMapperProfile()
    {
        /**** field *****************************************/
        CreateMap<Field, FieldDto>();

        /**** section *****************************************/
        CreateMap<Section, SectionDto>();
        CreateMap<EntryType, EntryTypeDto>();
        CreateMap<EntryFieldTab, EntryFieldTabDto>();
        CreateMap<EntryField, EntryFieldDto>()
                .Ignore(ef => ef.Field);


        /**** entity *****************************************/
        CreateMap<Entry, EntryDto>()
            .Ignore(e => e.Author)
            .MapExtraProperties(MappingPropertyDefinitionChecks.None);
    }
}
