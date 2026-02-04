using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dignite.Cms.Entries;
using Dignite.Cms.Fields;
using Dignite.Cms.Sections;
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
            var result = await _sectionRepository.FindByNameAsync(name);
            return await MapToSectionDto(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entryPath">
        /// The entry path does not contain culture.
        /// </param>
        /// <returns></returns>
        public async Task<SectionDto> FindByEntryPathAsync( string entryPath)
        {
            var allSections = await _sectionRepository.GetListAsync(null, true, true);
            var section = await MatchingSectionByEntryPath(allSections, entryPath);

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
            var result = await _sectionRepository.GetAsync(id);
            return await MapToSectionDto(result);
        }

        public async Task<SectionDto> GetDefaultAsync()
        {
            var result = await _sectionRepository.GetDefaultAsync();
            return await MapToSectionDto(result);
        }

        protected async Task<SectionDto> MatchingSectionByEntryPath(List<Section> sections, string entryPath)
        {
            var reorderedSections = sections
                .OrderBy(s => s.IsDefault)
                .ThenBy(s => s.Route.EnsureStartsWith('/').EnsureEndsWith('/'))
                .ToList();
            entryPath = entryPath.EnsureStartsWith('/').EnsureEndsWith('/');
            foreach (var section in reorderedSections)
            {
                var sectionRoute = section.Route.EnsureStartsWith('/').EnsureEndsWith('/');
                if (!sectionRoute.Contains("{slug}", StringComparison.InvariantCultureIgnoreCase))
                {
                    if (sectionRoute.Equals(entryPath, StringComparison.InvariantCultureIgnoreCase))
                        return await MapToSectionDto(section);
                }
                else
                {
                    var extractResult = FormattedStringValueExtracter.Extract(entryPath, sectionRoute, ignoreCase: true);
                    if (extractResult.IsMatch)
                    {
                        return await MapToSectionDto(section);
                    }
                    else
                    {
                        extractResult = FormattedStringValueExtracter.Extract($"{entryPath}{EntryConsts.DefaultSlug}/", sectionRoute, ignoreCase: true);
                        if (extractResult.IsMatch)
                        {
                            return await MapToSectionDto(section);
                        }
                    }
                }
            }

            return null;
        }

        protected async Task<SectionDto> MapToSectionDto(Section section)
        {
            if (section == null)
                return null;

            var dto = ObjectMapper.Map<Section, SectionDto>(
                section
                );

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
            return dto;
        }
    }
}
