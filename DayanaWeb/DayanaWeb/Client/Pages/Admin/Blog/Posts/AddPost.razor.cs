using DayanaWeb.Shared.BaseControl;
using DayanaWeb.Shared.EntityFramework.DTO.Blog;
using MudBlazor;
using System.Net;

namespace DayanaWeb.Client.Pages.Admin.Blog.Posts;

public partial class AddPost
{
    PostDto model = new();
    List<PostCategoryDto> categoryList = new();
    private long categorySelectedValue { get; set; }

    protected override async Task OnInitializedAsync()
    {
        categoryList = await _httpService.GetValueList<PostCategoryDto>(Routes.PostCategory + "get-post-categories");
    }

    protected async Task Add()
    {
        model.PostCategoryId = categorySelectedValue;
        var response = await _httpService.PostValue(Routes.Post + "add-post", model);
        if (response.StatusCode == HttpStatusCode.OK)
        {
            _snackbar.Add("Post Created Succesfully", Severity.Success);
        }
        else
        {
            _snackbar.Add("Operation Failed", Severity.Error);
        }
    }
}
