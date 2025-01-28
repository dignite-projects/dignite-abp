using Dignite.Cms.Entries;
using System;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.Domain.Services;

namespace Dignite.Cms.Sections
{
    public class SectionManager:DomainService
    {
        protected readonly ISectionRepository _sectionRepository;

        public SectionManager(ISectionRepository sectionRepository)
        {
            _sectionRepository = sectionRepository;
        }

        public async Task<Section> CreateAsync( SectionType type, string displayName, string name, bool isDefault, bool isActive, string route, string template, Guid? tenantId)
        {
            await CheckNameExistenceAsync(name);

            if(!route.IsNullOrWhiteSpace())
                await CheckRouteExistenceAsync(route);

            //
            var section = new Section(
                GuidGenerator.Create(),
                type,
                displayName, name,
                isDefault,
                isActive,
                route,
                template,
                tenantId);

            //
            CheckSectionType(section);
            CheckSlugRoutingParameter(section);

            //
            if (section.IsDefault)
            {
                var sections = await _sectionRepository.GetListAsync();
                foreach (var item in sections)
                {
                    section.SetDefault(false);
                }
            }

            //
            return await _sectionRepository.InsertAsync(section);
        }


        public async Task<Section> UpdateAsync(Guid id, SectionType type, string displayName, string name, bool isDefault, bool isActive, string route, string template,string concurrencyStamp)
        {
            var section = await _sectionRepository.GetAsync(id);
            section.SetConcurrencyStampIfNotNull(concurrencyStamp);
            if (!section.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
            {
                await CheckNameExistenceAsync( name);
            }
            if (!section.Route.IsNullOrWhiteSpace() && !section.Route.Equals(route, StringComparison.OrdinalIgnoreCase))
            {
                await CheckRouteExistenceAsync( route);
            }

            //
            if (isDefault && !section.IsDefault)
            {
                var sections = await _sectionRepository.GetListAsync();
                foreach (var item in sections.Where(s=>s.Id!=id))
                {
                    item.SetDefault(false);
                }
            }

            //
            section.SetActive(isActive);
            section.SetDefault(isDefault);
            section.SetDisplayName(displayName);
            section.Route = route;
            section.Template = template;
            section.SetName(name);
            section.SetType(type);

            //
            CheckSectionType(section);
            CheckSlugRoutingParameter(section);

            //
            return await _sectionRepository.UpdateAsync(section);
        }


        protected virtual async Task CheckNameExistenceAsync( string name)
        {
            if (await _sectionRepository.NameExistsAsync( name))
            {
                throw new SectionNameAlreadyExistException(name);
            }
        }
        protected virtual async Task CheckRouteExistenceAsync(string route)
        {
            if (await _sectionRepository.RouteExistsAsync(route))
            {
                throw new SectionRouteAlreadyExistException(route);
            }
        }


        protected virtual void CheckSectionType(Section section)
        {
            if (section.IsDefault && section.Type != SectionType.Single)
            {
                throw new DefaultSectionMustBeSingleTypeException(section.Name);
            }
        }


        protected virtual void CheckSlugRoutingParameter(Section section)
        {
            if (!section.Route.IsNullOrWhiteSpace() 
                && section.Type != SectionType.Single 
                && !section.Route.Contains($"{{{nameof(Entry.Slug)}}}", StringComparison.OrdinalIgnoreCase)
                )
            {
                throw new MissingSlugRoutingParameterException(section.Type, section.Route);
            }
        }
    }
}
