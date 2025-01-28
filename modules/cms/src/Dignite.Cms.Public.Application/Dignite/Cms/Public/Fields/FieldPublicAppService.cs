using Dignite.Cms.Fields;
using System.Threading.Tasks;

namespace Dignite.Cms.Public.Fields
{
    public class FieldPublicAppService : CmsPublicAppService, IFieldPublicAppService
    {
        private readonly IFieldRepository _fieldRepository;

        public FieldPublicAppService(IFieldRepository fieldRepository)
        {
            _fieldRepository = fieldRepository;
        }


        public async Task<FieldDto> FindByNameAsync(string name)
        {
            var result = await _fieldRepository.FindByNameAsync(name);
            return ObjectMapper.Map<Field, FieldDto>(result);
        }

    }
}