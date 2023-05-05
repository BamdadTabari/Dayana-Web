using DayanaWeb.Shared.BaseControl;
using DayanaWeb.Shared.EntityFramework.DTO.Blog;
using MudBlazor;
using System.Net;

namespace DayanaWeb.Client.Pages.Admin.Blog.Category;

public partial class AddPostCategory
{
    PostCategoryDto model = new();

    public async Task Add()
    {
        var response = await _httpService.PostValue(Routes.PostCategory + "add-post-category", model);
        if (response.StatusCode == HttpStatusCode.OK)
        {
            _snackbar.Add("Post Category Created Succesfully", Severity.Success);
        }
        else
        {
            _snackbar.Add("Operation Failed", Severity.Error);
        }
    }
}

