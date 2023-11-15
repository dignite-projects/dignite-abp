using Dignite.CmsKit.Favourites;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Guids;
using Volo.Abp.MultiTenancy;

namespace Dignite.CmsKit;

public class CmsKitDataSeedContributor : IDataSeedContributor, ITransientDependency
{
    private readonly IGuidGenerator _guidGenerator;
    private readonly CmsKitTestData _cmsKitTestData;
    private readonly IFavouriteRepository _favouriteRepository;
    private readonly ICurrentTenant _currentTenant;
    private readonly IOptions<CmsKitFavouriteOptions> _favouriteOptions;

    public CmsKitDataSeedContributor(
        IGuidGenerator guidGenerator,
        CmsKitTestData cmsKitTestData,
        IFavouriteRepository favouriteRepository,
        ICurrentTenant currentTenant,
        IOptions<CmsKitFavouriteOptions> favouriteOptions)
    {
        _guidGenerator = guidGenerator;
        _cmsKitTestData = cmsKitTestData;
        _favouriteRepository = favouriteRepository;
        _currentTenant = currentTenant;
        _favouriteOptions = favouriteOptions;
    }

    public async Task SeedAsync(DataSeedContext context)
    {
        using (_currentTenant.Change(context?.TenantId))
        {
            await ConfigureCmsKitOptionsAsync();

            await SeedFavouriteAsync();
        }
    }

    private Task ConfigureCmsKitOptionsAsync()
    {
        _favouriteOptions.Value.EntityTypes.Add(new FavouriteEntityTypeDefinition(_cmsKitTestData.EntityType1));
        _favouriteOptions.Value.EntityTypes.Add(new FavouriteEntityTypeDefinition(_cmsKitTestData.EntityType2));

        return Task.CompletedTask;
    }


    private async Task SeedFavouriteAsync()
    {
        await _favouriteRepository.InsertAsync(new Favourite(_guidGenerator.Create(),
                _cmsKitTestData.EntityType1,
                _cmsKitTestData.EntityId1,
                _cmsKitTestData.User1Id
            ));

        await _favouriteRepository.InsertAsync(new Favourite(_guidGenerator.Create(),
            _cmsKitTestData.EntityType2,
            _cmsKitTestData.EntityId2,
            _cmsKitTestData.User2Id
        ));
    }

}
