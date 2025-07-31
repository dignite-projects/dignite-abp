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
using Volo.CmsKit.Users;

namespace Dignite.Publisher;

public class PublisherDataSeedContributor : IDataSeedContributor, ITransientDependency
{
    private readonly IGuidGenerator _guidGenerator;
    private readonly ICurrentTenant _currentTenant;
    private readonly PublisherTestData _testData;
    private readonly ICmsUserRepository _cmsUserRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IPostRepository _postRepository;

    public PublisherDataSeedContributor(IGuidGenerator guidGenerator, ICurrentTenant currentTenant, PublisherTestData testData, 
        ICmsUserRepository cmsUserRepository, ICategoryRepository categoryRepository, IPostRepository postRepository)
    {
        _guidGenerator = guidGenerator;
        _currentTenant = currentTenant;
        _testData = testData;
        _cmsUserRepository = cmsUserRepository;
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
            await SeedUsersAsync();
            await SeedPostsAsync();
        }
    }

    private async Task SeedUsersAsync()
    {
        await _cmsUserRepository.InsertAsync(new CmsUser(new UserData(_testData.User1Id, "user1",
            "user1@dignite.com",
            "user", "1")),
        autoSave: true);

        await _cmsUserRepository.InsertAsync(new CmsUser(new UserData(_testData.User2Id, "user2",
            "user2@dignite.com",
            "user", "2")),
            autoSave: true);

        var list = await _cmsUserRepository.GetListAsync();
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
        var articlePost = new ArticlePost(
                _testData.Post_1_Id, _testData.Local_En, _testData.Post_1_Title, _testData.Post_1_Slug, null, null, null,
                new Guid[] { _testData.Category_1_Id, _testData.Category_2_Id }, _currentTenant.Id, _testData.Article_Post_1_Content);
        articlePost.SetPublished();
        await _postRepository.InsertAsync(
            articlePost,
            autoSave: true);

        var videoPost = new VideoPost(
                _testData.Post_2_Id, _testData.Local_En, _testData.Post_2_Title, _testData.Post_2_Slug, null, null, null,
                new Guid[] { _testData.Category_1_Id, _testData.Category_2_Id }, _currentTenant.Id, _testData.Video_Post_2_VideoUrl, TimeSpan.FromMinutes(3.5), null);
        videoPost.SetPublished();
        await _postRepository.InsertAsync(
            videoPost,
            autoSave: true);
    }
}
