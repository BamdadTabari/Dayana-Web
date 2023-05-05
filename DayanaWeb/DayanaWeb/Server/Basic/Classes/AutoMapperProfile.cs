using AutoMapper;
using DayanaWeb.Server.EntityFramework.Entities.Blog;
using DayanaWeb.Shared.Basic.Classes;
using DayanaWeb.Shared.EntityFramework.DTO.Blog;

namespace DayanaWeb.Server.Basic.Classes;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        #region Blog

        CreateMap<PostEntity, PostDto>().ReverseMap();
        CreateMap<PostCategoryEntity, PostCategoryDto>().ReverseMap();
        CreateMap<PaginatedList<PostCategoryEntity>, PaginatedList<PostCategoryDto>>().ReverseMap();
        CreateMap<PaginatedList<PostEntity>, PaginatedList<PostDto>>().ReverseMap();

        #endregion
    }
}