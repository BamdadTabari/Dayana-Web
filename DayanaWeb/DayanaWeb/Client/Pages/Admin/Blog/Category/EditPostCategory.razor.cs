using DayanaWeb.Shared.BaseControl;
using DayanaWeb.Shared.EntityFramework.DTO.Blog;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Net;

namespace DayanaWeb.Client.Pages.Admin.Blog.Category;

public partial class EditPostCategory
{
    [Parameter]
    public string Id { get; set; }
    PostCategoryDto model = new();
    protected override async Task OnInitializedAsync()
    {
        model = await _httpService.GetValue<PostCategoryDto>(Routes.PostCategory + $"get-post-category/{Id}");
    }

    private async Task OnEdit()
    {
        var response = await _httpService.PutValue(Routes.PostCategory + "update-post-category", model);
        if (response.StatusCode == HttpStatusCode.OK)
        {
            _snackbar.Add("Post Category Updated Succesfully", Severity.Success);
        }
        else
        {
            _snackbar.Add("Operation Failed", Severity.Error);
        }
    }
}
