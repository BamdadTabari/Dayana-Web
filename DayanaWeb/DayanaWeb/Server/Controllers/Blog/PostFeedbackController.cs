using AutoMapper;
using DayanaWeb.Server.EntityFramework.Common;
using DayanaWeb.Server.EntityFramework.Entities.Blog;
using DayanaWeb.Shared.Basic.Classes;
using DayanaWeb.Shared.EntityFramework.DTO.Blog;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace DayanaWeb.Server.Controllers.Blog;

public class PostFeedbackFeedBackController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public PostFeedbackFeedBackController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [Route(BlogRoutes.Post + CRUDRouts.Create)]
    [HttpPost]
    public async Task Create([FromBody] string data)
    {
        var dto = JsonSerializer.Deserialize<PostFeedBackDto>(data);
        var entity = _mapper.Map<PostFeedBackEntity>(dto);
        await _unitOfWork.PostFeedBacks.AddAsync(entity);
        await _unitOfWork.CommitAsync();
    }

    [Route(BlogRoutes.Post + CRUDRouts.ReadOneById +"/{data}")]
    [HttpGet]
    public async Task<PostFeedBackDto> GetById([FromRoute] long data)
    {
        var entity = await _unitOfWork.PostFeedBacks.GetByIdAsync(data);
        var dto = _mapper.Map<PostFeedBackDto>(entity);
        return dto;
    }

    [Route(BlogRoutes.Post + CRUDRouts.ReadListByFilter)]
    [HttpPost]
    public async Task<PaginatedList<PostFeedBackEntity>> GetListByFilter([FromBody] string data)
    {
        var paginationData = JsonSerializer.Deserialize<DefaultPaginationFilter>(data);
        return await _unitOfWork.PostFeedBacks.GetListByFilterAsync(paginationData ?? throw new NullReferenceException());
    }

    [Route(BlogRoutes.Post + CRUDRouts.Delete + "/{data}")]
    [HttpDelete]
    public async Task Delete([FromRoute] long data)
    {
        var entity = await _unitOfWork.PostFeedBacks.GetByIdAsync(data);
        _unitOfWork.PostFeedBacks.Remove(entity);
        await _unitOfWork.CommitAsync();
    }

    [Route(BlogRoutes.Post + CRUDRouts.ReadOneById)]
    [HttpPut]
    public async Task Update([FromBody] string data)
    {
        var dto = JsonSerializer.Deserialize<PostFeedBackDto>(data);
        var entity = _mapper.Map<PostFeedBackEntity>(dto);
        _unitOfWork.PostFeedBacks.Update(entity);
        await _unitOfWork.CommitAsync();
    }
}
