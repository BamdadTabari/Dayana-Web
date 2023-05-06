using AutoMapper;
using DayanaWeb.Server.EntityFramework.Common;
using DayanaWeb.Server.EntityFramework.Entities.Blog;
using DayanaWeb.Shared.Basic.Classes;
using DayanaWeb.Shared.EntityFramework.DTO.Blog;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace DayanaWeb.Server.Controllers.Blog;
[ApiController]
public class PostCategoryController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public PostCategoryController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [Route(BlogRoutes.PostCategory + CRUDRouts.Create)]
    [HttpPost]
    public async Task Create([FromBody] string data)
    {
        var dto = JsonSerializer.Deserialize<PostCategoryDto>(data);
        var entity = _mapper.Map<PostCategoryEntity>(dto);
        await _unitOfWork.PostCategories.AddAsync(entity);
        await _unitOfWork.CommitAsync();
    }

    [Route(BlogRoutes.PostCategory + CRUDRouts.ReadAll)]
    [HttpGet]
    public async Task<List<PostCategoryDto>> GetAll()
    {
        var entityList = await _unitOfWork.PostCategories.GetAllAsync();
        var dtoList = _mapper.Map<List<PostCategoryDto>>(entityList);

        return dtoList;
    }

    [Route(BlogRoutes.PostCategory + CRUDRouts.ReadOneById + "/{data}")]
    [HttpGet]
    public async Task<PostCategoryDto> GetById([FromRoute] long data)
    {
        var entity = await _unitOfWork.PostCategories.GetByIdAsync(data);
        var dto = _mapper.Map<PostCategoryDto>(entity);
        return dto;
    }

    [Route(BlogRoutes.PostCategory + CRUDRouts.ReadListByFilter)]
    [HttpPost]
    public async Task<PaginatedList<PostCategoryEntity>> GetListByFilter([FromBody] string data)
    {
        var paginationData = JsonSerializer.Deserialize<DefaultPaginationFilter>(data);
        return await _unitOfWork.PostCategories.GetListByFilterAsync(paginationData ?? throw new NullReferenceException());
    }

    [Route(BlogRoutes.PostCategory + CRUDRouts.Delete + "/{data:long}")]
    [HttpDelete]
    public async Task Delete([FromRoute] long data)
    {
        var entity = await _unitOfWork.PostCategories.GetByIdAsync(data);
        _unitOfWork.PostCategories.Remove(entity);
        await _unitOfWork.CommitAsync();
    }

    [Route(BlogRoutes.PostCategory + CRUDRouts.Update)]
    [HttpPut]
    public async Task Update([FromBody] string data)
    {
        var dto = JsonSerializer.Deserialize<PostCategoryDto>(data);
        var entity = _mapper.Map<PostCategoryEntity>(dto);
        _unitOfWork.PostCategories.Update(entity);
        await _unitOfWork.CommitAsync();
    }
}

