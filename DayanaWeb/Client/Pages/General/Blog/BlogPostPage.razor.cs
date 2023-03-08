using DayanaWeb.Shared.BaseControl;
using DayanaWeb.Shared.EntityFramework.DTO.Blog;
using Microsoft.AspNetCore.Components;

namespace DayanaWeb.Client.Pages.General.Blog;

public partial class BlogPostPage
{
    [Parameter]
    public string Id { get; set; }
    PostDto model = new();

    protected override async Task OnInitializedAsync()
    {
        model = await _httpService.GetValue<PostDto>(Routes.Post + $"get-post/{Id}");

    }
}