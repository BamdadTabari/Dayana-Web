using AutoMapper;
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

    [Route(Routes.PostFeedback + "add-post-feedback")]
    [HttpPost]
    public async Task AddPostFeedback([FromBody] string data)
    {
        var dto = JsonSerializer.Deserialize<PostFeedBackDto>(data);
        var entity = _mapper.Map<PostFeedBack>(dto);
        await _unitOfWork.PostFeedBacks.AddAsync(entity);
        await _unitOfWork.CommitAsync();
    }

    [Route(Routes.PostFeedback + "get-post-feedback/{data}")]
    [HttpGet]
    public async Task<PostFeedBackDto> GetPostFeedback([FromRoute] long data)
    {
        var entity = await _unitOfWork.PostFeedBacks.GetPostFeedBackByIdAsync(data);
        var dto = _mapper.Map<PostFeedBackDto>(entity);
        return dto;
    }

    [Route(Routes.PostFeedback + "get-post-feedback-list-by-filter")]
    [HttpPost]
    public async Task<PaginatedList<PostFeedBack>> GetPostFeedBackListByFilter([FromBody] string data)
    {
        var paginationData = JsonSerializer.Deserialize<DefaultPaginationFilter>(data);
        return await _unitOfWork.PostFeedBacks.GetPostFeedBacksByFilterAsync(paginationData ?? throw new NullReferenceException(CustomizedError<DefaultPaginationFilter>.NullRefError().ToString()));
    }

    [Route(Routes.PostFeedback + "delete-post-feedback/{data}")]
    [HttpDelete]
    public async Task DeletePostFeedback([FromRoute] long data)
    {
        var entity = await _unitOfWork.PostFeedBacks.GetPostFeedBackByIdAsync(data);
        _unitOfWork.PostFeedBacks.Remove(entity);
        await _unitOfWork.CommitAsync();
    }

    [Route(Routes.PostFeedback + "update-post-feedback")]
    [HttpPut]
    public async Task UpdatePostFeedback([FromBody] string data)
    {
        var dto = JsonSerializer.Deserialize<PostFeedBackDto>(data);
        var entity = _mapper.Map<PostFeedBack>(dto);
        _unitOfWork.PostFeedBacks.Update(entity);
        await _unitOfWork.CommitAsync();
    }
}
