using DayanaWeb.Shared.Basic.Classes;
using DayanaWeb.Shared.EntityFramework.DTO.Blog;
using Microsoft.AspNetCore.Components.Routing;
using MudBlazor;
using System.Net;

namespace DayanaWeb.Client.Pages.Admin.Blog.Categories;
public partial class AddPostCategory
{
    PostCategoryDto model = new();

    public async Task Add()
    {
        var response = await _httpService.PostValue(BlogRoutes.PostCategory + "add-post-category", model);
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
