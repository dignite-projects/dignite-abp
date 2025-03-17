using Dignite.Cms.Entries;
using Dignite.Cms.Fields;
using Dignite.Cms.Sections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Text.Formatting;

namespace Dignite.Cms.Public.Sections
{
    public class SectionPublicAppService : CmsPublicAppService, ISectionPublicAppService
    {
        private readonly ISectionRepository _sectionRepository;
        private readonly IFieldRepository _fieldRepository;

        public SectionPublicAppService(ISectionRepository sectionRepository, IFieldRepository fieldRepository)
        {
            _sectionRepository = sectionRepository;
            _fieldRepository= fieldRepository;
        }

        public async Task<SectionDto> FindByNameAsync(string name)
        {
            var dto = ObjectMapper.Map<Section, SectionDto>(
                await _sectionRepository.FindByNameAsync(name)
                );
            if (dto != null)
            {
                await FillFields(dto);
            }
            return dto;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entityPath">
        /// The entry path does not contain culture.
        /// </param>
        /// <returns></returns>
        public async Task<SectionDto> FindByEntityPathAsync( string entityPath)
        {
            var allSections = await _sectionRepository.GetListAsync(null, true, true);
            var section = await MatchingSectionByEntityPath(allSections, entityPath);

            /**
             * When no matching Section is found, add /index/ to the url to try again.
             * The reason is that when the Section type is SectionType.Single, the url may not contain a slug, so the default slug (i.e. index) is used to match again.
             * **/
            if (section == null)
            {
                entityPath = entityPath.EnsureEndsWith('/') + EntryConsts.DefaultSlug;
                section= await MatchingSectionByEntityPath(allSections,entityPath);
            }

            return section;
        }


        public async Task<ListResultDto<SectionDto>> GetListAsync(GetSectionsInput input)
        { 
            var list = await _sectionRepository.GetListAsync(
                isActive:true,
                includeDetails:false
                );
            var dto = ObjectMapper.Map<List<Section>, List<SectionDto>>(list);

            return new ListResultDto<SectionDto>(dto);
        }

        public async Task<SectionDto> GetAsync(Guid id)
        {
            var dto = ObjectMapper.Map<Section, SectionDto>(
                await _sectionRepository.GetAsync(id)
                );

            if (dto != null)
            {
                await FillFields(dto);
            }
            return dto;
        }

        public async Task<SectionDto> GetDefaultAsync()
        {
            var result = await _sectionRepository.GetDefaultAsync();

            if (result == null)
            {
                return null;
            }
            else
            {
                var dto = ObjectMapper.Map<Section, SectionDto>(
                    result
                    );

                await FillFields(dto);
                return dto;
            }
        }

        protected async Task<SectionDto> MatchingSectionByEntityPath(List<Section> sections, string entryPath)
        {
            entryPath = entryPath.EnsureStartsWith('/').EnsureEndsWith('/');
            foreach (var section in sections.OrderBy(s => s.Route))
            {
                var sectionRoute = section.Route.EnsureStartsWith('/').EnsureEndsWith('/');
                var extractResult = FormattedStringValueExtracter.Extract(entryPath, sectionRoute, ignoreCase: true);
                if (
                    (!sectionRoute.Contains("{slug}", StringComparison.InvariantCultureIgnoreCase) && sectionRoute.Equals(entryPath,StringComparison.InvariantCultureIgnoreCase))
                    ||
                    (sectionRoute.Contains("{slug}", StringComparison.InvariantCultureIgnoreCase) && extractResult.IsMatch)
                    )
                {
                    var dto = ObjectMapper.Map<Section, SectionDto>(
                        section
                        );
                    await FillFields(dto);
                    return dto;
                }
            }

            return null;
        }

        protected async Task FillFields(SectionDto dto)
        { 
            var allFields = await _fieldRepository.GetListAsync(false);
            var fieldsDto = ObjectMapper.Map<List<Field>, List<FieldDto>>(allFields);
            foreach (var entryType in dto.EntryTypes)
            {
                foreach (var fieldTab in entryType.FieldTabs)
                {
                    foreach (var entryField in fieldTab.Fields)
                    {
                        entryField.Field = fieldsDto.FirstOrDefault(f => f.Id == entryField.FieldId);
                    }
                }
            }
        }
    }
}
