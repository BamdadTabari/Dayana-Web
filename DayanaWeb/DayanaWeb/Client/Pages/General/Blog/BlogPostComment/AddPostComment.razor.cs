using DayanaWeb.Shared.BaseControl;
using DayanaWeb.Shared.EntityFramework.DTO.Blog;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Net;

namespace DayanaWeb.Client.Pages.General.Blog.BlogPostComment;

public partial class AddPostComment
{
    [Parameter]
    public long PostId { get; set; }
    public PostFeedBackDto model = new();
    public async Task Add()
    {
        model.PostId = PostId;
        var response = await _httpService.PostValue(Routes.PostFeedback + "add-post-feedback", model);
        if (response.StatusCode == HttpStatusCode.OK)
        {
            _snackbar.Add("Post Comment Created Succesfully", Severity.Success);
            this.StateHasChanged();
        }
        else
        {
            _snackbar.Add("Operation Failed", Severity.Error);
        }
    }
}
