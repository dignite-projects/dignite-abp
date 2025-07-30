using System.Threading.Tasks;
using Dignite.Publisher.TestBase;
using Shouldly;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Modularity;
using Xunit;

namespace Dignite.Publisher.Posts;

public abstract class PostManager_Tests<TStartupModule> : PublisherDomainTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{
    private readonly PostManager _postManager;
    private readonly IPostRepository _postRepository;
    private readonly PublisherTestData _testData;

    public PostManager_Tests()
    {
        _postManager = GetRequiredService<PostManager>();
        _postRepository = GetRequiredService<IPostRepository>();
        _testData = GetRequiredService<PublisherTestData>();
    }

    [Fact]
    public async Task DeleteAsync()
    {
        var post = await _postRepository.GetAsync(_testData.Post_1_Id);
        await _postManager.DeleteAsync(post);
        var exception = await Should.ThrowAsync<EntityNotFoundException>(async () =>
                            await _postRepository.GetAsync(_testData.Post_1_Id));
        exception.EntityType.ShouldBe(typeof(Post));
        exception.Id.ShouldBe(_testData.Post_1_Id);
    }
}
