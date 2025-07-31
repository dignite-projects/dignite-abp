using AutoMapper;
using Dignite.Publisher.Categories;
using Dignite.Publisher.Posts;

namespace Dignite.Publisher;

public class PublisherCommonApplicationAutoMapperProfile : Profile
{
    public PublisherCommonApplicationAutoMapperProfile()
    {
        CreateMap<Category, CategoryDto>()
            .MapExtraProperties();

        CreateMap<Post, PostDtoBase>()
            .IncludeAllDerived()
            .MapExtraProperties();

        CreateMap<VideoPost, VideoPostDto>();
        CreateMap<ArticlePost, ArticlePostDto>();

        CreateMap<PostCategory, PostCategoryDto>();
    }
}
