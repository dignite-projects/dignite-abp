using System;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Identity;
using Volo.Abp.Account;
using Dignite.Publisher.Admin.Posts;
using Dignite.Publisher.Posts;

namespace Dignite.Publisher.Demo.HttpApi.Client.ConsoleTestApp;

public class ClientDemoService : ITransientDependency
{
    private readonly IProfileAppService _profileAppService;
    private readonly IIdentityUserAppService _identityUserAppService;
    private readonly IPostAdminAppService _postAdminAppService;

    public ClientDemoService(
        IProfileAppService profileAppService,
        IIdentityUserAppService identityUserAppService,
        IPostAdminAppService postAdminAppService)
    {
        _profileAppService = profileAppService;
        _identityUserAppService = identityUserAppService;
        _postAdminAppService = postAdminAppService;
    }

    public async Task RunAsync()
    {
        var profileDto = await _profileAppService.GetAsync();
        Console.WriteLine($"UserName : {profileDto.UserName}");
        Console.WriteLine($"Email    : {profileDto.Email}");
        Console.WriteLine($"Name     : {profileDto.Name}");
        Console.WriteLine($"Surname  : {profileDto.Surname}");
        Console.WriteLine();

        var resultDto = await _identityUserAppService.GetListAsync(new GetIdentityUsersInput());
        Console.WriteLine($"Total users: {resultDto.TotalCount}");
        foreach (var identityUserDto in resultDto.Items)
        {
            Console.WriteLine($"- [{identityUserDto.Id}] {identityUserDto.Name}");
        }
        Console.WriteLine();

        // Create a new post
        var newArticlePost = new CreateArticlePostInput();
        newArticlePost.Locale = "en";
        newArticlePost.Title = "Title";
        newArticlePost.Slug = Guid.NewGuid().ToString();
        newArticlePost.Content = "This is a test article post content.";
        var articlePostDto = await _postAdminAppService.CreateAsync(newArticlePost);
        Console.WriteLine($"Created a new article post:{articlePostDto.Id} - {((ArticlePostAdminDto)articlePostDto).Content}");
        Console.WriteLine();

        // Create a new video post
        var newVideoPost = new CreateVideoPostInput();
        newVideoPost.Locale = "en";
        newVideoPost.Title = "Title";
        newVideoPost.Slug = Guid.NewGuid().ToString();
        newVideoPost.VideoUrl = "test/abc.mp4";
        newVideoPost.Duration = TimeSpan.FromMinutes(3.5); //3分30秒
        var videoPostDto = await _postAdminAppService.CreateAsync(newVideoPost);
        Console.WriteLine($"Created a new video post:{videoPostDto.Id} - {((VideoPostAdminDto)videoPostDto).VideoUrl}");
        Console.WriteLine();

        // Get the created post
        var articlePost = await _postAdminAppService.GetAsync(articlePostDto.Id);
        Console.WriteLine($"Get a article post:{articlePost.Id} - {((ArticlePostAdminDto)articlePost).Content}");
        Console.WriteLine();
        var videoPost = await _postAdminAppService.GetAsync(videoPostDto.Id);
        Console.WriteLine($"Get a video post:{videoPost.Id} - {((VideoPostAdminDto)videoPost).VideoUrl}");
        Console.WriteLine();

        // Get the list of posts
        var list = await _postAdminAppService.GetListAsync(
            new GetPostsInput() {
                Locale = "en",
            });
        Console.WriteLine($"{list.TotalCount} posts found");
        foreach (var post in list.Items)
        {
            Console.WriteLine($"- [{post.Id}] {post.Title} ({post.PostType})");
            if (post.PostType == PostTypeConsts.ArticlePostTypeName)
            {
                var postItem = (ArticlePostAdminDto)post;
                Console.WriteLine($"- article content: [{postItem.Content}]");
            }
            if (post.PostType == PostTypeConsts.VideoPostTypeName)
            {
                var postItem = (VideoPostAdminDto)post;
                Console.WriteLine($"- video url：[{postItem.VideoUrl}] [{postItem.Duration}]");
            }
        }
        Console.WriteLine();
    }
}
