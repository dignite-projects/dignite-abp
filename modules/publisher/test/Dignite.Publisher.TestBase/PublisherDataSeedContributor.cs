using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dignite.Publisher.Categories;
using Dignite.Publisher.Posts;
using Dignite.Publisher.TestBase;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Guids;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Users;

namespace Dignite.Publisher;

public class PublisherDataSeedContributor : IDataSeedContributor, ITransientDependency
{
    private readonly IGuidGenerator _guidGenerator;
    private readonly ICurrentTenant _currentTenant;
    private readonly PublisherTestData _testData;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IPostRepository _postRepository;

    public PublisherDataSeedContributor(IGuidGenerator guidGenerator, ICurrentTenant currentTenant, PublisherTestData testData, ICategoryRepository categoryRepository, IPostRepository postRepository)
    {
        _guidGenerator = guidGenerator;
        _currentTenant = currentTenant;
        _testData = testData;
        _categoryRepository = categoryRepository;
        _postRepository = postRepository;
    }

    public async Task SeedAsync(DataSeedContext context)
    {
        /* Instead of returning the Task.CompletedTask, you can insert your test data
         * at this point!
         */

        using (_currentTenant.Change(context?.TenantId))
        {
            await SeedCategoriesAsync();
            await SeedPostsAsync();
        }
    }

    private async Task SeedCategoriesAsync()
    {
        await _categoryRepository.InsertAsync(
            new Category(
                _testData.Category_1_Id,_testData.Local_En,null,_testData.Category_1_DisplayName,_testData.Category_1_Name,null,true,
                new List<string> { PostTypeConsts.ArticlePostTypeName,PostTypeConsts.VideoPostTypeName},1,_currentTenant.Id),
            autoSave: true);

        await _categoryRepository.InsertAsync(
            new Category(
                _testData.Category_2_Id, _testData.Local_En, _testData.Category_1_Id, _testData.Category_2_DisplayName, _testData.Category_2_Name, null, true,
                new List<string> { PostTypeConsts.ArticlePostTypeName, PostTypeConsts.VideoPostTypeName }, 1, _currentTenant.Id),
            autoSave: true);
    }
    private async Task SeedPostsAsync()
    {
        await _postRepository.InsertAsync(
            new ArticlePost(
                _testData.Post_1_Id,_testData.Local_En,_testData.Post_1_Title,_testData.Post_1_Slug,null,null,null,
                new Guid[] {_testData.Category_1_Id,_testData.Category_2_Id }, _currentTenant.Id,_testData.Article_Post_1_Content),
            autoSave: true);

        await _postRepository.InsertAsync(
            new ArticlePost(
                _testData.Post_2_Id, _testData.Local_En, _testData.Post_2_Title, _testData.Post_2_Slug, null, null, null,
                new Guid[] { _testData.Category_1_Id, _testData.Category_2_Id }, _currentTenant.Id, _testData.Article_Post_2_Content),
            autoSave: true);
    }
}
