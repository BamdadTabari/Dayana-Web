using DayanaWeb.Shared.Basic.Classes;
using DayanaWeb.Shared.EntityFramework.DTO.Blog;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Net;

namespace DayanaWeb.Client.Pages.Admin.Blog.Posts;

public partial class EditPost
{
    [Parameter]
    public string Id { get; set; }
    PostDto model = new();
    List<PostCategoryDto> categoryList = new();
    private long categorySelectedValue { get; set; }
    protected override async Task OnInitializedAsync()
    {
        model = await _httpService.GetValue<PostDto>(BlogRoutes.PostCategory + CRUDRouts.ReadOneById + "/{Id}");
        categoryList = await _httpService.GetValueList<PostCategoryDto>(BlogRoutes.PostCategory + CRUDRouts.ReadAll);
    }

    private async Task OnEdit()
    {
        var response = await _httpService.PutValue(BlogRoutes.PostCategory + CRUDRouts.Update, model);
        if (response.StatusCode == HttpStatusCode.OK)
        {
            _snackbar.Add("Post Updated Succesfully", Severity.Success);
        }
        else
        {
            _snackbar.Add("Operation Failed", Severity.Error);
        }
    }
}
