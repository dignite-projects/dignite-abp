using AutoMapper;
using Dignite.Publisher.Admin.Posts;
using Dignite.Publisher.Posts;

namespace Dignite.Publisher.Admin;

public class PublisherAdminApplicationAutoMapperProfile : Profile
{
    public PublisherAdminApplicationAutoMapperProfile()
    {
        CreateMap<Post, PostAdminDtoBase>()
            .IncludeAllDerived()
            .MapExtraProperties();

        CreateMap<VideoPost, VideoPostAdminDto>();
        CreateMap<ArticlePost, ArticlePostAdminDto>();
    }
}
