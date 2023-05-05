using AutoMapper;
using DayanaWeb.Server.EntityFramework.Common;
using DayanaWeb.Server.EntityFramework.Entities.Blog;
using DayanaWeb.Shared.Basic.Classes;
using DayanaWeb.Shared.EntityFramework.DTO.Blog;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace DayanaWeb.Server.Controllers.Blog;
[ApiController]
public class PostController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public PostController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [Route(BlogRoutes.Post + CRUDRouts.Create)]
    [HttpPost]
    public async Task Create([FromBody] string data)
    {
        var dto = JsonSerializer.Deserialize<PostDto>(data);
        var entity = _mapper.Map<PostEntity>(dto);
        await _unitOfWork.Posts.AddAsync(entity);
        await _unitOfWork.CommitAsync();
    }

    [Route(BlogRoutes.Post + CRUDRouts.ReadOneById + "/{data}")]
    [HttpGet]
    public async Task<PostDto> GetById([FromRoute] long data)
    {
        var entity = await _unitOfWork.Posts.GetByIdAsync(data);
        var dto = _mapper.Map<PostDto>(entity);
        return dto;
    }

    [Route(BlogRoutes.Post + CRUDRouts.ReadListByFilter)]
    [HttpPost]
    public async Task<PaginatedList<PostEntity>> GetListByFilter([FromBody] string data)
    {
        var paginationData = JsonSerializer.Deserialize<DefaultPaginationFilter>(data);
        return await _unitOfWork.Posts.GetListByFilterAsync(paginationData ?? throw new NullReferenceException(CustomizedError<DefaultPaginationFilter>.NullRefError().ToString()));
    }

    [Route(BlogRoutes.Post + CRUDRouts.Delete + "/{data}")]
    [HttpDelete]
    public async Task Delete([FromRoute] long data)
    {
        var entity = await _unitOfWork.Posts.GetByIdAsync(data);
        _unitOfWork.Posts.Remove(entity);
        await _unitOfWork.CommitAsync();
    }

    [Route(BlogRoutes.Post + CRUDRouts.Update)]
    [HttpPut]
    public async Task Update([FromBody] string data)
    {
        var dto = JsonSerializer.Deserialize<PostDto>(data);
        var entity = _mapper.Map<PostEntity>(dto);
        _unitOfWork.Posts.Update(entity);
        await _unitOfWork.CommitAsync();
    }
}