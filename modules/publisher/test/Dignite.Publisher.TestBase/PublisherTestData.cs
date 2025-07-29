using System;
using Volo.Abp.DependencyInjection;

namespace Dignite.Publisher.TestBase;
public class PublisherTestData : ISingletonDependency
{
    public Guid User1Id { get; } = Guid.NewGuid();

    public string User1UserName => "fake.user";

    public Guid User2Id { get; } = Guid.NewGuid();

    public string Content_1 { get; } = "First things first\nI'ma say all the words inside my head\nI'm fired up and tired of the way that things have been, oh-ooh\nThe way that things have been, oh-ooh";

    public string Content_2 { get; } = "Second thing second\nDon't you tell me what you think that I could be\nI'm the one at the sail, I'm the master of my sea, oh-ooh\nThe master of my sea, oh-ooh";

    public string Local_En => "en";

    public string Local_ZhHans => "zh-hans";

    public Guid Category_1_Id { get; } = Guid.NewGuid();

    public string Category_1_DisplayName => "News";

    public string Category_1_Name => "news";

    public Guid Category_2_Id { get; } = Guid.NewGuid();
    public string Category_2_DisplayName => "Local";

    public string Category_2_Name => "local";

    public Guid Post_1_Id { get; } = Guid.NewGuid();

    public string Post_1_Title => "How to install Publisher?";

    public string Post_1_Slug => "how-to-install-publisher";
    public string Article_Post_1_Content => Content_1;

    public Guid Post_2_Id { get; } = Guid.NewGuid();

    public string Post_2_Title => "How to use Publisher?";

    public string Post_2_Slug => "how-to-use-publisher";
    public string Video_Post_2_VideoUrl => "video.mp4";
}
